namespace NZ.XrmToolbox.PersonalArtefactManager
{
    partial class PersonalArtefactManagerControl
    {
        /// <summary> 
        /// Variable nécessaire au concepteur.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Nettoyage des ressources utilisées.
        /// </summary>
        /// <param name="disposing">true si les ressources managées doivent être supprimées ; sinon, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Code généré par le Concepteur de composants

        /// <summary> 
        /// Méthode requise pour la prise en charge du concepteur - ne modifiez pas 
        /// le contenu de cette méthode avec l'éditeur de code.
        /// </summary>
        private void InitializeComponent()
        {
            this.lvSourceUsers = new System.Windows.Forms.ListView();
            this.colUserEmail = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colUserName = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.lvCrmArtefacts = new System.Windows.Forms.ListView();
            this.colSelArtefacts = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colArtefactName = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colEntityName = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.lvTargetUsers = new System.Windows.Forms.ListView();
            this.columnHeader4 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader5 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader6 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.tblLayoutListViews = new System.Windows.Forms.TableLayoutPanel();
            this.lblTargetUsersCaption = new System.Windows.Forms.Label();
            this.lblCrmArtefactsCaption = new System.Windows.Forms.Label();
            this.lblUserArtefactStatus = new System.Windows.Forms.Label();
            this.lblTargetUsersStatus = new System.Windows.Forms.Label();
            this.lblSourceUsersStatus = new System.Windows.Forms.Label();
            this.lblSourceUsersCaption = new System.Windows.Forms.Label();
            this.cbOperationSelector = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.cbArtefactTypeSelector = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.label6 = new System.Windows.Forms.Label();
            this.toolStripMenu = new System.Windows.Forms.ToolStrip();
            this.tsbClose = new System.Windows.Forms.ToolStripButton();
            this.tssSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.btnLoadUsers = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.btnDoMigration = new System.Windows.Forms.ToolStripButton();
            this.tblLayoutListViews.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.panel1.SuspendLayout();
            this.toolStripMenu.SuspendLayout();
            this.SuspendLayout();
            // 
            // lvSourceUsers
            // 
            this.lvSourceUsers.Activation = System.Windows.Forms.ItemActivation.OneClick;
            this.lvSourceUsers.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lvSourceUsers.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lvSourceUsers.CheckBoxes = true;
            this.lvSourceUsers.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.colUserEmail,
            this.colUserName});
            this.lvSourceUsers.Enabled = false;
            this.lvSourceUsers.FullRowSelect = true;
            this.lvSourceUsers.GridLines = true;
            this.lvSourceUsers.HideSelection = false;
            this.lvSourceUsers.Location = new System.Drawing.Point(3, 24);
            this.lvSourceUsers.Margin = new System.Windows.Forms.Padding(3, 0, 20, 3);
            this.lvSourceUsers.MultiSelect = false;
            this.lvSourceUsers.Name = "lvSourceUsers";
            this.lvSourceUsers.Size = new System.Drawing.Size(282, 296);
            this.lvSourceUsers.Sorting = System.Windows.Forms.SortOrder.Ascending;
            this.lvSourceUsers.TabIndex = 5;
            this.lvSourceUsers.UseCompatibleStateImageBehavior = false;
            this.lvSourceUsers.View = System.Windows.Forms.View.Details;
            this.lvSourceUsers.SelectedIndexChanged += new System.EventHandler(this.lvSourceUsers_SelectedIndexChanged);
            // 
            // colUserEmail
            // 
            this.colUserEmail.Text = "Email";
            this.colUserEmail.Width = 120;
            // 
            // colUserName
            // 
            this.colUserName.Text = "Fullname";
            this.colUserName.Width = 160;
            // 
            // lvCrmArtefacts
            // 
            this.lvCrmArtefacts.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lvCrmArtefacts.CheckBoxes = true;
            this.lvCrmArtefacts.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.colSelArtefacts,
            this.colArtefactName,
            this.colEntityName});
            this.lvCrmArtefacts.Enabled = false;
            this.lvCrmArtefacts.FullRowSelect = true;
            this.lvCrmArtefacts.GridLines = true;
            this.lvCrmArtefacts.HideSelection = false;
            this.lvCrmArtefacts.Location = new System.Drawing.Point(308, 24);
            this.lvCrmArtefacts.Margin = new System.Windows.Forms.Padding(3, 0, 20, 3);
            this.lvCrmArtefacts.Name = "lvCrmArtefacts";
            this.lvCrmArtefacts.Size = new System.Drawing.Size(283, 296);
            this.lvCrmArtefacts.Sorting = System.Windows.Forms.SortOrder.Ascending;
            this.lvCrmArtefacts.TabIndex = 6;
            this.lvCrmArtefacts.UseCompatibleStateImageBehavior = false;
            this.lvCrmArtefacts.View = System.Windows.Forms.View.Details;
            this.lvCrmArtefacts.ItemChecked += new System.Windows.Forms.ItemCheckedEventHandler(this.lvCrmArtefacts_ItemChecked);
            // 
            // colSelArtefacts
            // 
            this.colSelArtefacts.Text = "";
            this.colSelArtefacts.Width = 30;
            // 
            // colArtefactName
            // 
            this.colArtefactName.Text = "Name";
            this.colArtefactName.Width = 120;
            // 
            // colEntityName
            // 
            this.colEntityName.Text = "Entity";
            this.colEntityName.Width = 160;
            // 
            // lvTargetUsers
            // 
            this.lvTargetUsers.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lvTargetUsers.CheckBoxes = true;
            this.lvTargetUsers.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader4,
            this.columnHeader5,
            this.columnHeader6});
            this.lvTargetUsers.Enabled = false;
            this.lvTargetUsers.FullRowSelect = true;
            this.lvTargetUsers.GridLines = true;
            this.lvTargetUsers.HideSelection = false;
            this.lvTargetUsers.Location = new System.Drawing.Point(614, 24);
            this.lvTargetUsers.Margin = new System.Windows.Forms.Padding(3, 0, 20, 3);
            this.lvTargetUsers.MultiSelect = false;
            this.lvTargetUsers.Name = "lvTargetUsers";
            this.lvTargetUsers.Size = new System.Drawing.Size(284, 296);
            this.lvTargetUsers.Sorting = System.Windows.Forms.SortOrder.Ascending;
            this.lvTargetUsers.TabIndex = 7;
            this.lvTargetUsers.UseCompatibleStateImageBehavior = false;
            this.lvTargetUsers.View = System.Windows.Forms.View.Details;
            this.lvTargetUsers.ItemChecked += new System.Windows.Forms.ItemCheckedEventHandler(this.lvTargetUsers_ItemChecked);
            // 
            // columnHeader4
            // 
            this.columnHeader4.Text = "";
            this.columnHeader4.Width = 29;
            // 
            // columnHeader5
            // 
            this.columnHeader5.Text = "Email";
            this.columnHeader5.Width = 120;
            // 
            // columnHeader6
            // 
            this.columnHeader6.Text = "Fullname";
            this.columnHeader6.Width = 160;
            // 
            // tblLayoutListViews
            // 
            this.tblLayoutListViews.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tblLayoutListViews.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.tblLayoutListViews.BackColor = System.Drawing.SystemColors.Control;
            this.tblLayoutListViews.ColumnCount = 3;
            this.tblLayoutListViews.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33332F));
            this.tblLayoutListViews.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33334F));
            this.tblLayoutListViews.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33334F));
            this.tblLayoutListViews.Controls.Add(this.lblTargetUsersCaption, 2, 0);
            this.tblLayoutListViews.Controls.Add(this.lblCrmArtefactsCaption, 1, 0);
            this.tblLayoutListViews.Controls.Add(this.lvSourceUsers, 0, 1);
            this.tblLayoutListViews.Controls.Add(this.lvTargetUsers, 2, 1);
            this.tblLayoutListViews.Controls.Add(this.lvCrmArtefacts, 1, 1);
            this.tblLayoutListViews.Controls.Add(this.lblUserArtefactStatus, 1, 2);
            this.tblLayoutListViews.Controls.Add(this.lblTargetUsersStatus, 2, 2);
            this.tblLayoutListViews.Controls.Add(this.lblSourceUsersStatus, 0, 2);
            this.tblLayoutListViews.Controls.Add(this.lblSourceUsersCaption, 0, 0);
            this.tblLayoutListViews.Location = new System.Drawing.Point(20, 128);
            this.tblLayoutListViews.Name = "tblLayoutListViews";
            this.tblLayoutListViews.RowCount = 3;
            this.tblLayoutListViews.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 24F));
            this.tblLayoutListViews.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tblLayoutListViews.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tblLayoutListViews.Size = new System.Drawing.Size(918, 343);
            this.tblLayoutListViews.TabIndex = 8;
            // 
            // lblTargetUsersCaption
            // 
            this.lblTargetUsersCaption.AutoSize = true;
            this.lblTargetUsersCaption.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTargetUsersCaption.Location = new System.Drawing.Point(614, 0);
            this.lblTargetUsersCaption.Name = "lblTargetUsersCaption";
            this.lblTargetUsersCaption.Size = new System.Drawing.Size(135, 17);
            this.lblTargetUsersCaption.TabIndex = 19;
            this.lblTargetUsersCaption.Text = "3) Target User(s)";
            // 
            // lblCrmArtefactsCaption
            // 
            this.lblCrmArtefactsCaption.AutoSize = true;
            this.lblCrmArtefactsCaption.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCrmArtefactsCaption.Location = new System.Drawing.Point(308, 0);
            this.lblCrmArtefactsCaption.Name = "lblCrmArtefactsCaption";
            this.lblCrmArtefactsCaption.Size = new System.Drawing.Size(88, 17);
            this.lblCrmArtefactsCaption.TabIndex = 18;
            this.lblCrmArtefactsCaption.Text = "2) Artifacts";
            // 
            // lblUserArtefactStatus
            // 
            this.lblUserArtefactStatus.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblUserArtefactStatus.Location = new System.Drawing.Point(308, 323);
            this.lblUserArtefactStatus.Margin = new System.Windows.Forms.Padding(3, 0, 20, 0);
            this.lblUserArtefactStatus.Name = "lblUserArtefactStatus";
            this.lblUserArtefactStatus.Size = new System.Drawing.Size(283, 19);
            this.lblUserArtefactStatus.TabIndex = 14;
            this.lblUserArtefactStatus.Text = "--";
            this.lblUserArtefactStatus.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblTargetUsersStatus
            // 
            this.lblTargetUsersStatus.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblTargetUsersStatus.Location = new System.Drawing.Point(614, 323);
            this.lblTargetUsersStatus.Margin = new System.Windows.Forms.Padding(3, 0, 20, 0);
            this.lblTargetUsersStatus.Name = "lblTargetUsersStatus";
            this.lblTargetUsersStatus.Size = new System.Drawing.Size(284, 19);
            this.lblTargetUsersStatus.TabIndex = 12;
            this.lblTargetUsersStatus.Text = "--";
            this.lblTargetUsersStatus.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblSourceUsersStatus
            // 
            this.lblSourceUsersStatus.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblSourceUsersStatus.Location = new System.Drawing.Point(3, 323);
            this.lblSourceUsersStatus.Margin = new System.Windows.Forms.Padding(3, 0, 20, 0);
            this.lblSourceUsersStatus.Name = "lblSourceUsersStatus";
            this.lblSourceUsersStatus.Size = new System.Drawing.Size(282, 19);
            this.lblSourceUsersStatus.TabIndex = 13;
            this.lblSourceUsersStatus.Text = "--";
            this.lblSourceUsersStatus.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblSourceUsersCaption
            // 
            this.lblSourceUsersCaption.AutoSize = true;
            this.lblSourceUsersCaption.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSourceUsersCaption.Location = new System.Drawing.Point(3, 0);
            this.lblSourceUsersCaption.Name = "lblSourceUsersCaption";
            this.lblSourceUsersCaption.Size = new System.Drawing.Size(118, 17);
            this.lblSourceUsersCaption.TabIndex = 17;
            this.lblSourceUsersCaption.Text = "1) Source User";
            // 
            // cbOperationSelector
            // 
            this.cbOperationSelector.BackColor = System.Drawing.SystemColors.Info;
            this.cbOperationSelector.DropDownHeight = 100;
            this.cbOperationSelector.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbOperationSelector.Enabled = false;
            this.cbOperationSelector.FormattingEnabled = true;
            this.cbOperationSelector.IntegralHeight = false;
            this.cbOperationSelector.Location = new System.Drawing.Point(480, 35);
            this.cbOperationSelector.Name = "cbOperationSelector";
            this.cbOperationSelector.Size = new System.Drawing.Size(207, 24);
            this.cbOperationSelector.TabIndex = 21;
            this.cbOperationSelector.SelectedIndexChanged += new System.EventHandler(this.cbOperationSelector_SelectedIndexChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(477, 13);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(102, 17);
            this.label2.TabIndex = 20;
            this.label2.Text = "Operation type";
            // 
            // cbArtefactTypeSelector
            // 
            this.cbArtefactTypeSelector.BackColor = System.Drawing.SystemColors.Info;
            this.cbArtefactTypeSelector.DropDownHeight = 100;
            this.cbArtefactTypeSelector.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbArtefactTypeSelector.FormattingEnabled = true;
            this.cbArtefactTypeSelector.IntegralHeight = false;
            this.cbArtefactTypeSelector.Location = new System.Drawing.Point(137, 35);
            this.cbArtefactTypeSelector.Name = "cbArtefactTypeSelector";
            this.cbArtefactTypeSelector.Size = new System.Drawing.Size(268, 24);
            this.cbArtefactTypeSelector.TabIndex = 17;
            this.cbArtefactTypeSelector.SelectedIndexChanged += new System.EventHandler(this.cbArtefactTypeSelector_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(136, 13);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(196, 17);
            this.label1.TabIndex = 16;
            this.label1.Text = "What do you want to migrate?";
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tableLayoutPanel1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.tableLayoutPanel1.BackColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.tableLayoutPanel1.ColumnCount = 3;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 872F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Controls.Add(this.panel1, 1, 0);
            this.tableLayoutPanel1.Location = new System.Drawing.Point(3, 32);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 1;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(946, 81);
            this.tableLayoutPanel1.TabIndex = 11;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.label6);
            this.panel1.Controls.Add(this.cbOperationSelector);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.cbArtefactTypeSelector);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(40, 3);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(866, 75);
            this.panel1.TabIndex = 12;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Wingdings", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.ForeColor = System.Drawing.SystemColors.ControlLight;
            this.label6.Location = new System.Drawing.Point(422, 22);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(43, 41);
            this.label6.TabIndex = 23;
            this.label6.Text = "è";
            this.label6.UseCompatibleTextRendering = true;
            // 
            // toolStripMenu
            // 
            this.toolStripMenu.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.toolStripMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsbClose,
            this.tssSeparator1,
            this.btnLoadUsers,
            this.toolStripSeparator2,
            this.btnDoMigration});
            this.toolStripMenu.Location = new System.Drawing.Point(0, 0);
            this.toolStripMenu.Name = "toolStripMenu";
            this.toolStripMenu.Size = new System.Drawing.Size(949, 27);
            this.toolStripMenu.TabIndex = 12;
            // 
            // tsbClose
            // 
            this.tsbClose.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.tsbClose.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbClose.Name = "tsbClose";
            this.tsbClose.Size = new System.Drawing.Size(94, 24);
            this.tsbClose.Text = "Close Plugin";
            this.tsbClose.Click += new System.EventHandler(this.tsbClose_Click);
            // 
            // tssSeparator1
            // 
            this.tssSeparator1.Name = "tssSeparator1";
            this.tssSeparator1.Size = new System.Drawing.Size(6, 27);
            // 
            // btnLoadUsers
            // 
            this.btnLoadUsers.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.btnLoadUsers.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnLoadUsers.Name = "btnLoadUsers";
            this.btnLoadUsers.Size = new System.Drawing.Size(99, 24);
            this.btnLoadUsers.Text = "Reload Users";
            this.btnLoadUsers.Click += new System.EventHandler(this.btnLoadUsers_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 27);
            // 
            // btnDoMigration
            // 
            this.btnDoMigration.Image = global::NZ.XrmToolbox.PersonalArtefactManager.Properties.Resources.Execute;
            this.btnDoMigration.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.btnDoMigration.ImageTransparentColor = System.Drawing.Color.White;
            this.btnDoMigration.Name = "btnDoMigration";
            this.btnDoMigration.Size = new System.Drawing.Size(80, 24);
            this.btnDoMigration.Text = "Execute";
            this.btnDoMigration.Click += new System.EventHandler(this.btnDoMigration_Click);
            // 
            // PersonalArtefactManagerControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.Controls.Add(this.tableLayoutPanel1);
            this.Controls.Add(this.tblLayoutListViews);
            this.Controls.Add(this.toolStripMenu);
            this.Margin = new System.Windows.Forms.Padding(4);
            this.MinimumSize = new System.Drawing.Size(949, 489);
            this.Name = "PersonalArtefactManagerControl";
            this.Size = new System.Drawing.Size(949, 489);
            this.OnCloseTool += new System.EventHandler(this.PersonalArtefactControl_OnCloseTool);
            this.ConnectionUpdated += new XrmToolBox.Extensibility.PluginControlBase.ConnectionUpdatedHandler(this.PersonalArtefactManagerControl_ConnectionUpdated);
            this.Load += new System.EventHandler(this.PersonalArtefactManagerControl_Load);
            this.tblLayoutListViews.ResumeLayout(false);
            this.tblLayoutListViews.PerformLayout();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.toolStripMenu.ResumeLayout(false);
            this.toolStripMenu.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.ToolStrip toolStripMenu;
        
        private System.Windows.Forms.ColumnHeader colUserEmail;
        private System.Windows.Forms.ColumnHeader colUserName;
		
        private System.Windows.Forms.ListView lvCrmArtefacts;
        private System.Windows.Forms.ColumnHeader colArtefactName;
        private System.Windows.Forms.ColumnHeader colEntityName;
        private System.Windows.Forms.ListView lvTargetUsers;
        private System.Windows.Forms.ColumnHeader columnHeader4;
        private System.Windows.Forms.ColumnHeader columnHeader5;
        private System.Windows.Forms.ColumnHeader columnHeader6;
        private System.Windows.Forms.ColumnHeader colSelArtefacts;
        
        private System.Windows.Forms.TableLayoutPanel tblLayoutListViews;
        private System.Windows.Forms.ListView lvSourceUsers;
        private System.Windows.Forms.Label lblUserArtefactStatus;
        private System.Windows.Forms.Label lblSourceUsersStatus;
        private System.Windows.Forms.Label lblTargetUsersStatus;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.Label lblTargetUsersCaption;
        private System.Windows.Forms.Label lblCrmArtefactsCaption;
        private System.Windows.Forms.Label lblSourceUsersCaption;
        private System.Windows.Forms.ComboBox cbOperationSelector;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox cbArtefactTypeSelector;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label6;
		
		private System.Windows.Forms.ToolStripButton tsbClose;
private System.Windows.Forms.ToolStripSeparator tssSeparator1;
private System.Windows.Forms.ToolStripButton btnLoadUsers;
private System.Windows.Forms.ToolStripButton btnDoMigration;
    }
}
