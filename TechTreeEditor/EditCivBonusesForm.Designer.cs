namespace TechTreeEditor
{
	partial class EditCivBonusesForm
	{
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing)
		{
			if(disposing && (components != null))
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Windows Form Designer generated code

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(EditCivBonusesForm));
			this._bottomPanel = new System.Windows.Forms.Panel();
			this._closeButton = new System.Windows.Forms.Button();
			this._mainPanel = new System.Windows.Forms.Panel();
			this._mainTabControl = new System.Windows.Forms.TabControl();
			this._bonusesTab = new System.Windows.Forms.TabPage();
			this._civBonusEffectField = new TechTreeEditor.Controls.TechEffectControl();
			this._teamBonusesTab = new System.Windows.Forms.TabPage();
			this._teamBonusEffectField = new TechTreeEditor.Controls.TechEffectControl();
			this._bottomPanel.SuspendLayout();
			this._mainPanel.SuspendLayout();
			this._mainTabControl.SuspendLayout();
			this._bonusesTab.SuspendLayout();
			this._teamBonusesTab.SuspendLayout();
			this.SuspendLayout();
			// 
			// _bottomPanel
			// 
			resources.ApplyResources(this._bottomPanel, "_bottomPanel");
			this._bottomPanel.Controls.Add(this._closeButton);
			this._bottomPanel.Name = "_bottomPanel";
			// 
			// _closeButton
			// 
			resources.ApplyResources(this._closeButton, "_closeButton");
			this._closeButton.Name = "_closeButton";
			this._closeButton.UseVisualStyleBackColor = true;
			this._closeButton.Click += new System.EventHandler(this._closeButton_Click);
			// 
			// _mainPanel
			// 
			resources.ApplyResources(this._mainPanel, "_mainPanel");
			this._mainPanel.Controls.Add(this._mainTabControl);
			this._mainPanel.Name = "_mainPanel";
			// 
			// _mainTabControl
			// 
			resources.ApplyResources(this._mainTabControl, "_mainTabControl");
			this._mainTabControl.Controls.Add(this._bonusesTab);
			this._mainTabControl.Controls.Add(this._teamBonusesTab);
			this._mainTabControl.Name = "_mainTabControl";
			this._mainTabControl.SelectedIndex = 0;
			// 
			// _bonusesTab
			// 
			resources.ApplyResources(this._bonusesTab, "_bonusesTab");
			this._bonusesTab.Controls.Add(this._civBonusEffectField);
			this._bonusesTab.Name = "_bonusesTab";
			this._bonusesTab.UseVisualStyleBackColor = true;
			// 
			// _civBonusEffectField
			// 
			resources.ApplyResources(this._civBonusEffectField, "_civBonusEffectField");
			this._civBonusEffectField.EffectList = null;
			this._civBonusEffectField.Name = "_civBonusEffectField";
			this._civBonusEffectField.ProjectFile = null;
			// 
			// _teamBonusesTab
			// 
			resources.ApplyResources(this._teamBonusesTab, "_teamBonusesTab");
			this._teamBonusesTab.Controls.Add(this._teamBonusEffectField);
			this._teamBonusesTab.Name = "_teamBonusesTab";
			this._teamBonusesTab.UseVisualStyleBackColor = true;
			// 
			// _teamBonusEffectField
			// 
			resources.ApplyResources(this._teamBonusEffectField, "_teamBonusEffectField");
			this._teamBonusEffectField.EffectList = null;
			this._teamBonusEffectField.Name = "_teamBonusEffectField";
			this._teamBonusEffectField.ProjectFile = null;
			// 
			// EditCivBonusesForm
			// 
			resources.ApplyResources(this, "$this");
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this._mainPanel);
			this.Controls.Add(this._bottomPanel);
			this.Name = "EditCivBonusesForm";
			this._bottomPanel.ResumeLayout(false);
			this._mainPanel.ResumeLayout(false);
			this._mainTabControl.ResumeLayout(false);
			this._bonusesTab.ResumeLayout(false);
			this._teamBonusesTab.ResumeLayout(false);
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.Panel _bottomPanel;
		private System.Windows.Forms.Button _closeButton;
		private System.Windows.Forms.Panel _mainPanel;
		private Controls.TechEffectControl _civBonusEffectField;
		private System.Windows.Forms.TabControl _mainTabControl;
		private System.Windows.Forms.TabPage _bonusesTab;
		private System.Windows.Forms.TabPage _teamBonusesTab;
		private Controls.TechEffectControl _teamBonusEffectField;
	}
}