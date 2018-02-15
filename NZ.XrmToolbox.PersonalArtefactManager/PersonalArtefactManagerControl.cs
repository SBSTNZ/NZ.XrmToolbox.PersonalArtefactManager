using System;
using System.Collections.Generic;
using System.Windows.Forms;
using XrmToolBox.Extensibility;
using NZ.XrmToolbox.PersonalArtefactManager.AppCode;
using NZ.XrmToolbox.PersonalArtefactManager.AppCode.Events;
using NZ.XrmToolbox.PersonalArtefactManager.AppCode.Form;
using XrmToolBox.Extensibility.Args;
using XrmToolBox.Extensibility.Interfaces;

namespace NZ.XrmToolbox.PersonalArtefactManager
{
    public partial class PersonalArtefactManagerControl : PluginControlBase, IStatusBarMessenger
    {
        private Settings _pluginSettings;
        private PluginContext _pluginCtx;
        private UserManager _userManager;
        private UserListViewBuilder _userListViewBuilder;
        private ArtefactManagerFactory _artefactManagerFactory;

        private bool _isPluginLoaded = false;
        private bool IsFormStateReadyForProcessing
        {
            get {
                return btnDoMigration.Enabled = (SelectedOperation != null) 
                    && (SelectedCrmArtefacts.Length > 0)
                    && (SelectedSourceUser != null)
                    && (SelectedTargetUsers.Length > 0 || SelectedOperation == PluginOperation.Delete);
            }
        }

        private string SelectedArtefactTypeName
        {
            get
            {
                var selectedItem = cbArtefactTypeSelector.SelectedItem as ComboBoxItem;
                var selectedEntityType = selectedItem?.Tag as string;
                return selectedEntityType;
            }
        }
        private IPersonalArtefact[] SelectedCrmArtefacts
        {
            get
            {
                var selArtefacts = new List<IPersonalArtefact>();
                foreach (ListViewItem item in lvCrmArtefacts.CheckedItems)
                {
                    selArtefacts.Add((IPersonalArtefact)item.Tag);
                }
                return selArtefacts.ToArray();
            }
        }
        private PluginOperation? SelectedOperation
        {
            get
            {
                var selectedItem = cbOperationSelector.SelectedItem as ComboBoxItem;
                if (selectedItem == null) return null;
                var selectedOp = (PluginOperation)selectedItem.Tag;
                return selectedOp;
            }
        }
        private User SelectedSourceUser
        {
            get
            {
                return (lvSourceUsers.SelectedItems.Count == 0)
                    ? null : (lvSourceUsers.SelectedItems[0]?.Tag as User);
            }
        }
        private User[] SelectedTargetUsers
        {
            get
            {
                var selUsers = new List<User>();
                foreach (ListViewItem item in lvTargetUsers.CheckedItems)
                {
                    selUsers.Add((User)item.Tag);
                }
                return selUsers.ToArray();
            }
        }

        #region Events

        public event EventHandler<StatusBarMessageEventArgs> SendMessageToStatusBar;
        internal event EventHandler<ArtefactTypeSelectedEventArgs> PersonalArtifactTypeSelected;
        internal event EventHandler PersonalArtifactSelected;
        internal event EventHandler<UserSelectedEventArgs> SourceUserSelected;
        internal event EventHandler<UserSelectedEventArgs> TargetUserSelected;

        #endregion

        public PersonalArtefactManagerControl()
        {
            InitializeComponent();
        }

        #region Application Event Handlers

        private void PersonalArtefactManagerControl_Load(object sender, EventArgs e)
        {
            //OpenLogFile();
            //ShowInfoNotification("This is a notification that can lead to XrmToolBox repository", new Uri("http://github.com/MscrmTools/XrmToolBox"));

            // Loads or creates the settings for the pluginCtx
            if (!SettingsManager.Instance.TryLoad(GetType(), out _pluginSettings))
            {
                _pluginSettings = new Settings();
                LogWarning("Settings not found => a new settings file has been created!");
            }
            else
            {
                LogInfo("Settings found and loaded");
            }

            _pluginCtx = new PluginContext(this);
            _userManager = new UserManager(_pluginCtx);
            _userListViewBuilder = new UserListViewBuilder();
            _artefactManagerFactory = new ArtefactManagerFactory(_pluginCtx); 

            // Initialize controls
            lvCrmArtefacts.Visible = true;
            cbArtefactTypeSelector.Items.AddRange(new object[] {
                new ComboBoxItem("Personal Views", PersonalArtefactType.UserQuery),
                new ComboBoxItem("Personal Dashboards", PersonalArtefactType.UserForm),
                new ComboBoxItem("Personal Diagrams", PersonalArtefactType.UserQueryVisualization),
            });
            cbOperationSelector.Items.AddRange(new object[] {
                new ComboBoxItem("Copy + Assign", PluginOperation.Duplicate),
                new ComboBoxItem("Assign", PluginOperation.Assign),
                new ComboBoxItem("Delete", PluginOperation.Delete),
            });

            // Wire up application event handlers
            _userManager.UserListUpdated += OnUserManagerOnUserListUpdated;
            _artefactManagerFactory.ArtefactListUpdated += OnArtefactListUpdated;
            PersonalArtifactTypeSelected += OnArtefactTypeSelected;
            SourceUserSelected += OnSourceUserSelected;

            // Initially load user list
            btnLoadUsers.PerformClick();

            ResetFormState();

            _isPluginLoaded = true;
        }

