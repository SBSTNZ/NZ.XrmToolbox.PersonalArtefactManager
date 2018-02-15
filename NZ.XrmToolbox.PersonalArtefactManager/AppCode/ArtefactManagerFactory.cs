using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xrm.Sdk;
using NZ.XrmToolbox.PersonalArtefactManager.AppCode.Events;
using NZ.XrmToolbox.PersonalArtefactManager.AppCode.Form;

namespace NZ.XrmToolbox.PersonalArtefactManager.AppCode
{
    internal class ArtefactManagerFactory
    {

        #region Singleton instances

        private Dictionary<string, IPersonalArtefactManager> ManagerInstances { get; set; } = new Dictionary<string, IPersonalArtefactManager>();
        private Dictionary<string, ListViewBuilder> BuilderInstances { get; set; } = new Dictionary<string, ListViewBuilder>(); 
        
        #endregion

        private PluginContext _pluginContext;

        internal EventHandler<ArtefactListUpdatedEventArgs> ArtefactListUpdated;

        public ArtefactManagerFactory(PluginContext context)
        {
            _pluginContext = context;
        }

        /// <summary>
        /// Tells if the given artefact type is known to the current factory instance
        /// </summary>
        /// <param name="typeName"></param>
        /// <returns></returns>
        public bool IsKnownType(string typeName)
        {
            return (typeName == PersonalArtefactType.UserQuery) 
                || (typeName == PersonalArtefactType.UserForm) 
                || (typeName == PersonalArtefactType.UserQueryVisualization);
        }

        /// <summary>
        /// Factory method for returning the apropriate manager instance for the specified type
        /// </summary>
        /// <param name="typeName"></param>
        /// <returns></returns>
        public IPersonalArtefactManager GetManager(string typeName)
        {
            IPersonalArtefactManager instance = null;
            switch (typeName)
            {
                case PersonalArtefactType.UserQuery:
                    if (!ManagerInstances.ContainsKey(typeName))
                    {
                        var viewManager = new PersonalViewManager(_pluginContext);
                        viewManager.PersonalViewsListUpdated += (object sender, EventArgs evt) =>
                        {
                            ArtefactListUpdated?.Invoke(this, new ArtefactListUpdatedEventArgs(viewManager.Views));
                        };
                        ManagerInstances.Add(typeName, viewManager);
                    }
                    instance = ManagerInstances[typeName];
                    break;

                case "userform":
                    if (!ManagerInstances.ContainsKey(typeName))
                    {
                        var dashboardManager = new PersonalDashboardManager(_pluginContext);
                        dashboardManager.PersonalDashboardsListUpdated += (object sender, EventArgs evt) =>
                        {
                            ArtefactListUpdated?.Invoke(this, new ArtefactListUpdatedEventArgs(dashboardManager.Dashboards));
                        };
                        ManagerInstances.Add(typeName, dashboardManager);
                    }
                    instance = ManagerInstances[typeName];
                    break;

                case "userqueryvisualization":
                    if (!ManagerInstances.ContainsKey(typeName))
                    {
                        var diagramManager = new PersonalDiagramManager(_pluginContext);
                        diagramManager.PersonalDiagramsListUpdated += (object sender, EventArgs evt) =>
                        {
                            ArtefactListUpdated?.Invoke(this, new ArtefactListUpdatedEventArgs(diagramManager.Diagrams));
                        };
                        ManagerInstances.Add(typeName, diagramManager);
                    }
                    instance = ManagerInstances[typeName];
                    break;

                default:
                    throw new UnknownArtefactTypeError(typeName, $"Unknown artefact type \"{typeName}\"");
                    break;
            }

            return instance;
        }

        public ListViewBuilder GetListViewBuilder(string typeName)
        {
            ListViewBuilder instance = null;
            switch (typeName)
            {
                case PersonalArtefactType.UserQuery:
                    if (!BuilderInstances.ContainsKey(typeName))
                    {
                        BuilderInstances.Add(typeName, new PersonalViewListViewBuilder());
                    }
                    instance = BuilderInstances[typeName];
                    break;

                case "userform":
                    if (!BuilderInstances.ContainsKey(typeName))
                    {
                        BuilderInstances.Add(typeName, new PersonalDashboardListViewBuilder());
                    }
                    instance = BuilderInstances[typeName];
                    break;

                case "userqueryvisualization":
                    if (!BuilderInstances.ContainsKey(typeName))
                    {
                        BuilderInstances.Add(typeName, new PersonalDiagramListViewBuilder());
                    }
                    instance = BuilderInstances[typeName];
                    break;

                default:
                    throw new UnknownArtefactTypeError(typeName, $"Unknown artefact type \"{typeName}\"");
                    break;
            }

            return instance;
        }
    }
}