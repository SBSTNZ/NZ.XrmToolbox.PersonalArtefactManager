using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using Microsoft.Crm.Sdk.Messages;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Query;
using NZ.XrmToolbox.PersonalArtefactManager.AppCode.Helpers;
using XrmToolBox.Extensibility;

namespace NZ.XrmToolbox.PersonalArtefactManager.AppCode
{
    internal class PersonalDiagramManager : IPersonalArtefactManager
    {
        private readonly PluginContext _pluginContext;

        public PersonalDiagram[] Diagrams { get; private set; } = new PersonalDiagram[] { };

        public PersonalDiagramManager(PluginContext context)
        {
            _pluginContext = context;
        }

        public event EventHandler PersonalDiagramsListUpdated;

        #region Interface implementation "IPersonalArtefactManager"

        public void Duplicate(IPersonalArtefact artefact, Owner owner)
        {
            var diagramArtefact = (PersonalDiagram)artefact;
            var diagramEntity = diagramArtefact.Entity;

            _pluginContext.WorkAsync(new WorkAsyncInfo
            {
                Message = $"Duplicating userqueryvisualization {diagramEntity.GetAttributeValue<string>("name")} ({diagramEntity.Id.ToString()}) for user {owner.Id}...",
                Work = (worker, args) =>
                {
                    var client = _pluginContext.ConnectionDetail.GetCrmServiceClient();
                    var userIdBefore = client.CallerId;
                    client.CallerId = owner.Id;

                    try
                    {
                        // Define list of attributes that can safely be copied
                        var attribsToBeCopied = new string[]
                        {
                            "datadescription", "isdefault", "name", "presentationdescription", "primaryentitytypecode", "webresourceid"
                        };

                        var newDiagramEntity = new Entity(diagramEntity.LogicalName);
                        newDiagramEntity.Attributes["ownerid"] = owner.entityReference;
                        foreach (var key in attribsToBeCopied)
                        {
                            if (diagramEntity.Contains(key))
                                newDiagramEntity.Attributes[key] = diagramEntity.Attributes[key];
                        }
                       
                        var response = _pluginContext.Service.Create(newDiagramEntity);
                        args.Result = response;
                    }
                    catch (Exception exc)
                    {
                        args.Result = null;
                        client.CallerId = userIdBefore;
                        throw exc;
                    }
                },
                PostWorkCallBack = (args) =>
                {
                    if (args.Error != null)
                    {
                        ErrorHelper.ShowExceptionMessageDialog(args.Error);
                    }
                    else
                    {
                        var newRecordId = (Guid) args.Result;
                        Clipboard.SetText(newRecordId.ToString());
                        MessageBox.Show($"Personal Diagram successfully duplicated and assigned to user \"{owner.LogicalName}\" ({owner.Name} / {owner.Id}). New record Id was copied to clipboard ({newRecordId.ToString()})", "Info", MessageBoxButtons.OK, MessageBoxIcon.None);
                    }
                }
            });
        }

        public void Assign(IPersonalArtefact artefact, Owner newOwner)
        {
            var diagramArtefact = (PersonalDiagram)artefact;
            var diagramEntity = diagramArtefact.Entity;

            _pluginContext.WorkAsync(new WorkAsyncInfo
            {
                Message = $"Assigning userqueryvisualization {diagramEntity.GetAttributeValue<string>("name")} ({diagramEntity.Id.ToString()})...",
                Work = (worker, args) =>
                {
                    var client = _pluginContext.ConnectionDetail.GetCrmServiceClient();
                    var userIdBefore = client.CallerId;
                    client.CallerId = diagramEntity.GetAttributeValue<EntityReference>("ownerid").Id;

                    try
                    {
                        var response = (AssignResponse)_pluginContext.Service.Execute(new AssignRequest()
                        {
                            Assignee = newOwner.entityReference,
                            Target = diagramEntity.ToEntityReference()
                        });
                    }
                    catch (Exception exc)
                    {
                        args.Result = null;
                        client.CallerId = userIdBefore;
                        throw exc;
                    }
                },
                PostWorkCallBack = (args) =>
                {
                    if (args.Error != null)
                    {
                        ErrorHelper.ShowExceptionMessageDialog(args.Error);
                    }
                    else
                    {
                        MessageBox.Show($"Personal Diagram successfully assigned to user \"{newOwner.LogicalName}\" ({newOwner.Name} / {newOwner.Id})", "Info", MessageBoxButtons.OK, MessageBoxIcon.None);
                    }
                }
            });
        }