        private void OnArtefactListUpdated(object evtSender, ArtefactListUpdatedEventArgs evt)
        {
            lblUserArtefactStatus.Text = $"{evt.Artefacts.Length} {SelectedArtefactTypeName} found";

            _artefactManagerFactory.GetListViewBuilder(SelectedArtefactTypeName)
                .BuildList(lvCrmArtefacts, evt.Artefacts);

            lvCrmArtefacts.Enabled = true;
            btnDoMigration.Enabled = IsFormStateReadyForProcessing;
        }

        private void OnUserManagerOnUserListUpdated(object evtSender, EventArgs evt)
        {
            lblSourceUsersStatus.Text = lblTargetUsersStatus.Text = $"{_userManager.Users.Length} users found";
            // Populate list of source users
            _userListViewBuilder.HasCheckboxes = false;
            _userListViewBuilder.BuildList(lvSourceUsers, _userManager.Users);
            // Populate list of target users
            _userListViewBuilder.HasCheckboxes = true;
            _userListViewBuilder.BuildList(lvTargetUsers, _userManager.Users);
            // Update visibility state
            lvSourceUsers.Enabled = true;
            cbArtefactTypeSelector.Enabled = true;
            cbOperationSelector.Enabled = true;
            btnDoMigration.Enabled = IsFormStateReadyForProcessing;
        }

        private void OnSourceUserSelected(object evtSender, UserSelectedEventArgs evt)
        {
            if (_artefactManagerFactory.IsKnownType(SelectedArtefactTypeName) == false || SelectedSourceUser == null)
            {
                lvCrmArtefacts.Items.Clear();
                lvCrmArtefacts.Enabled = true;
                return;
            }

            lvCrmArtefacts.Enabled = false;

            // Load data from CRM
            ExecuteMethod(LoadCrmArtefacts);
        }
        
        private void OnArtefactTypeSelected(object evtSender, ArtefactTypeSelectedEventArgs evt)
        {
            if (SelectedSourceUser == null || !_artefactManagerFactory.IsKnownType(SelectedArtefactTypeName))
            {
                lvCrmArtefacts.Items.Clear();
                lvCrmArtefacts.Enabled = true;
                return;
            }

            lvCrmArtefacts.Enabled = false;
            lvTargetUsers.Visible = lblTargetUsersStatus.Visible = lblTargetUsersCaption.Visible = (SelectedOperation != PluginOperation.Delete);

            // Load data from CRM
            ExecuteMethod(LoadCrmArtefacts);
        } 
                   
        /// <summary>
        /// This event occurs when the pluginCtx is closed
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PersonalArtefactControl_OnCloseTool(object sender, EventArgs e)
        {
            // Before leaving, save the settings
            SettingsManager.Instance.Save(GetType(), _pluginSettings);
        }

        /// <summary>
        /// This event occurs when the connection has been updated in XrmToolBox
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PersonalArtefactManagerControl_ConnectionUpdated(object sender, ConnectionUpdatedEventArgs e)
        {
            ResetFormState();

            if (_isPluginLoaded)
            {
                _pluginSettings.LastUsedOrganizationWebappUrl = e.ConnectionDetail.WebApplicationUrl;
                LogInfo("Connection has changed to: {0}", e.ConnectionDetail.WebApplicationUrl);
                // Reload users
                btnLoadUsers.PerformClick();
            }                               
        }

        #endregion

        #region UI Event Handler

        private void tsbClose_Click(object sender, EventArgs e)
        {
            CloseTool();
        }
             
        private void btnLoadUsers_Click(object sender, EventArgs e)
        {
            // Disable list views while loading
            lvSourceUsers.Enabled = lvCrmArtefacts.Enabled = lvTargetUsers.Enabled = false;
            // Invoke user re-load
            ExecuteMethod(LoadCrmUsers);
        }

        

        private void lvSourceUsers_SelectedIndexChanged(object sender, EventArgs e)
        {
            var selectedItem = lvSourceUsers.SelectedItems.Count == 0 ? null : lvSourceUsers.SelectedItems[0];
            var selectedUsers = new List<User>();
            selectedUsers.Add(selectedItem?.Tag as User);
            selectedUsers.RemoveAll(u => u == null);
            // Trigger event
            SourceUserSelected(this, new UserSelectedEventArgs(selectedUsers.ToArray()));
        }

