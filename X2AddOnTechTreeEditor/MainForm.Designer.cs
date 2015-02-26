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
			this._mainMenu = new System.Windows.Forms.MenuStrip();
			this._fileMenuButton = new System.Windows.Forms.ToolStripMenuItem();
			this._openProjectMenuButton = new System.Windows.Forms.ToolStripMenuItem();
			this._saveProjectMenuButton = new System.Windows.Forms.ToolStripMenuItem();
			this._importDATMenuButton = new System.Windows.Forms.ToolStripMenuItem();
			this._exitMenuButton = new System.Windows.Forms.ToolStripMenuItem();
			this._mainToolBar = new System.Windows.Forms.ToolStrip();
			this._civSelectComboBox = new System.Windows.Forms.ToolStripComboBox();
			this._techTreeElementContextMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
			this.testToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this._openProjectDialog = new System.Windows.Forms.OpenFileDialog();
			this._renderPanel = new X2AddOnTechTreeEditor.RenderControl();
			this._menuContainer.BottomToolStripPanel.SuspendLayout();
			this._menuContainer.ContentPanel.SuspendLayout();
			this._menuContainer.TopToolStripPanel.SuspendLayout();
			this._menuContainer.SuspendLayout();
			this._statusStrip.SuspendLayout();
			this._mainMenu.SuspendLayout();
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
			this._menuContainer.Name = "_menuContainer";
			// 
			// _menuContainer.TopToolStripPanel
			// 
			this._menuContainer.TopToolStripPanel.Controls.Add(this._mainMenu);
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
			// _mainMenu
			// 
			resources.ApplyResources(this._mainMenu, "_mainMenu");
			this._mainMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this._fileMenuButton});
			this._mainMenu.Name = "_mainMenu";
			// 
			// _fileMenuButton
			// 
			this._fileMenuButton.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this._openProjectMenuButton,
            this._saveProjectMenuButton,
            this._importDATMenuButton,
            this._exitMenuButton});
			this._fileMenuButton.Name = "_fileMenuButton";
			resources.ApplyResources(this._fileMenuButton, "_fileMenuButton");
			// 
			// _openProjectMenuButton
			// 
			this._openProjectMenuButton.Name = "_openProjectMenuButton";
			resources.ApplyResources(this._openProjectMenuButton, "_openProjectMenuButton");
			this._openProjectMenuButton.Click += new System.EventHandler(this._openProjectMenuButton_Click);
			// 
			// _saveProjectMenuButton
			// 
			resources.ApplyResources(this._saveProjectMenuButton, "_saveProjectMenuButton");
			this._saveProjectMenuButton.Name = "_saveProjectMenuButton";
			this._saveProjectMenuButton.Click += new System.EventHandler(this._saveProjectMenuButton_Click);
			// 
			// _importDATMenuButton
			// 
			this._importDATMenuButton.Name = "_importDATMenuButton";
			resources.ApplyResources(this._importDATMenuButton, "_importDATMenuButton");
			this._importDATMenuButton.Click += new System.EventHandler(this._importDATMenuButton_Click);
			// 
			// _exitMenuButton
			// 
			this._exitMenuButton.Name = "_exitMenuButton";
			resources.ApplyResources(this._exitMenuButton, "_exitMenuButton");
			this._exitMenuButton.Click += new System.EventHandler(this._exitMenuButton_Click);
			// 
			// _mainToolBar
			// 
			resources.ApplyResources(this._mainToolBar, "_mainToolBar");
			this._mainToolBar.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this._civSelectComboBox});
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
			this._menuContainer.TopToolStripPanel.ResumeLayout(false);
			this._menuContainer.TopToolStripPanel.PerformLayout();
			this._menuContainer.ResumeLayout(false);
			this._menuContainer.PerformLayout();
			this._statusStrip.ResumeLayout(false);
			this._statusStrip.PerformLayout();
			this._mainMenu.ResumeLayout(false);
			this._mainMenu.PerformLayout();
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
	}
}

