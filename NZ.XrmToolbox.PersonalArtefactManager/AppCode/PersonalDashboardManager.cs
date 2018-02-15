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
    internal class PersonalDashboardManager : IPersonalArtefactManager
    {
        private readonly PluginContext _pluginContext;
        
        public PersonalDashboard[] Dashboards { get; private set; } = new PersonalDashboard[] { };

        public PersonalDashboardManager(PluginContext context)
        {
            _pluginContext = context;
        }

        public event EventHandler PersonalDashboardsListUpdated;

        private void ReplaceDashboards(IEnumerable<Entity> diagramEntities)
        {
            Dashboards = diagramEntities.Select(entity => new PersonalDashboard(this, entity)).ToArray();
            OnPersonalDashboardsListUpdated();
        }

        private void OnPersonalDashboardsListUpdated()
        {
            // Notify all subscribers
            PersonalDashboardsListUpdated?.Invoke(this, null);
        }

        #region Interface implementation "IPersonalArtefactManager"

        public void Duplicate(IPersonalArtefact artefact, User owner)
        {
            var dashboardArtefact = (PersonalDashboard)artefact;
            var dashboardEntity = dashboardArtefact.Entity;

            _pluginContext.WorkAsync(new WorkAsyncInfo
            {
                Message = $"Duplicating userform {dashboardEntity.GetAttributeValue<string>("name")} ({dashboardEntity.Id.ToString()}) for user {owner.Id}...",
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
                            "description", "formxml", "name", "istabletenabled", "objecttypecode", "type"
                        };

                        var newDashboardEntity = new Entity(dashboardEntity.LogicalName);
                        newDashboardEntity.Attributes["ownerid"] = owner.Entity.ToEntityReference();
                        foreach (var key in attribsToBeCopied)
                        {
                            if (dashboardEntity.Contains(key))
                                newDashboardEntity.Attributes[key] = dashboardEntity.Attributes[key];
                        }
                       
                        var response = _pluginContext.Service.Create(newDashboardEntity);
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
                        MessageBox.Show($"Personal Dashboard successfully duplicated and assigned to user \"{owner.Fullname}\" ({owner.Email} / {owner.Id}). New record Id was copied to clipboard ({newRecordId.ToString()})", "Info", MessageBoxButtons.OK, MessageBoxIcon.None);
                    }
                }
            });
        }

        public void Assign(IPersonalArtefact artefact, User newOwner)
        {
            var dashboardArtefact = (PersonalDashboard)artefact;
            var dashboardEntity = dashboardArtefact.Entity;

            _pluginContext.WorkAsync(new WorkAsyncInfo
            {
                Message = $"Assigning userform {dashboardEntity.GetAttributeValue<string>("name")} ({dashboardEntity.Id.ToString()})...",
                Work = (worker, args) =>
                {
                    var client = _pluginContext.ConnectionDetail.GetCrmServiceClient();
                    var userIdBefore = client.CallerId;
                    client.CallerId = dashboardEntity.GetAttributeValue<EntityReference>("ownerid").Id;

                    try
                    {
                        var response = (AssignResponse)_pluginContext.Service.Execute(new AssignRequest()
                        {
                            Assignee = newOwner.Entity.ToEntityReference(),
                            Target = dashboardEntity .ToEntityReference()
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
                        MessageBox.Show($"Personal Dashboard successfully assigned to user \"{newOwner.Fullname}\" ({newOwner.Email} / {newOwner.Id})", "Info", MessageBoxButtons.OK, MessageBoxIcon.None);
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
            var dashboardArtefact = (PersonalDashboard) artefact;
            var dashboardEntity = dashboardArtefact.Entity;

            _pluginContext.WorkAsync(new WorkAsyncInfo
            {
                Message = $"Deleting userform {dashboardEntity.GetAttributeValue<string>("name")} ({dashboardEntity.Id.ToString()})...",
                Work = (worker, args) =>
                {
                    var client = _pluginContext.ConnectionDetail.GetCrmServiceClient();
                    var userIdBefore = client.CallerId;
                    client.CallerId = dashboardEntity.GetAttributeValue<EntityReference>("ownerid").Id;

                    try
                    {
                        _pluginContext.Service.Delete(dashboardEntity.LogicalName, dashboardEntity.Id);
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
                    
                    OnPersonalDashboardsListUpdated();
                }
            });
        }

        /// <summary>
        /// Query personal artefacts for given User
        /// </summary>
        /// <param name="user" type="User"></param>
        public void QueryByUser(User user)
        {                               
            // Clear user list
            ReplaceDashboards(Array.Empty<Entity>());

            if (user == null) return;

            _pluginContext.WorkAsync(new WorkAsyncInfo
            {
                Message = "Quering list of userforms...",
                Work = (worker, args) =>
                {
                    var client = _pluginContext.ConnectionDetail.GetCrmServiceClient();
                    var userIdBefore = client.CallerId;
                    client.CallerId = user.Id;

                    try
                    {
                        args.Result = _pluginContext.Service.RetrieveMultiple(new QueryExpression("userform")
                        {
                            Orders = {new OrderExpression("name", OrderType.Ascending)},
                            PageInfo = {ReturnTotalRecordCount = true},
                            ColumnSet = new ColumnSet(true),
                            Criteria = new FilterExpression
                            {
                                Conditions = {new ConditionExpression("ownerid", ConditionOperator.Equal, user.Id)}
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
                            ReplaceDashboards(resultSet.Entities);
                        }
                    }    
                }
            });
        }

        #endregion


        
    }
}