        /// <summary>
        /// Delete `PersonalView` artefact
        /// </summary>
        /// <param name="artefact" type="PersonalView"></param>
        public void Delete(IPersonalArtefact artefact)
        {
            var diagramArtefact = (PersonalView) artefact;
            var diagramEntity = diagramArtefact.Entity;

            _pluginContext.WorkAsync(new WorkAsyncInfo
            {
                Message = $"Deleting  userqueryvisualization {diagramEntity.GetAttributeValue<string>("name")} ({diagramEntity.Id.ToString()})...",
                Work = (worker, args) =>
                {
                    var client = _pluginContext.ConnectionDetail.GetCrmServiceClient();
                    var userIdBefore = client.CallerId;
                    client.CallerId = diagramEntity.GetAttributeValue<EntityReference>("ownerid").Id;

                    try
                    {
                        _pluginContext.Service.Delete(diagramEntity.LogicalName, diagramEntity.Id);
                        args.Result = true;
                    }
                    catch (Exception exc)
                    {
                        args.Result = false;
                        client.CallerId = userIdBefore;
                        throw exc;
                    }
                },
                PostWorkCallBack = (args) =>
                {
                    if (args.Error != null)
                    {
                        ErrorHelper.ShowExceptionMessageDialog(args.Error);
                    }

                    OnPersonalDiagramsListUpdated();
                }
            });
        }

        /// <summary>
        /// Query personal artefacts for given Owner
        /// </summary>
        /// <param name="owner" type="Owner"></param>
        public void QueryByOwner(Owner owner)
        {                               
            // Clear owner list
            ReplaceDiagrams(Array.Empty<Entity>());

            if (owner == null) return;

            _pluginContext.WorkAsync(new WorkAsyncInfo
            {
                Message = "Quering list of userqueryvisualizations...",
                Work = (worker, args) =>
                {
                    var client = _pluginContext.ConnectionDetail.GetCrmServiceClient();
                    var userIdBefore = client.CallerId;
                    client.CallerId = owner.Id;

                    try
                    {
                        args.Result = _pluginContext.Service.RetrieveMultiple(new QueryExpression("userqueryvisualization")
                        {
                            Orders = {new OrderExpression("name", OrderType.Ascending)},
                            PageInfo = {ReturnTotalRecordCount = true},
                            ColumnSet = new ColumnSet(true),
                            Criteria = new FilterExpression
                            {
                                Conditions = {new ConditionExpression("ownerid", ConditionOperator.Equal, owner.Id)}
                            }
                        });
                    }
                    catch (Exception exc)
                    {
                        client.CallerId = userIdBefore;
                        throw exc;
                    }
                },
                PostWorkCallBack = (args) =>
                {
                    if (args.Error != null)
                    {
                        ErrorHelper.ShowExceptionMessageDialog(args.Error);
                    }
                    else
                    {
                        var resultSet = args.Result as EntityCollection;
                        if (resultSet != null)
                        {
                            ReplaceDiagrams(resultSet.Entities);
                        }
                    }    
                }
            });
        }

        #endregion


        private void ReplaceDiagrams(IEnumerable<Entity> diagramEntities)
        {
            Diagrams = diagramEntities.Select(entity => new PersonalDiagram(this, entity)).ToArray();
            OnPersonalDiagramsListUpdated();
        }

        private void OnPersonalDiagramsListUpdated()
        {
            // Notify all subscribers
            PersonalDiagramsListUpdated?.Invoke(this, null);
        }
    }
}