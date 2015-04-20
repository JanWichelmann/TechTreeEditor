namespace X2AddOnTechTreeEditor
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
			this._teamBonusesTab = new System.Windows.Forms.TabPage();
			this._civBonusEffectField = new X2AddOnTechTreeEditor.Controls.TechEffectControl();
			this._teamBonusEffectField = new X2AddOnTechTreeEditor.Controls.TechEffectControl();
			this._bottomPanel.SuspendLayout();
			this._mainPanel.SuspendLayout();
			this._mainTabControl.SuspendLayout();
			this._bonusesTab.SuspendLayout();
			this._teamBonusesTab.SuspendLayout();
			this.SuspendLayout();
			// 
			// _bottomPanel
			// 
			this._bottomPanel.Controls.Add(this._closeButton);
			this._bottomPanel.Dock = System.Windows.Forms.DockStyle.Bottom;
			this._bottomPanel.Location = new System.Drawing.Point(0, 479);
			this._bottomPanel.Name = "_bottomPanel";
			this._bottomPanel.Size = new System.Drawing.Size(872, 45);
			this._bottomPanel.TabIndex = 1;
			// 
			// _closeButton
			// 
			this._closeButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this._closeButton.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			this._closeButton.Location = new System.Drawing.Point(699, 10);
			this._closeButton.Name = "_closeButton";
			this._closeButton.Size = new System.Drawing.Size(161, 23);
			this._closeButton.TabIndex = 3;
			this._closeButton.Text = "Fertig";
			this._closeButton.UseVisualStyleBackColor = true;
			this._closeButton.Click += new System.EventHandler(this._closeButton_Click);
			// 
			// _mainPanel
			// 
			this._mainPanel.Controls.Add(this._mainTabControl);
			this._mainPanel.Dock = System.Windows.Forms.DockStyle.Fill;
			this._mainPanel.Location = new System.Drawing.Point(0, 0);
			this._mainPanel.Name = "_mainPanel";
			this._mainPanel.Size = new System.Drawing.Size(872, 479);
			this._mainPanel.TabIndex = 2;
			// 
			// _mainTabControl
			// 
			this._mainTabControl.Controls.Add(this._bonusesTab);
			this._mainTabControl.Controls.Add(this._teamBonusesTab);
			this._mainTabControl.Dock = System.Windows.Forms.DockStyle.Fill;
			this._mainTabControl.Location = new System.Drawing.Point(0, 0);
			this._mainTabControl.Name = "_mainTabControl";
			this._mainTabControl.SelectedIndex = 0;
			this._mainTabControl.Size = new System.Drawing.Size(872, 479);
			this._mainTabControl.TabIndex = 1;
			// 
			// _bonusesTab
			// 
			this._bonusesTab.Controls.Add(this._civBonusEffectField);
			this._bonusesTab.Location = new System.Drawing.Point(4, 22);
			this._bonusesTab.Name = "_bonusesTab";
			this._bonusesTab.Padding = new System.Windows.Forms.Padding(3);
			this._bonusesTab.Size = new System.Drawing.Size(864, 453);
			this._bonusesTab.TabIndex = 0;
			this._bonusesTab.Text = "Kultur-Boni";
			this._bonusesTab.UseVisualStyleBackColor = true;
			// 
			// _teamBonusesTab
			// 
			this._teamBonusesTab.Controls.Add(this._teamBonusEffectField);
			this._teamBonusesTab.Location = new System.Drawing.Point(4, 22);
			this._teamBonusesTab.Name = "_teamBonusesTab";
			this._teamBonusesTab.Padding = new System.Windows.Forms.Padding(3);
			this._teamBonusesTab.Size = new System.Drawing.Size(864, 453);
			this._teamBonusesTab.TabIndex = 1;
			this._teamBonusesTab.Text = "Team-Boni";
			this._teamBonusesTab.UseVisualStyleBackColor = true;
			// 
			// _civBonusEffectField
			// 
			this._civBonusEffectField.Dock = System.Windows.Forms.DockStyle.Fill;
			this._civBonusEffectField.EffectList = null;
			this._civBonusEffectField.Location = new System.Drawing.Point(3, 3);
			this._civBonusEffectField.Name = "_civBonusEffectField";
			this._civBonusEffectField.ProjectFile = null;
			this._civBonusEffectField.Size = new System.Drawing.Size(858, 447);
			this._civBonusEffectField.TabIndex = 0;
			// 
			// _teamBonusEffectField
			// 
			this._teamBonusEffectField.Dock = System.Windows.Forms.DockStyle.Fill;
			this._teamBonusEffectField.EffectList = null;
			this._teamBonusEffectField.Location = new System.Drawing.Point(3, 3);
			this._teamBonusEffectField.Name = "_teamBonusEffectField";
			this._teamBonusEffectField.ProjectFile = null;
			this._teamBonusEffectField.Size = new System.Drawing.Size(858, 447);
			this._teamBonusEffectField.TabIndex = 0;
			// 
			// EditCivBonuses
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(872, 524);
			this.Controls.Add(this._mainPanel);
			this.Controls.Add(this._bottomPanel);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.Name = "EditCivBonuses";
			this.Text = "Kultur-Boni bearbeiten";
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