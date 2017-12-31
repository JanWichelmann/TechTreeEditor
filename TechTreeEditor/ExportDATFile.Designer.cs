namespace TechTreeEditor
{
	partial class ExportDATFile
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ExportDATFile));
			this.label1 = new System.Windows.Forms.Label();
			this._baseDATButton = new System.Windows.Forms.Button();
			this._openDATDialog = new System.Windows.Forms.OpenFileDialog();
			this._finishButton = new System.Windows.Forms.Button();
			this._cancelButton = new System.Windows.Forms.Button();
			this._finishProgressBar = new System.Windows.Forms.ProgressBar();
			this._saveDATDialog = new System.Windows.Forms.SaveFileDialog();
			this._outputDATButton = new System.Windows.Forms.Button();
			this.label2 = new System.Windows.Forms.Label();
			this._updateProjectIDsCheckBox = new System.Windows.Forms.CheckBox();
			this._exportIdMappingCheckBox = new System.Windows.Forms.CheckBox();
			this._outputDATTextBox = new System.Windows.Forms.TextBox();
			this._baseDATTextBox = new System.Windows.Forms.TextBox();
			this._finishProgressLabel = new System.Windows.Forms.Label();
			this.SuspendLayout();
			// 
			// label1
			// 
			resources.ApplyResources(this.label1, "label1");
			this.label1.Name = "label1";
			// 
			// _baseDATButton
			// 
			resources.ApplyResources(this._baseDATButton, "_baseDATButton");
			this._baseDATButton.Name = "_baseDATButton";
			this._baseDATButton.UseVisualStyleBackColor = true;
			this._baseDATButton.Click += new System.EventHandler(this._baseDATButton_Click);
			// 
			// _openDATDialog
			// 
			resources.ApplyResources(this._openDATDialog, "_openDATDialog");
			// 
			// _finishButton
			// 
			resources.ApplyResources(this._finishButton, "_finishButton");
			this._finishButton.Name = "_finishButton";
			this._finishButton.UseVisualStyleBackColor = true;
			this._finishButton.Click += new System.EventHandler(this._finishButton_Click);
			// 
			// _cancelButton
			// 
			resources.ApplyResources(this._cancelButton, "_cancelButton");
			this._cancelButton.Name = "_cancelButton";
			this._cancelButton.UseVisualStyleBackColor = true;
			this._cancelButton.Click += new System.EventHandler(this._cancelButton_Click);
			// 
			// _finishProgressBar
			// 
			resources.ApplyResources(this._finishProgressBar, "_finishProgressBar");
			this._finishProgressBar.Name = "_finishProgressBar";
			this._finishProgressBar.Step = 1;
			this._finishProgressBar.Style = System.Windows.Forms.ProgressBarStyle.Continuous;
			// 
			// _saveDATDialog
			// 
			resources.ApplyResources(this._saveDATDialog, "_saveDATDialog");
			// 
			// _outputDATButton
			// 
			resources.ApplyResources(this._outputDATButton, "_outputDATButton");
			this._outputDATButton.Name = "_outputDATButton";
			this._outputDATButton.UseVisualStyleBackColor = true;
			this._outputDATButton.Click += new System.EventHandler(this._outputDATButton_Click);
			// 
			// label2
			// 
			resources.ApplyResources(this.label2, "label2");
			this.label2.Name = "label2";
			// 
			// _updateProjectIDsCheckBox
			// 
			resources.ApplyResources(this._updateProjectIDsCheckBox, "_updateProjectIDsCheckBox");
			this._updateProjectIDsCheckBox.Name = "_updateProjectIDsCheckBox";
			this._updateProjectIDsCheckBox.UseVisualStyleBackColor = true;
			// 
			// _exportIdMappingCheckBox
			// 
			resources.ApplyResources(this._exportIdMappingCheckBox, "_exportIdMappingCheckBox");
			this._exportIdMappingCheckBox.Checked = global::TechTreeEditor.Properties.Settings.Default.ExportDATFileExportIDMapping;
			this._exportIdMappingCheckBox.DataBindings.Add(new System.Windows.Forms.Binding("Checked", global::TechTreeEditor.Properties.Settings.Default, "ExportDATFileExportIDMapping", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
			this._exportIdMappingCheckBox.Name = "_exportIdMappingCheckBox";
			this._exportIdMappingCheckBox.UseVisualStyleBackColor = true;
			// 
			// _outputDATTextBox
			// 
			resources.ApplyResources(this._outputDATTextBox, "_outputDATTextBox");
			this._outputDATTextBox.DataBindings.Add(new System.Windows.Forms.Binding("Text", global::TechTreeEditor.Properties.Settings.Default, "ExportDATFileOutputDATPath", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
			this._outputDATTextBox.Name = "_outputDATTextBox";
			this._outputDATTextBox.Text = global::TechTreeEditor.Properties.Settings.Default.ExportDATFileOutputDATPath;
			// 
			// _baseDATTextBox
			// 
			resources.ApplyResources(this._baseDATTextBox, "_baseDATTextBox");
			this._baseDATTextBox.DataBindings.Add(new System.Windows.Forms.Binding("Text", global::TechTreeEditor.Properties.Settings.Default, "ExportDATFileBaseDATPath", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
			this._baseDATTextBox.Name = "_baseDATTextBox";
			this._baseDATTextBox.Text = global::TechTreeEditor.Properties.Settings.Default.ExportDATFileBaseDATPath;
			// 
			// _finishProgressLabel
			// 
			resources.ApplyResources(this._finishProgressLabel, "_finishProgressLabel");
			this._finishProgressLabel.AutoEllipsis = true;
			this._finishProgressLabel.Name = "_finishProgressLabel";
			// 
			// ExportDATFile
			// 
			resources.ApplyResources(this, "$this");
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this._finishProgressLabel);
			this.Controls.Add(this._exportIdMappingCheckBox);
			this.Controls.Add(this._updateProjectIDsCheckBox);
			this.Controls.Add(this._outputDATButton);
			this.Controls.Add(this._outputDATTextBox);
			this.Controls.Add(this.label2);
			this.Controls.Add(this._finishProgressBar);
			this.Controls.Add(this._cancelButton);
			this.Controls.Add(this._finishButton);
			this.Controls.Add(this._baseDATButton);
			this.Controls.Add(this._baseDATTextBox);
			this.Controls.Add(this.label1);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.MaximizeBox = false;
			this.Name = "ExportDATFile";
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.TextBox _baseDATTextBox;
		private System.Windows.Forms.Button _baseDATButton;
		private System.Windows.Forms.OpenFileDialog _openDATDialog;
		private System.Windows.Forms.Button _finishButton;
		private System.Windows.Forms.Button _cancelButton;
		private System.Windows.Forms.SaveFileDialog _saveDATDialog;
		private System.Windows.Forms.ProgressBar _finishProgressBar;
		private System.Windows.Forms.Button _outputDATButton;
		private System.Windows.Forms.TextBox _outputDATTextBox;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.CheckBox _updateProjectIDsCheckBox;
		private System.Windows.Forms.CheckBox _exportIdMappingCheckBox;
		private System.Windows.Forms.Label _finishProgressLabel;
	}
}