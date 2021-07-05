using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Security;
using System.Windows.Forms;
using Microsoft.Crm.Sdk;
using Microsoft.Crm.Sdk.Messages;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Query;
using NZ.XrmToolbox.PersonalArtefactManager.AppCode.Helpers;
using XrmToolBox.Extensibility;

namespace NZ.XrmToolbox.PersonalArtefactManager.AppCode
{
    internal class PersonalViewManager : IPersonalArtefactManager
    {
        private readonly PluginContext _pluginContext;

        public PersonalView[] Views { get; private set; } = new PersonalView[] { };

        public PersonalViewManager(PluginContext context)
        {
            _pluginContext = context;
        }

        public event EventHandler PersonalViewsListUpdated;

        #region Interface implementation "IPersonalArtefactManager"

        public void Duplicate(IPersonalArtefact artefact, Owner owner)
        {
            var viewArtefact = (PersonalView)artefact;
            var viewEntity = viewArtefact.Entity;

            _pluginContext.WorkAsync(new WorkAsyncInfo
            {
                Message = $"Duplicating userquery {viewEntity.GetAttributeValue<string>("name")} ({viewEntity.Id.ToString()}) for user {owner.Id}...",
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
                            "advancedgroupby", "columnsetxml", "conditionalformatting", "description", "fetchxml",
                            "layoutxml", "name", "parentqueryid", "querytype", "returnedtypecode", "statecode",
                            "statuscode"
                        };

                        var newViewEntity = new Entity(viewEntity.LogicalName);
                        newViewEntity.Attributes["ownerid"] = owner;
                        foreach (var key in attribsToBeCopied)
                        {
                            if (viewEntity.Contains(key))
                                newViewEntity.Attributes[key] = viewEntity.Attributes[key];
                        }
                       
                        var response = _pluginContext.Service.Create(newViewEntity);
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
                        MessageBox.Show($"Personal View successfully assigned to user \"{owner.LogicalName}\" ({owner.Name} / {owner.Id}). New record Id was copied to clipboard ({newRecordId.ToString()})", "Info", MessageBoxButtons.OK, MessageBoxIcon.None);
                    }
                }
            });
        }

        public void Assign(IPersonalArtefact artefact, Owner newOwner)
        {
            var viewArtefact = (PersonalView)artefact;
            var viewEntity = viewArtefact.Entity;

            _pluginContext.WorkAsync(new WorkAsyncInfo
            {
                Message = $"Assigning userquery {viewEntity.GetAttributeValue<string>("name")} ({viewEntity.Id.ToString()})...",
                Work = (worker, args) =>
                {
                    var client = _pluginContext.ConnectionDetail.GetCrmServiceClient();
                    var userIdBefore = client.CallerId;
                    client.CallerId = viewEntity.GetAttributeValue<EntityReference>("ownerid").Id;

                    try
                    {
                        var response = (AssignResponse)_pluginContext.Service.Execute(new AssignRequest()
                        {
                            Assignee = newOwner,
                            Target = viewEntity.ToEntityReference()
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
                        MessageBox.Show($"Personal View successfully assigned to user \"{newOwner.LogicalName}\" ({newOwner.Name} / {newOwner.Id})", "Info", MessageBoxButtons.OK, MessageBoxIcon.None);
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
            var viewArtefact = (PersonalView) artefact;
            var viewEntity = viewArtefact.Entity;

            _pluginContext.WorkAsync(new WorkAsyncInfo
            {
                Message = $"Deleting  userquery {viewEntity.GetAttributeValue<string>("name")} ({viewEntity.Id.ToString()})...",
                Work = (worker, args) =>
                {
                    var client = _pluginContext.ConnectionDetail.GetCrmServiceClient();
                    var userIdBefore = client.CallerId;
                    client.CallerId = viewEntity.GetAttributeValue<EntityReference>("ownerid").Id;

                    try
                    {
                        _pluginContext.Service.Delete(viewEntity.LogicalName, viewEntity.Id);
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
                                                 
                    OnPersonalViewsListUpdated();
                }
            });
        }

        /// <summary>
        /// Query personal artefacts for given User
        /// </summary>
        /// <param name="owner" type="Owner"></param>
        public void QueryByUser(Owner user)
        {                               
            // Clear owner list
            ReplaceViews(Array.Empty<Entity>());

            if (owner == null) return;

            _pluginContext.WorkAsync(new WorkAsyncInfo
            {
                Message = "Quering list of userqueries...",
                Work = (worker, args) =>
                {
                    var client = _pluginContext.ConnectionDetail.GetCrmServiceClient();
                    var userIdBefore = client.CallerId;
                    client.CallerId = owner.Id;

                    try
                    {
                        args.Result = _pluginContext.Service.RetrieveMultiple(new QueryExpression("userquery")
                        {
                            Orders = {new OrderExpression("name", OrderType.Ascending)},
                            PageInfo = {ReturnTotalRecordCount = true},
                            ColumnSet = new ColumnSet(true),
                            Criteria = new FilterExpression(LogicalOperator.And)
                            {
                                Conditions =
                                {
                                    new ConditionExpression("ownerid", ConditionOperator.Equal, owner.Id),
                                    // Only respect userqueries manually created by users
                                    new ConditionExpression("querytype", ConditionOperator.Equal, UserQueryQueryType.MainApplicationView)
                                }
                            }
                        });
                    }
                    catch (Exception exc)
                    {
                        client.CallerId = userIdBefore;
                        throw exc;
                    }
                    /*catch (FaultException<Microsoft.Xrm.Sdk.OrganizationServiceFault> exc) { HandleException(exc); }
                    catch (TimeoutException exc) { HandleException(exc); }
                    catch (SecurityTokenValidationException exc) { HandleException(exc); }
                    catch (ExpiredSecurityTokenException exc) { HandleException(exc); }
                    catch (MessageSecurityException exc) { HandleException(exc); }
                    catch (SecurityNegotiationException exc) { HandleException(exc); }
                    catch (SecurityAccessDeniedException exc) { HandleException(exc); }
                    catch (Exception exc) { HandleException(exc); }*/
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
                            ReplaceViews(resultSet.Entities);
                        }
                    }    
                }
            });
        }

        #endregion


        private void ReplaceViews(IEnumerable<Entity> viewEntities)
        {
            Views = viewEntities.Select(entity => new PersonalView(this, entity)).ToArray();
            OnPersonalViewsListUpdated();
        }

        private void OnPersonalViewsListUpdated()
        {
            // Notify all subscribers
            PersonalViewsListUpdated?.Invoke(this, null);
        }
    }
}