        private void cbArtefactTypeSelector_SelectedIndexChanged(object sender, EventArgs e)
        {
            var selectedEntityType = SelectedArtefactTypeName;
            // Trigger event
            PersonalArtifactTypeSelected(this, new ArtefactTypeSelectedEventArgs(selectedEntityType));
        }

        private void cbOperationSelector_SelectedIndexChanged(object sender, EventArgs e)
        {
            btnDoMigration.Enabled = IsFormStateReadyForProcessing;
            lvTargetUsers.Enabled = (SelectedOperation != PluginOperation.Delete) && (SelectedCrmArtefacts.Length > 0);
            lvTargetUsers.Visible = lblTargetUsersStatus.Visible = lblTargetUsersCaption.Visible = (SelectedOperation != PluginOperation.Delete);
        }

        private void btnDoMigration_Click(object sender, EventArgs e)
        {
            if ((SelectedOperation == null) || (SelectedCrmArtefacts.Length == 0) || (SelectedSourceUser == null) ||
                (SelectedTargetUsers.Length == 0 && SelectedOperation != PluginOperation.Delete))
            {
                return;
            }

            Action operation = delegate () { ProcessArtefacts(); };
            ExecuteMethod(operation);
        }
        
        private void lvCrmArtefacts_ItemChecked(object sender, ItemCheckedEventArgs e)
        {
            var areArtefactsSelected = (lvCrmArtefacts.CheckedItems.Count > 0);
            btnDoMigration.Enabled = IsFormStateReadyForProcessing;
            lvTargetUsers.Enabled = areArtefactsSelected && (SelectedOperation != null);
            lvTargetUsers.Visible = lblTargetUsersStatus.Visible = lblTargetUsersCaption.Visible = (SelectedOperation != PluginOperation.Delete);
        }

        #endregion

        #region Non-Event code

        private void ProcessArtefacts()
        {
            if (SelectedOperation == PluginOperation.Delete)
            {
                var nUnitsOfWork = lvCrmArtefacts.CheckedItems.Count;
                var nUnitsOfWorkDone = 0;
                var percentagePerUnit = 100 / nUnitsOfWork;

                SendMessageToStatusBar?.Invoke(this, new StatusBarMessageEventArgs(nUnitsOfWorkDone, $"Deleting {nUnitsOfWork} artefacts"));
                // Iterate over all artefacts and delete
                foreach (ListViewItem item in lvCrmArtefacts.CheckedItems)
                {
                    var artefact = (IPersonalArtefact)item.Tag;
                    artefact.Container.Delete(artefact);
                    SendMessageToStatusBar?.Invoke(this, new StatusBarMessageEventArgs(Math.Min(100, ++nUnitsOfWork * percentagePerUnit), $"Deleting artefacts"));
                }
            }
            else
            {
                var nUnitsOfWork = lvCrmArtefacts.CheckedItems.Count * SelectedTargetUsers.Length;
                var nUnitsOfWorkDone = 0;
                var percentagePerUnit = 100 / nUnitsOfWork;

                SendMessageToStatusBar?.Invoke(this, new StatusBarMessageEventArgs(nUnitsOfWorkDone, $"Processing {nUnitsOfWork} artefacts"));
                // Iterate through all possible artefact-user combinations and process
                foreach (var targetUser in SelectedTargetUsers)
                {
                    foreach (ListViewItem item in lvCrmArtefacts.CheckedItems)
                    {
                        var artefact = (IPersonalArtefact)item.Tag;
                        switch (SelectedOperation)
                        {
                            case PluginOperation.Assign:
                                artefact.Container.Assign(artefact, targetUser);
                                break;
                            case PluginOperation.Duplicate:
                                artefact.Container.Duplicate(artefact, targetUser);
                                break;
                        }
                        SendMessageToStatusBar?.Invoke(this, new StatusBarMessageEventArgs(Math.Min(100, ++nUnitsOfWork * percentagePerUnit), $"Processing artefacts"));
                    }
                }
            }
        }

        private void ResetFormState()
        {
            // Reset dropdowns
            cbArtefactTypeSelector.SelectedIndex = cbOperationSelector.SelectedIndex = -1;
            cbArtefactTypeSelector.Enabled = cbOperationSelector.Enabled = false;

            // Clear list views
            lvSourceUsers.Clear();
            lvCrmArtefacts.Clear();
            lvTargetUsers.Clear();
            lvSourceUsers.Enabled = lvCrmArtefacts.Enabled = lvTargetUsers.Enabled = false;
        }

        private void LoadCrmUsers()
        {
            _userManager.LoadSystemUsers();
        }
        private void LoadCrmArtefacts()
        {
            _artefactManagerFactory.GetManager(SelectedArtefactTypeName)
                .QueryByUser(SelectedSourceUser);
        }

        #endregion

        private void lvTargetUsers_ItemChecked(object sender, ItemCheckedEventArgs e)
        {
            btnDoMigration.Enabled = IsFormStateReadyForProcessing;
        }
    }
}