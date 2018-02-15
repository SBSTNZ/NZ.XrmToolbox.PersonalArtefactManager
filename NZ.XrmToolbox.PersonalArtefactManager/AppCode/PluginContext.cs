using System;
using McTools.Xrm.Connection;
using Microsoft.Xrm.Sdk;
using XrmToolBox.Extensibility;

namespace NZ.XrmToolbox.PersonalArtefactManager.AppCode
{
    internal class PluginContext
    {
        private PluginControlBase PluginCtrl { get; set; }

        public IOrganizationService Service
        {
            get => PluginCtrl.Service;
        }

        public ConnectionDetail ConnectionDetail
        {
            get => PluginCtrl.ConnectionDetail; 
        }
                       
        event PluginControlBase.ConnectionUpdatedHandler ConnectionUpdated;
        event EventHandler OnCloseTool;

        delegate void EventDelegator(object sender, PluginControlBase.ConnectionUpdatedEventArgs e);

        public PluginContext(PersonalArtefactManagerControl instance)
        {
            PluginCtrl = instance;

            // Attach event delegators
            PluginCtrl.ConnectionUpdated += (object sender, PluginControlBase.ConnectionUpdatedEventArgs e) => ConnectionUpdated?.Invoke(this, e);
            PluginCtrl.OnCloseTool += (object sender, EventArgs e) => OnCloseTool?.Invoke(this, e);
        }

        public void WorkAsync(WorkAsyncInfo info)
        {
            PluginCtrl.WorkAsync(info);
        }

        public void CancelWorker()
        {
            PluginCtrl.CancelWorker();
        }

        public void ExecuteMethod(Action action)
        {
            PluginCtrl.ExecuteMethod(action);
        }

        public void ExecuteMethod<T>(Action<T> action, T parameter)
        {
            PluginCtrl.ExecuteMethod<T>(action, parameter);
        }

    }
}