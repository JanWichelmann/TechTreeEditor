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
			this._techTreeElementContextMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
			this.testToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this._openProjectDialog = new System.Windows.Forms.OpenFileDialog();
			this._newUnitButton = new System.Windows.Forms.ToolStripButton();
			this._newBuildingButton = new System.Windows.Forms.ToolStripButton();
			this._newResearchButton = new System.Windows.Forms.ToolStripButton();
			this._newLinkButton = new System.Windows.Forms.ToolStripButton();
			this._deleteLinkButton = new System.Windows.Forms.ToolStripButton();
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
			this.toolStripButton1 = new System.Windows.Forms.ToolStripButton();
			this.toolStripButton2 = new System.Windows.Forms.ToolStripButton();
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
            this._newLinkButton,
            this._deleteLinkButton,
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
            this.toolStripButton1,
            this.toolStripButton2});
			this._mainToolBar.Name = "_mainToolBar";
			// 
			// _civSelectComboBox
			// 
			this._civSelectComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this._civSelectComboBox.Name = "_civSelectComboBox";
			resources.ApplyResources(this._civSelectComboBox, "_civSelectComboBox");
			this._civSelectComboBox.SelectedIndexChanged += new System.EventHandler(this._civSelectComboBox_SelectedIndexChanged);
			// 
			// _techTreeElementContextMenu
			// 
			this._techTreeElementContextMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.testToolStripMenuItem});
			this._techTreeElementContextMenu.Name = "_techTreeElementContextMenu";
			resources.ApplyResources(this._techTreeElementContextMenu, "_techTreeElementContextMenu");
			// 
			// testToolStripMenuItem
			// 
			this.testToolStripMenuItem.Name = "testToolStripMenuItem";
			resources.ApplyResources(this.testToolStripMenuItem, "testToolStripMenuItem");
			// 
			// _openProjectDialog
			// 
			resources.ApplyResources(this._openProjectDialog, "_openProjectDialog");
			// 
			// _newUnitButton
			// 
			this._newUnitButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this._newUnitButton.Image = global::X2AddOnTechTreeEditor.Icons.NewUnit;
			resources.ApplyResources(this._newUnitButton, "_newUnitButton");
			this._newUnitButton.Name = "_newUnitButton";
			// 
			// _newBuildingButton
			// 
			this._newBuildingButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this._newBuildingButton.Image = global::X2AddOnTechTreeEditor.Icons.NewBuilding;
			resources.ApplyResources(this._newBuildingButton, "_newBuildingButton");
			this._newBuildingButton.Name = "_newBuildingButton";
			// 
			// _newResearchButton
			// 
			this._newResearchButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this._newResearchButton.Image = global::X2AddOnTechTreeEditor.Icons.NewResearch;
			resources.ApplyResources(this._newResearchButton, "_newResearchButton");
			this._newResearchButton.Name = "_newResearchButton";
			// 
			// _newLinkButton
			// 
			this._newLinkButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this._newLinkButton.Image = global::X2AddOnTechTreeEditor.Icons.CreateConnection;
			resources.ApplyResources(this._newLinkButton, "_newLinkButton");
			this._newLinkButton.Name = "_newLinkButton";
			// 
			// _deleteLinkButton
			// 
			this._deleteLinkButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this._deleteLinkButton.Image = global::X2AddOnTechTreeEditor.Icons.DeleteConnection;
			resources.ApplyResources(this._deleteLinkButton, "_deleteLinkButton");
			this._deleteLinkButton.Name = "_deleteLinkButton";
			// 
			// _deleteElementButton
			// 
			this._deleteElementButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this._deleteElementButton.Image = global::X2AddOnTechTreeEditor.Icons.DeleteElement;
			resources.ApplyResources(this._deleteElementButton, "_deleteElementButton");
			this._deleteElementButton.Name = "_deleteElementButton";
			// 
			// _importDATMenuButton
			// 
			this._importDATMenuButton.Image = global::X2AddOnTechTreeEditor.Icons.NewProject;
			this._importDATMenuButton.Name = "_importDATMenuButton";
			resources.ApplyResources(this._importDATMenuButton, "_importDATMenuButton");
			this._importDATMenuButton.Click += new System.EventHandler(this._importDATMenuButton_Click);
			// 
			// _openProjectMenuButton
			// 
			this._openProjectMenuButton.Image = global::X2AddOnTechTreeEditor.Icons.OpenProject;
			this._openProjectMenuButton.Name = "_openProjectMenuButton";
			resources.ApplyResources(this._openProjectMenuButton, "_openProjectMenuButton");
			this._openProjectMenuButton.Click += new System.EventHandler(this._openProjectMenuButton_Click);
			// 
			// _saveProjectMenuButton
			// 
			resources.ApplyResources(this._saveProjectMenuButton, "_saveProjectMenuButton");
			this._saveProjectMenuButton.Image = global::X2AddOnTechTreeEditor.Icons.SaveProject;
			this._saveProjectMenuButton.Name = "_saveProjectMenuButton";
			this._saveProjectMenuButton.Click += new System.EventHandler(this._saveProjectMenuButton_Click);
			// 
			// _exitMenuButton
			// 
			this._exitMenuButton.Image = global::X2AddOnTechTreeEditor.Icons.Exit;
			this._exitMenuButton.Name = "_exitMenuButton";
			resources.ApplyResources(this._exitMenuButton, "_exitMenuButton");
			this._exitMenuButton.Click += new System.EventHandler(this._exitMenuButton_Click);
			// 
			// _undoMenuButton
			// 
			this._undoMenuButton.Image = global::X2AddOnTechTreeEditor.Icons.Undo;
			this._undoMenuButton.Name = "_undoMenuButton";
			resources.ApplyResources(this._undoMenuButton, "_undoMenuButton");
			// 
			// redoMenuButton
			// 
			this.redoMenuButton.Image = global::X2AddOnTechTreeEditor.Icons.Redo;
			this.redoMenuButton.Name = "redoMenuButton";
			resources.ApplyResources(this.redoMenuButton, "redoMenuButton");
			// 
			// _infoMenuButton
			// 
			this._infoMenuButton.Image = global::X2AddOnTechTreeEditor.Icons.Info;
			this._infoMenuButton.Name = "_infoMenuButton";
			resources.ApplyResources(this._infoMenuButton, "_infoMenuButton");
			// 
			// _importDATButton
			// 
			this._importDATButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this._importDATButton.Image = global::X2AddOnTechTreeEditor.Icons.NewProject;
			resources.ApplyResources(this._importDATButton, "_importDATButton");
			this._importDATButton.Name = "_importDATButton";
			this._importDATButton.Click += new System.EventHandler(this._importDATButton_Click);
			// 
			// _openProjectButton
			// 
			this._openProjectButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this._openProjectButton.Image = global::X2AddOnTechTreeEditor.Icons.OpenProject;
			resources.ApplyResources(this._openProjectButton, "_openProjectButton");
			this._openProjectButton.Name = "_openProjectButton";
			this._openProjectButton.Click += new System.EventHandler(this._openProjectButton_Click);
			// 
			// _saveProjectButton
			// 
			this._saveProjectButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			resources.ApplyResources(this._saveProjectButton, "_saveProjectButton");
			this._saveProjectButton.Image = global::X2AddOnTechTreeEditor.Icons.SaveProject;
			this._saveProjectButton.Name = "_saveProjectButton";
			this._saveProjectButton.Click += new System.EventHandler(this._saveProjectButton_Click);
			// 
			// toolStripButton1
			// 
			this.toolStripButton1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.toolStripButton1.Image = global::X2AddOnTechTreeEditor.Icons.EditorMode;
			resources.ApplyResources(this.toolStripButton1, "toolStripButton1");
			this.toolStripButton1.Name = "toolStripButton1";
			// 
			// toolStripButton2
			// 
			this.toolStripButton2.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.toolStripButton2.Image = global::X2AddOnTechTreeEditor.Icons.StandardMode;
			resources.ApplyResources(this.toolStripButton2, "toolStripButton2");
			this.toolStripButton2.Name = "toolStripButton2";
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
		private System.Windows.Forms.ToolStripMenuItem testToolStripMenuItem;
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
		private System.Windows.Forms.ToolStripButton toolStripButton1;
		private System.Windows.Forms.ToolStripButton toolStripButton2;
	}
}

