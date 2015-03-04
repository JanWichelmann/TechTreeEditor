namespace X2AddOnTechTreeEditor
{
	partial class MainForm
	{
		/// <summary>
		/// Erforderliche Designervariable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// Verwendete Ressourcen bereinigen.
		/// </summary>
		/// <param name="disposing">True, wenn verwaltete Ressourcen gelöscht werden sollen; andernfalls False.</param>
		protected override void Dispose(bool disposing)
		{
			if(disposing && (components != null))
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Vom Windows Form-Designer generierter Code

		/// <summary>
		/// Erforderliche Methode für die Designerunterstützung.
		/// Der Inhalt der Methode darf nicht mit dem Code-Editor geändert werden.
		/// </summary>
		private void InitializeComponent()
		{
			this.components = new System.ComponentModel.Container();
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
			this._menuContainer = new System.Windows.Forms.ToolStripContainer();
			this._statusStrip = new System.Windows.Forms.StatusStrip();
			this._statusLabel = new System.Windows.Forms.ToolStripStatusLabel();
			this._selectedNameLabel = new System.Windows.Forms.ToolStripStatusLabel();
			this._toolBoxBar = new System.Windows.Forms.ToolStrip();
			this._mainMenu = new System.Windows.Forms.MenuStrip();
			this._fileMenuButton = new System.Windows.Forms.ToolStripMenuItem();
			this._menuSeparator1 = new System.Windows.Forms.ToolStripSeparator();
			this._editMenuButton = new System.Windows.Forms.ToolStripMenuItem();
			this._menuSeparator2 = new System.Windows.Forms.ToolStripSeparator();
			this._helpMenuButton = new System.Windows.Forms.ToolStripMenuItem();
			this._projectToolBar = new System.Windows.Forms.ToolStrip();
			this._mainToolBar = new System.Windows.Forms.ToolStrip();
			this._civSelectComboBox = new System.Windows.Forms.ToolStripComboBox();
			this._addToolsBar = new System.Windows.Forms.ToolStrip();
			this._techTreeElementContextMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
			this._menuSeparator3 = new System.Windows.Forms.ToolStripSeparator();
			this._menuSeparator4 = new System.Windows.Forms.ToolStripSeparator();
			this._openProjectDialog = new System.Windows.Forms.OpenFileDialog();
			this._newUnitButton = new System.Windows.Forms.ToolStripButton();
			this._newBuildingButton = new System.Windows.Forms.ToolStripButton();
			this._newResearchButton = new System.Windows.Forms.ToolStripButton();
			this._newEyeCandyButton = new System.Windows.Forms.ToolStripButton();
			this._newLinkButton = new System.Windows.Forms.ToolStripButton();
			this._deleteLinkButton = new System.Windows.Forms.ToolStripButton();
			this._newMakeAvailDepButton = new System.Windows.Forms.ToolStripButton();
			this._newSuccResDepButton = new System.Windows.Forms.ToolStripButton();
			this._newBuildingDepButton = new System.Windows.Forms.ToolStripButton();
			this._deleteDepButton = new System.Windows.Forms.ToolStripButton();
			this._deleteElementButton = new System.Windows.Forms.ToolStripButton();
			this._importDATMenuButton = new System.Windows.Forms.ToolStripMenuItem();
			this._openProjectMenuButton = new System.Windows.Forms.ToolStripMenuItem();
			this._saveProjectMenuButton = new System.Windows.Forms.ToolStripMenuItem();
			this._exitMenuButton = new System.Windows.Forms.ToolStripMenuItem();
			this._undoMenuButton = new System.Windows.Forms.ToolStripMenuItem();
			this.redoMenuButton = new System.Windows.Forms.ToolStripMenuItem();
			this._infoMenuButton = new System.Windows.Forms.ToolStripMenuItem();
			this._importDATButton = new System.Windows.Forms.ToolStripButton();
			this._openProjectButton = new System.Windows.Forms.ToolStripButton();
			this._saveProjectButton = new System.Windows.Forms.ToolStripButton();
			this._editorModeButton = new System.Windows.Forms.ToolStripButton();
			this._standardModeButton = new System.Windows.Forms.ToolStripButton();
			this._ageUpButton = new System.Windows.Forms.ToolStripButton();
			this._ageDownButton = new System.Windows.Forms.ToolStripButton();
			this._standardElementCheckButton = new System.Windows.Forms.ToolStripMenuItem();
			this._showInEditorCheckButton = new System.Windows.Forms.ToolStripMenuItem();
			this._gaiaOnlyCheckButton = new System.Windows.Forms.ToolStripMenuItem();
			this._lockIDCheckButton = new System.Windows.Forms.ToolStripMenuItem();
			this._blockForCivCheckButton = new System.Windows.Forms.ToolStripMenuItem();
			this._freeForCivCheckButton = new System.Windows.Forms.ToolStripMenuItem();
			this._renderPanel = new X2AddOnTechTreeEditor.RenderControl();
			this._menuContainer.BottomToolStripPanel.SuspendLayout();
			this._menuContainer.ContentPanel.SuspendLayout();
			this._menuContainer.LeftToolStripPanel.SuspendLayout();
			this._menuContainer.TopToolStripPanel.SuspendLayout();
			this._menuContainer.SuspendLayout();
			this._statusStrip.SuspendLayout();
			this._toolBoxBar.SuspendLayout();
			this._mainMenu.SuspendLayout();
			this._projectToolBar.SuspendLayout();
			this._mainToolBar.SuspendLayout();
			this._addToolsBar.SuspendLayout();
			this._techTreeElementContextMenu.SuspendLayout();
			this.SuspendLayout();
			// 
			// _menuContainer
			// 
			// 
			// _menuContainer.BottomToolStripPanel
			// 
			this._menuContainer.BottomToolStripPanel.Controls.Add(this._statusStrip);
			// 
			// _menuContainer.ContentPanel
			// 
			this._menuContainer.ContentPanel.Controls.Add(this._renderPanel);
			resources.ApplyResources(this._menuContainer.ContentPanel, "_menuContainer.ContentPanel");
			resources.ApplyResources(this._menuContainer, "_menuContainer");
			// 
			// _menuContainer.LeftToolStripPanel
			// 
			this._menuContainer.LeftToolStripPanel.Controls.Add(this._toolBoxBar);
			this._menuContainer.Name = "_menuContainer";
			// 
			// _menuContainer.TopToolStripPanel
			// 
			this._menuContainer.TopToolStripPanel.Controls.Add(this._mainMenu);
			this._menuContainer.TopToolStripPanel.Controls.Add(this._projectToolBar);
			this._menuContainer.TopToolStripPanel.Controls.Add(this._mainToolBar);
			this._menuContainer.TopToolStripPanel.Controls.Add(this._addToolsBar);
			// 
			// _statusStrip
			// 
			resources.ApplyResources(this._statusStrip, "_statusStrip");
			this._statusStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this._statusLabel,
            this._selectedNameLabel});
			this._statusStrip.Name = "_statusStrip";
			// 
			// _statusLabel
			// 
			resources.ApplyResources(this._statusLabel, "_statusLabel");
			this._statusLabel.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
			this._statusLabel.Name = "_statusLabel";
			// 
			// _selectedNameLabel
			// 
			this._selectedNameLabel.Name = "_selectedNameLabel";
			resources.ApplyResources(this._selectedNameLabel, "_selectedNameLabel");
			// 
			// _toolBoxBar
			// 
			resources.ApplyResources(this._toolBoxBar, "_toolBoxBar");
			this._toolBoxBar.ImageScalingSize = new System.Drawing.Size(32, 32);
			this._toolBoxBar.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this._newUnitButton,
            this._newBuildingButton,
            this._newResearchButton,
            this._newEyeCandyButton,
            this._newLinkButton,
            this._deleteLinkButton,
            this._newMakeAvailDepButton,
            this._newSuccResDepButton,
            this._newBuildingDepButton,
            this._deleteDepButton,
            this._deleteElementButton});
			this._toolBoxBar.Name = "_toolBoxBar";
			// 
			// _mainMenu
			// 
			resources.ApplyResources(this._mainMenu, "_mainMenu");
			this._mainMenu.ImageScalingSize = new System.Drawing.Size(32, 32);
			this._mainMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this._fileMenuButton,
            this._editMenuButton,
            this._helpMenuButton});
			this._mainMenu.Name = "_mainMenu";
			// 
			// _fileMenuButton
			// 
			this._fileMenuButton.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this._importDATMenuButton,
            this._openProjectMenuButton,
            this._saveProjectMenuButton,
            this._menuSeparator1,
            this._exitMenuButton});
			this._fileMenuButton.Name = "_fileMenuButton";
			resources.ApplyResources(this._fileMenuButton, "_fileMenuButton");
			// 
			// _menuSeparator1
			// 
			this._menuSeparator1.Name = "_menuSeparator1";
			resources.ApplyResources(this._menuSeparator1, "_menuSeparator1");
			// 
			// _editMenuButton
			// 
			this._editMenuButton.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this._undoMenuButton,
            this.redoMenuButton,
            this._menuSeparator2});
			this._editMenuButton.Name = "_editMenuButton";
			resources.ApplyResources(this._editMenuButton, "_editMenuButton");
			// 
			// _menuSeparator2
			// 
			this._menuSeparator2.Name = "_menuSeparator2";
			resources.ApplyResources(this._menuSeparator2, "_menuSeparator2");
			// 
			// _helpMenuButton
			// 
			this._helpMenuButton.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this._infoMenuButton});
			this._helpMenuButton.Name = "_helpMenuButton";
			resources.ApplyResources(this._helpMenuButton, "_helpMenuButton");
			// 
			// _projectToolBar
			// 
			resources.ApplyResources(this._projectToolBar, "_projectToolBar");
			this._projectToolBar.ImageScalingSize = new System.Drawing.Size(32, 32);
			this._projectToolBar.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this._importDATButton,
            this._openProjectButton,
            this._saveProjectButton});
			this._projectToolBar.Name = "_projectToolBar";
			// 
			// _mainToolBar
			// 
			resources.ApplyResources(this._mainToolBar, "_mainToolBar");
			this._mainToolBar.ImageScalingSize = new System.Drawing.Size(32, 32);
			this._mainToolBar.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this._civSelectComboBox,
            this._editorModeButton,
            this._standardModeButton});
			this._mainToolBar.Name = "_mainToolBar";
			// 
			// _civSelectComboBox
			// 
			this._civSelectComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			resources.ApplyResources(this._civSelectComboBox, "_civSelectComboBox");
			this._civSelectComboBox.Name = "_civSelectComboBox";
			this._civSelectComboBox.SelectedIndexChanged += new System.EventHandler(this._civSelectComboBox_SelectedIndexChanged);
			// 
			// _addToolsBar
			// 
			resources.ApplyResources(this._addToolsBar, "_addToolsBar");
			this._addToolsBar.ImageScalingSize = new System.Drawing.Size(32, 32);
			this._addToolsBar.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this._ageUpButton,
            this._ageDownButton});
			this._addToolsBar.Name = "_addToolsBar";
			// 
			// _techTreeElementContextMenu
			// 
			this._techTreeElementContextMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this._standardElementCheckButton,
            this._menuSeparator3,
            this._showInEditorCheckButton,
            this._gaiaOnlyCheckButton,
            this._lockIDCheckButton,
            this._menuSeparator4,
            this._blockForCivCheckButton,
            this._freeForCivCheckButton});
			this._techTreeElementContextMenu.Name = "_techTreeElementContextMenu";
			resources.ApplyResources(this._techTreeElementContextMenu, "_techTreeElementContextMenu");
			this._techTreeElementContextMenu.MouseEnter += new System.EventHandler(this._techTreeElementContextMenu_MouseEnter);
			this._techTreeElementContextMenu.MouseLeave += new System.EventHandler(this._techTreeElementContextMenu_MouseLeave);
			// 
			// _menuSeparator3
			// 
			this._menuSeparator3.Name = "_menuSeparator3";
			resources.ApplyResources(this._menuSeparator3, "_menuSeparator3");
			// 
			// _menuSeparator4
			// 
			this._menuSeparator4.Name = "_menuSeparator4";
			resources.ApplyResources(this._menuSeparator4, "_menuSeparator4");
			// 
			// _openProjectDialog
			// 
			resources.ApplyResources(this._openProjectDialog, "_openProjectDialog");
			// 
			// _newUnitButton
			// 
			this._newUnitButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			resources.ApplyResources(this._newUnitButton, "_newUnitButton");
			this._newUnitButton.Name = "_newUnitButton";
			// 
			// _newBuildingButton
			// 
			this._newBuildingButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			resources.ApplyResources(this._newBuildingButton, "_newBuildingButton");
			this._newBuildingButton.Name = "_newBuildingButton";
			// 
			// _newResearchButton
			// 
			this._newResearchButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			resources.ApplyResources(this._newResearchButton, "_newResearchButton");
			this._newResearchButton.Name = "_newResearchButton";
			// 
			// _newEyeCandyButton
			// 
			this._newEyeCandyButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			resources.ApplyResources(this._newEyeCandyButton, "_newEyeCandyButton");
			this._newEyeCandyButton.Image = global::X2AddOnTechTreeEditor.Icons.NewEyeCandy;
			this._newEyeCandyButton.Name = "_newEyeCandyButton";
			// 
			// _newLinkButton
			// 
			this._newLinkButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			resources.ApplyResources(this._newLinkButton, "_newLinkButton");
			this._newLinkButton.Name = "_newLinkButton";
			this._newLinkButton.Click += new System.EventHandler(this._newLinkButton_Click);
			// 
			// _deleteLinkButton
			// 
			this._deleteLinkButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			resources.ApplyResources(this._deleteLinkButton, "_deleteLinkButton");
			this._deleteLinkButton.Name = "_deleteLinkButton";
			this._deleteLinkButton.Click += new System.EventHandler(this._deleteLinkButton_Click);
			// 
			// _newMakeAvailDepButton
			// 
			this._newMakeAvailDepButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			resources.ApplyResources(this._newMakeAvailDepButton, "_newMakeAvailDepButton");
			this._newMakeAvailDepButton.Image = global::X2AddOnTechTreeEditor.Icons.MakeAvailDependency;
			this._newMakeAvailDepButton.Name = "_newMakeAvailDepButton";
			this._newMakeAvailDepButton.Click += new System.EventHandler(this._newMakeAvailDepButton_Click);
			// 
			// _newSuccResDepButton
			// 
			this._newSuccResDepButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			resources.ApplyResources(this._newSuccResDepButton, "_newSuccResDepButton");
			this._newSuccResDepButton.Image = global::X2AddOnTechTreeEditor.Icons.UpgradeDependency;
			this._newSuccResDepButton.Name = "_newSuccResDepButton";
			this._newSuccResDepButton.Click += new System.EventHandler(this._newSuccResDepButton_Click);
			// 
			// _newBuildingDepButton
			// 
			this._newBuildingDepButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			resources.ApplyResources(this._newBuildingDepButton, "_newBuildingDepButton");
			this._newBuildingDepButton.Image = global::X2AddOnTechTreeEditor.Icons.BuildingDependeny;
			this._newBuildingDepButton.Name = "_newBuildingDepButton";
			this._newBuildingDepButton.Click += new System.EventHandler(this._newBuildingDepButton_Click);
			// 
			// _deleteDepButton
			// 
			this._deleteDepButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			resources.ApplyResources(this._deleteDepButton, "_deleteDepButton");
			this._deleteDepButton.Image = global::X2AddOnTechTreeEditor.Icons.DeleteDependency;
			this._deleteDepButton.Name = "_deleteDepButton";
			this._deleteDepButton.Click += new System.EventHandler(this._deleteDepButton_Click);
			// 
			// _deleteElementButton
			// 
			this._deleteElementButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			resources.ApplyResources(this._deleteElementButton, "_deleteElementButton");
			this._deleteElementButton.Name = "_deleteElementButton";
			this._deleteElementButton.Click += new System.EventHandler(this._deleteElementButton_Click);
			// 
			// _importDATMenuButton
			// 
			resources.ApplyResources(this._importDATMenuButton, "_importDATMenuButton");
			this._importDATMenuButton.Name = "_importDATMenuButton";
			this._importDATMenuButton.Click += new System.EventHandler(this._importDATMenuButton_Click);
			// 
			// _openProjectMenuButton
			// 
			resources.ApplyResources(this._openProjectMenuButton, "_openProjectMenuButton");
			this._openProjectMenuButton.Name = "_openProjectMenuButton";
			this._openProjectMenuButton.Click += new System.EventHandler(this._openProjectMenuButton_Click);
			// 
			// _saveProjectMenuButton
			// 
			resources.ApplyResources(this._saveProjectMenuButton, "_saveProjectMenuButton");
			this._saveProjectMenuButton.Name = "_saveProjectMenuButton";
			this._saveProjectMenuButton.Click += new System.EventHandler(this._saveProjectMenuButton_Click);
			// 
			// _exitMenuButton
			// 
			resources.ApplyResources(this._exitMenuButton, "_exitMenuButton");
			this._exitMenuButton.Name = "_exitMenuButton";
			this._exitMenuButton.Click += new System.EventHandler(this._exitMenuButton_Click);
			// 
			// _undoMenuButton
			// 
			resources.ApplyResources(this._undoMenuButton, "_undoMenuButton");
			this._undoMenuButton.Name = "_undoMenuButton";
			// 
			// redoMenuButton
			// 
			resources.ApplyResources(this.redoMenuButton, "redoMenuButton");
			this.redoMenuButton.Name = "redoMenuButton";
			// 
			// _infoMenuButton
			// 
			resources.ApplyResources(this._infoMenuButton, "_infoMenuButton");
			this._infoMenuButton.Name = "_infoMenuButton";
			// 
			// _importDATButton
			// 
			this._importDATButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			resources.ApplyResources(this._importDATButton, "_importDATButton");
			this._importDATButton.Name = "_importDATButton";
			this._importDATButton.Click += new System.EventHandler(this._importDATButton_Click);
			// 
			// _openProjectButton
			// 
			this._openProjectButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			resources.ApplyResources(this._openProjectButton, "_openProjectButton");
			this._openProjectButton.Name = "_openProjectButton";
			this._openProjectButton.Click += new System.EventHandler(this._openProjectButton_Click);
			// 
			// _saveProjectButton
			// 
			this._saveProjectButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			resources.ApplyResources(this._saveProjectButton, "_saveProjectButton");
			this._saveProjectButton.Name = "_saveProjectButton";
			this._saveProjectButton.Click += new System.EventHandler(this._saveProjectButton_Click);
			// 
			// _editorModeButton
			// 
			this._editorModeButton.Checked = true;
			this._editorModeButton.CheckOnClick = true;
			this._editorModeButton.CheckState = System.Windows.Forms.CheckState.Checked;
			this._editorModeButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			resources.ApplyResources(this._editorModeButton, "_editorModeButton");
			this._editorModeButton.Name = "_editorModeButton";
			this._editorModeButton.CheckedChanged += new System.EventHandler(this._editorModeButton_CheckedChanged);
			// 
			// _standardModeButton
			// 
			this._standardModeButton.CheckOnClick = true;
			this._standardModeButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			resources.ApplyResources(this._standardModeButton, "_standardModeButton");
			this._standardModeButton.Name = "_standardModeButton";
			this._standardModeButton.CheckedChanged += new System.EventHandler(this._standardModeButton_CheckedChanged);
			// 
			// _ageUpButton
			// 
			this._ageUpButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			resources.ApplyResources(this._ageUpButton, "_ageUpButton");
			this._ageUpButton.Image = global::X2AddOnTechTreeEditor.Icons.AgeUp;
			this._ageUpButton.Name = "_ageUpButton";
			this._ageUpButton.Click += new System.EventHandler(this._ageUpButton_Click);
			// 
			// _ageDownButton
			// 
			this._ageDownButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			resources.ApplyResources(this._ageDownButton, "_ageDownButton");
			this._ageDownButton.Image = global::X2AddOnTechTreeEditor.Icons.AgeDown;
			this._ageDownButton.Name = "_ageDownButton";
			this._ageDownButton.Click += new System.EventHandler(this._ageDownButton_Click);
			// 
			// _standardElementCheckButton
			// 
			this._standardElementCheckButton.CheckOnClick = true;
			resources.ApplyResources(this._standardElementCheckButton, "_standardElementCheckButton");
			this._standardElementCheckButton.Name = "_standardElementCheckButton";
			this._standardElementCheckButton.CheckedChanged += new System.EventHandler(this._standardElementCheckButton_CheckedChanged);
			// 
			// _showInEditorCheckButton
			// 
			this._showInEditorCheckButton.CheckOnClick = true;
			resources.ApplyResources(this._showInEditorCheckButton, "_showInEditorCheckButton");
			this._showInEditorCheckButton.Name = "_showInEditorCheckButton";
			this._showInEditorCheckButton.CheckedChanged += new System.EventHandler(this._showInEditorCheckButton_CheckedChanged);
			// 
			// _gaiaOnlyCheckButton
			// 
			this._gaiaOnlyCheckButton.CheckOnClick = true;
			resources.ApplyResources(this._gaiaOnlyCheckButton, "_gaiaOnlyCheckButton");
			this._gaiaOnlyCheckButton.Name = "_gaiaOnlyCheckButton";
			this._gaiaOnlyCheckButton.CheckedChanged += new System.EventHandler(this._gaiaOnlyCheckButton_CheckedChanged);
			// 
			// _lockIDCheckButton
			// 
			this._lockIDCheckButton.CheckOnClick = true;
			resources.ApplyResources(this._lockIDCheckButton, "_lockIDCheckButton");
			this._lockIDCheckButton.Name = "_lockIDCheckButton";
			this._lockIDCheckButton.CheckedChanged += new System.EventHandler(this._lockIDCheckButton_CheckedChanged);
			// 
			// _blockForCivCheckButton
			// 
			this._blockForCivCheckButton.CheckOnClick = true;
			resources.ApplyResources(this._blockForCivCheckButton, "_blockForCivCheckButton");
			this._blockForCivCheckButton.Name = "_blockForCivCheckButton";
			this._blockForCivCheckButton.CheckedChanged += new System.EventHandler(this._blockForCivCheckButton_CheckedChanged);
			// 
			// _freeForCivCheckButton
			// 
			this._freeForCivCheckButton.CheckOnClick = true;
			resources.ApplyResources(this._freeForCivCheckButton, "_freeForCivCheckButton");
			this._freeForCivCheckButton.Name = "_freeForCivCheckButton";
			this._freeForCivCheckButton.CheckedChanged += new System.EventHandler(this._freeForCivCheckButton_CheckedChanged);
			// 
			// _renderPanel
			// 
			resources.ApplyResources(this._renderPanel, "_renderPanel");
			this._renderPanel.Name = "_renderPanel";
			this._renderPanel.SelectionChanged += new X2AddOnTechTreeEditor.RenderControl.SelectionChangedEventHandler(this._renderPanel_SelectionChanged);
			this._renderPanel.MouseClick += new System.Windows.Forms.MouseEventHandler(this._renderPanel_MouseClick);
			// 
			// MainForm
			// 
			resources.ApplyResources(this, "$this");
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this._menuContainer);
			this.MainMenuStrip = this._mainMenu;
			this.Name = "MainForm";
			this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
			this.Load += new System.EventHandler(this.MainForm_Load);
			this.Shown += new System.EventHandler(this.MainForm_Shown);
			this._menuContainer.BottomToolStripPanel.ResumeLayout(false);
			this._menuContainer.BottomToolStripPanel.PerformLayout();
			this._menuContainer.ContentPanel.ResumeLayout(false);
			this._menuContainer.LeftToolStripPanel.ResumeLayout(false);
			this._menuContainer.LeftToolStripPanel.PerformLayout();
			this._menuContainer.TopToolStripPanel.ResumeLayout(false);
			this._menuContainer.TopToolStripPanel.PerformLayout();
			this._menuContainer.ResumeLayout(false);
			this._menuContainer.PerformLayout();
			this._statusStrip.ResumeLayout(false);
			this._statusStrip.PerformLayout();
			this._toolBoxBar.ResumeLayout(false);
			this._toolBoxBar.PerformLayout();
			this._mainMenu.ResumeLayout(false);
			this._mainMenu.PerformLayout();
			this._projectToolBar.ResumeLayout(false);
			this._projectToolBar.PerformLayout();
			this._mainToolBar.ResumeLayout(false);
			this._mainToolBar.PerformLayout();
			this._addToolsBar.ResumeLayout(false);
			this._addToolsBar.PerformLayout();
			this._techTreeElementContextMenu.ResumeLayout(false);
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.ToolStripContainer _menuContainer;
		private System.Windows.Forms.MenuStrip _mainMenu;
		private System.Windows.Forms.ToolStripMenuItem _fileMenuButton;
		private System.Windows.Forms.ToolStripMenuItem _exitMenuButton;
		private System.Windows.Forms.StatusStrip _statusStrip;
		private System.Windows.Forms.ToolStrip _mainToolBar;
		private System.Windows.Forms.ToolStripComboBox _civSelectComboBox;
		private System.Windows.Forms.ToolStripStatusLabel _statusLabel;
		private System.Windows.Forms.ContextMenuStrip _techTreeElementContextMenu;
		private System.Windows.Forms.ToolStripMenuItem _standardElementCheckButton;
		private System.Windows.Forms.ToolStripStatusLabel _selectedNameLabel;
		private System.Windows.Forms.ToolStripMenuItem _importDATMenuButton;
		private RenderControl _renderPanel;
		private System.Windows.Forms.ToolStripMenuItem _openProjectMenuButton;
		private System.Windows.Forms.ToolStripMenuItem _saveProjectMenuButton;
		private System.Windows.Forms.OpenFileDialog _openProjectDialog;
		private System.Windows.Forms.ToolStrip _toolBoxBar;
		private System.Windows.Forms.ToolStripMenuItem _editMenuButton;
		private System.Windows.Forms.ToolStripMenuItem _undoMenuButton;
		private System.Windows.Forms.ToolStripMenuItem redoMenuButton;
		private System.Windows.Forms.ToolStripSeparator _menuSeparator1;
		private System.Windows.Forms.ToolStripSeparator _menuSeparator2;
		private System.Windows.Forms.ToolStrip _projectToolBar;
		private System.Windows.Forms.ToolStripButton _importDATButton;
		private System.Windows.Forms.ToolStripButton _openProjectButton;
		private System.Windows.Forms.ToolStripButton _saveProjectButton;
		private System.Windows.Forms.ToolStripMenuItem _helpMenuButton;
		private System.Windows.Forms.ToolStripMenuItem _infoMenuButton;
		private System.Windows.Forms.ToolStripButton _newBuildingButton;
		private System.Windows.Forms.ToolStripButton _newUnitButton;
		private System.Windows.Forms.ToolStripButton _newResearchButton;
		private System.Windows.Forms.ToolStripButton _newLinkButton;
		private System.Windows.Forms.ToolStripButton _deleteLinkButton;
		private System.Windows.Forms.ToolStripButton _deleteElementButton;
		private System.Windows.Forms.ToolStripButton _editorModeButton;
		private System.Windows.Forms.ToolStripButton _standardModeButton;
		private System.Windows.Forms.ToolStripMenuItem _showInEditorCheckButton;
		private System.Windows.Forms.ToolStripMenuItem _gaiaOnlyCheckButton;
		private System.Windows.Forms.ToolStripMenuItem _lockIDCheckButton;
		private System.Windows.Forms.ToolStripSeparator _menuSeparator3;
		private System.Windows.Forms.ToolStripMenuItem _blockForCivCheckButton;
		private System.Windows.Forms.ToolStripMenuItem _freeForCivCheckButton;
		private System.Windows.Forms.ToolStripSeparator _menuSeparator4;
		private System.Windows.Forms.ToolStrip _addToolsBar;
		private System.Windows.Forms.ToolStripButton _ageUpButton;
		private System.Windows.Forms.ToolStripButton _ageDownButton;
		private System.Windows.Forms.ToolStripButton _newEyeCandyButton;
		private System.Windows.Forms.ToolStripButton _newMakeAvailDepButton;
		private System.Windows.Forms.ToolStripButton _newSuccResDepButton;
		private System.Windows.Forms.ToolStripButton _newBuildingDepButton;
		private System.Windows.Forms.ToolStripButton _deleteDepButton;
	}
}

