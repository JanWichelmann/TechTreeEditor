namespace TechTreeEditor
{
	partial class ProjectSettingsForm
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ProjectSettingsForm));
			this._cancelButton = new System.Windows.Forms.Button();
			this._okButton = new System.Windows.Forms.Button();
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this._graphicsDRSButton = new System.Windows.Forms.Button();
			this.label1 = new System.Windows.Forms.Label();
			this._graphicsDRSTextBox = new System.Windows.Forms.TextBox();
			this._dllButton = new System.Windows.Forms.Button();
			this.label3 = new System.Windows.Forms.Label();
			this._dllTextBox = new System.Windows.Forms.TextBox();
			this._interfacDRSButton = new System.Windows.Forms.Button();
			this.label4 = new System.Windows.Forms.Label();
			this._interfacDRSTextBox = new System.Windows.Forms.TextBox();
			this.label2 = new System.Windows.Forms.Label();
			this.label5 = new System.Windows.Forms.Label();
			this._ageCountField = new System.Windows.Forms.NumericUpDown();
			this._openDLLDialog = new System.Windows.Forms.OpenFileDialog();
			this._openInterfacDRSDialog = new System.Windows.Forms.OpenFileDialog();
			this._openGraphicsDRSDialog = new System.Windows.Forms.OpenFileDialog();
			this._useNewTechTreeCheckBox = new System.Windows.Forms.CheckBox();
			this.groupBox1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this._ageCountField)).BeginInit();
			this.SuspendLayout();
			// 
			// _cancelButton
			// 
			resources.ApplyResources(this._cancelButton, "_cancelButton");
			this._cancelButton.Name = "_cancelButton";
			this._cancelButton.UseVisualStyleBackColor = true;
			this._cancelButton.Click += new System.EventHandler(this._cancelButton_Click);
			// 
			// _okButton
			// 
			resources.ApplyResources(this._okButton, "_okButton");
			this._okButton.Name = "_okButton";
			this._okButton.UseVisualStyleBackColor = true;
			this._okButton.Click += new System.EventHandler(this._okButton_Click);
			// 
			// groupBox1
			// 
			resources.ApplyResources(this.groupBox1, "groupBox1");
			this.groupBox1.Controls.Add(this._graphicsDRSButton);
			this.groupBox1.Controls.Add(this.label1);
			this.groupBox1.Controls.Add(this._graphicsDRSTextBox);
			this.groupBox1.Controls.Add(this._dllButton);
			this.groupBox1.Controls.Add(this.label3);
			this.groupBox1.Controls.Add(this._dllTextBox);
			this.groupBox1.Controls.Add(this._interfacDRSButton);
			this.groupBox1.Controls.Add(this.label4);
			this.groupBox1.Controls.Add(this._interfacDRSTextBox);
			this.groupBox1.Controls.Add(this.label2);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.TabStop = false;
			// 
			// _graphicsDRSButton
			// 
			resources.ApplyResources(this._graphicsDRSButton, "_graphicsDRSButton");
			this._graphicsDRSButton.Name = "_graphicsDRSButton";
			this._graphicsDRSButton.UseVisualStyleBackColor = true;
			this._graphicsDRSButton.Click += new System.EventHandler(this._graphicsDRSButton_Click);
			// 
			// label1
			// 
			resources.ApplyResources(this.label1, "label1");
			this.label1.Name = "label1";
			// 
			// _graphicsDRSTextBox
			// 
			resources.ApplyResources(this._graphicsDRSTextBox, "_graphicsDRSTextBox");
			this._graphicsDRSTextBox.Name = "_graphicsDRSTextBox";
			// 
			// _dllButton
			// 
			resources.ApplyResources(this._dllButton, "_dllButton");
			this._dllButton.Name = "_dllButton";
			this._dllButton.UseVisualStyleBackColor = true;
			this._dllButton.Click += new System.EventHandler(this._dllButton_Click);
			// 
			// label3
			// 
			resources.ApplyResources(this.label3, "label3");
			this.label3.Name = "label3";
			// 
			// _dllTextBox
			// 
			resources.ApplyResources(this._dllTextBox, "_dllTextBox");
			this._dllTextBox.Name = "_dllTextBox";
			// 
			// _interfacDRSButton
			// 
			resources.ApplyResources(this._interfacDRSButton, "_interfacDRSButton");
			this._interfacDRSButton.Name = "_interfacDRSButton";
			this._interfacDRSButton.UseVisualStyleBackColor = true;
			this._interfacDRSButton.Click += new System.EventHandler(this._interfacDRSButton_Click);
			// 
			// label4
			// 
			resources.ApplyResources(this.label4, "label4");
			this.label4.Name = "label4";
			// 
			// _interfacDRSTextBox
			// 
			resources.ApplyResources(this._interfacDRSTextBox, "_interfacDRSTextBox");
			this._interfacDRSTextBox.Name = "_interfacDRSTextBox";
			// 
			// label2
			// 
			resources.ApplyResources(this.label2, "label2");
			this.label2.Name = "label2";
			// 
			// label5
			// 
			resources.ApplyResources(this.label5, "label5");
			this.label5.Name = "label5";
			// 
			// _ageCountField
			// 
			resources.ApplyResources(this._ageCountField, "_ageCountField");
			this._ageCountField.Maximum = new decimal(new int[] {
            10,
            0,
            0,
            0});
			this._ageCountField.Minimum = new decimal(new int[] {
            2,
            0,
            0,
            0});
			this._ageCountField.Name = "_ageCountField";
			this._ageCountField.Value = new decimal(new int[] {
            10,
            0,
            0,
            0});
			// 
			// _openDLLDialog
			// 
			resources.ApplyResources(this._openDLLDialog, "_openDLLDialog");
			this._openDLLDialog.Multiselect = true;
			// 
			// _openInterfacDRSDialog
			// 
			resources.ApplyResources(this._openInterfacDRSDialog, "_openInterfacDRSDialog");
			// 
			// _openGraphicsDRSDialog
			// 
			resources.ApplyResources(this._openGraphicsDRSDialog, "_openGraphicsDRSDialog");
			// 
			// _useNewTechTreeCheckBox
			// 
			resources.ApplyResources(this._useNewTechTreeCheckBox, "_useNewTechTreeCheckBox");
			this._useNewTechTreeCheckBox.Name = "_useNewTechTreeCheckBox";
			this._useNewTechTreeCheckBox.UseVisualStyleBackColor = true;
			// 
			// ProjectSettingsForm
			// 
			resources.ApplyResources(this, "$this");
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this._useNewTechTreeCheckBox);
			this.Controls.Add(this.label5);
			this.Controls.Add(this.groupBox1);
			this.Controls.Add(this._cancelButton);
			this.Controls.Add(this._okButton);
			this.Controls.Add(this._ageCountField);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
			this.MaximizeBox = false;
			this.Name = "ProjectSettingsForm";
			this.groupBox1.ResumeLayout(false);
			this.groupBox1.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this._ageCountField)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Button _cancelButton;
		private System.Windows.Forms.Button _okButton;
		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Button _dllButton;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.TextBox _dllTextBox;
		private System.Windows.Forms.Button _interfacDRSButton;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.TextBox _interfacDRSTextBox;
		private System.Windows.Forms.OpenFileDialog _openDLLDialog;
		private System.Windows.Forms.OpenFileDialog _openInterfacDRSDialog;
		private System.Windows.Forms.Button _graphicsDRSButton;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.TextBox _graphicsDRSTextBox;
		private System.Windows.Forms.OpenFileDialog _openGraphicsDRSDialog;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.NumericUpDown _ageCountField;
		private System.Windows.Forms.CheckBox _useNewTechTreeCheckBox;
	}
}