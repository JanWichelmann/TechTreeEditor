namespace TechTreeEditor
{
	partial class EditProjectileForm
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(EditProjectileForm));
			this._otherUnitsGroupBox = new System.Windows.Forms.GroupBox();
			this.label1 = new System.Windows.Forms.Label();
			this._deadUnitComboBox = new System.Windows.Forms.ComboBox();
			this.label3 = new System.Windows.Forms.Label();
			this._trackUnitComboBox = new System.Windows.Forms.ComboBox();
			this._closeButton = new System.Windows.Forms.Button();
			this._otherUnitsGroupBox.SuspendLayout();
			this.SuspendLayout();
			// 
			// _otherUnitsGroupBox
			// 
			resources.ApplyResources(this._otherUnitsGroupBox, "_otherUnitsGroupBox");
			this._otherUnitsGroupBox.Controls.Add(this.label1);
			this._otherUnitsGroupBox.Controls.Add(this._deadUnitComboBox);
			this._otherUnitsGroupBox.Controls.Add(this.label3);
			this._otherUnitsGroupBox.Controls.Add(this._trackUnitComboBox);
			this._otherUnitsGroupBox.Name = "_otherUnitsGroupBox";
			this._otherUnitsGroupBox.TabStop = false;
			// 
			// label1
			// 
			resources.ApplyResources(this.label1, "label1");
			this.label1.Name = "label1";
			// 
			// _deadUnitComboBox
			// 
			resources.ApplyResources(this._deadUnitComboBox, "_deadUnitComboBox");
			this._deadUnitComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this._deadUnitComboBox.FormattingEnabled = true;
			this._deadUnitComboBox.Name = "_deadUnitComboBox";
			this._deadUnitComboBox.Sorted = true;
			this._deadUnitComboBox.SelectedIndexChanged += new System.EventHandler(this._deadUnitComboBox_SelectedIndexChanged);
			// 
			// label3
			// 
			resources.ApplyResources(this.label3, "label3");
			this.label3.Name = "label3";
			// 
			// _trackUnitComboBox
			// 
			resources.ApplyResources(this._trackUnitComboBox, "_trackUnitComboBox");
			this._trackUnitComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this._trackUnitComboBox.FormattingEnabled = true;
			this._trackUnitComboBox.Name = "_trackUnitComboBox";
			this._trackUnitComboBox.Sorted = true;
			this._trackUnitComboBox.SelectedIndexChanged += new System.EventHandler(this._trackUnitComboBox_SelectedIndexChanged);
			// 
			// _closeButton
			// 
			resources.ApplyResources(this._closeButton, "_closeButton");
			this._closeButton.Name = "_closeButton";
			this._closeButton.UseVisualStyleBackColor = true;
			this._closeButton.Click += new System.EventHandler(this._closeButton_Click);
			// 
			// EditProjectileForm
			// 
			resources.ApplyResources(this, "$this");
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this._closeButton);
			this.Controls.Add(this._otherUnitsGroupBox);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
			this.MaximizeBox = false;
			this.Name = "EditProjectileForm";
			this._otherUnitsGroupBox.ResumeLayout(false);
			this._otherUnitsGroupBox.PerformLayout();
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.GroupBox _otherUnitsGroupBox;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.ComboBox _trackUnitComboBox;
		private System.Windows.Forms.Button _closeButton;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.ComboBox _deadUnitComboBox;
	}
}