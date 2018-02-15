using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Query;
using XrmToolBox.Extensibility;

namespace NZ.XrmToolbox.PersonalArtefactManager.AppCode
{
    internal class UserManager
    {
        private readonly PluginContext _pluginContext;
        public User[] Users { get; private set; } = new User[] { };

        public event EventHandler UserListUpdated;

        public UserManager(PluginContext context)
        {
            _pluginContext = context;         
        }

        public void LoadSystemUsers()
        {
            _pluginContext.WorkAsync(new WorkAsyncInfo
            {
                Message = "Query list of systemusers",
                Work = (worker, args) =>
                {
                    args.Result = _pluginContext.Service.RetrieveMultiple(new QueryExpression("systemuser")
                    {
                        Orders = { new OrderExpression("internalemailaddress", OrderType.Ascending) },
                        PageInfo = { ReturnTotalRecordCount = true },
                        ColumnSet = new ColumnSet("isdisabled", "internalemailaddress", "fullname"),
                        LinkEntities = { new LinkEntity("systemuser", "systemuserroles", "systemuserid", "systemuserid", JoinOperator.Inner) },
                        Distinct = true
                    });
                },
                PostWorkCallBack = (args) =>
                {
                    // Clear user list
                    Users = new User[] { };

                    if (args.Error != null)
                    {
                        MessageBox.Show(args.Error.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    else
                    {
                        var resultSet = args.Result as EntityCollection;
                        if (resultSet != null)
                        {
                            Users = resultSet.Entities.Select(e => new User(e)).ToArray();
                        }
                    }
                    

                    // Notify all subscribers
                    UserListUpdated(this, null);
                }
            });
        }

        public void Invalidate()
        {
            Users = new User[] {};
            UserListUpdated(this, null);
        }
    }
}