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
			this.label2 = new System.Windows.Forms.Label();
			this._dllButton = new System.Windows.Forms.Button();
			this.label3 = new System.Windows.Forms.Label();
			this._dllTextBox = new System.Windows.Forms.TextBox();
			this._openDLLDialog = new System.Windows.Forms.OpenFileDialog();
			this._openInterfacDRSDialog = new System.Windows.Forms.OpenFileDialog();
			this._interfacDRSButton = new System.Windows.Forms.Button();
			this.label4 = new System.Windows.Forms.Label();
			this._interfacDRSTextBox = new System.Windows.Forms.TextBox();
			this.groupBox1.SuspendLayout();
			this.SuspendLayout();
			// 
			// _cancelButton
			// 
			this._cancelButton.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			this._cancelButton.Location = new System.Drawing.Point(447, 111);
			this._cancelButton.Name = "_cancelButton";
			this._cancelButton.Size = new System.Drawing.Size(120, 23);
			this._cancelButton.TabIndex = 9;
			this._cancelButton.Text = "Abbrechen";
			this._cancelButton.UseVisualStyleBackColor = true;
			this._cancelButton.Click += new System.EventHandler(this._cancelButton_Click);
			// 
			// _okButton
			// 
			this._okButton.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			this._okButton.Location = new System.Drawing.Point(322, 111);
			this._okButton.Name = "_okButton";
			this._okButton.Size = new System.Drawing.Size(119, 23);
			this._okButton.TabIndex = 8;
			this._okButton.Text = "OK";
			this._okButton.UseVisualStyleBackColor = true;
			this._okButton.Click += new System.EventHandler(this._okButton_Click);
			// 
			// groupBox1
			// 
			this.groupBox1.Controls.Add(this._dllButton);
			this.groupBox1.Controls.Add(this.label3);
			this.groupBox1.Controls.Add(this._dllTextBox);
			this.groupBox1.Controls.Add(this._interfacDRSButton);
			this.groupBox1.Controls.Add(this.label4);
			this.groupBox1.Controls.Add(this._interfacDRSTextBox);
			this.groupBox1.Controls.Add(this.label2);
			this.groupBox1.Location = new System.Drawing.Point(12, 12);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(555, 93);
			this.groupBox1.TabIndex = 12;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "Pfade";
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(5, 16);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(160, 13);
			this.label2.TabIndex = 0;
			this.label2.Text = "Auch relative Pfade sind erlaubt.";
			// 
			// _dllButton
			// 
			this._dllButton.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			this._dllButton.Location = new System.Drawing.Point(516, 35);
			this._dllButton.Name = "_dllButton";
			this._dllButton.Size = new System.Drawing.Size(31, 23);
			this._dllButton.TabIndex = 17;
			this._dllButton.Text = "...";
			this._dllButton.UseVisualStyleBackColor = true;
			this._dllButton.Click += new System.EventHandler(this._dllButton_Click);
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			this.label3.Location = new System.Drawing.Point(5, 40);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(86, 13);
			this.label3.TabIndex = 16;
			this.label3.Text = "Language-DLLs:";
			// 
			// _dllTextBox
			// 
			this._dllTextBox.Location = new System.Drawing.Point(150, 37);
			this._dllTextBox.Name = "_dllTextBox";
			this._dllTextBox.Size = new System.Drawing.Size(360, 20);
			this._dllTextBox.TabIndex = 15;
			// 
			// _openDLLDialog
			// 
			this._openDLLDialog.Filter = "Age of Empires II String-Dateien (*.dll)|*.dll";
			this._openDLLDialog.Multiselect = true;
			this._openDLLDialog.Title = "Language-DLL-Dateien laden...";
			// 
			// _openInterfacDRSDialog
			// 
			this._openInterfacDRSDialog.Filter = "DRS-Dateien (*.drs)|*.drs";
			this._openInterfacDRSDialog.Title = "Interfac-DRS-Datei öffnen...";
			// 
			// _interfacDRSButton
			// 
			this._interfacDRSButton.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			this._interfacDRSButton.Location = new System.Drawing.Point(516, 61);
			this._interfacDRSButton.Name = "_interfacDRSButton";
			this._interfacDRSButton.Size = new System.Drawing.Size(31, 23);
			this._interfacDRSButton.TabIndex = 20;
			this._interfacDRSButton.Text = "...";
			this._interfacDRSButton.UseVisualStyleBackColor = true;
			this._interfacDRSButton.Click += new System.EventHandler(this._interfacDRSButton_Click);
			// 
			// label4
			// 
			this.label4.AutoSize = true;
			this.label4.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			this.label4.Location = new System.Drawing.Point(5, 66);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(72, 13);
			this.label4.TabIndex = 19;
			this.label4.Text = "Interfac-DRS:";
			// 
			// _interfacDRSTextBox
			// 
			this._interfacDRSTextBox.Location = new System.Drawing.Point(150, 63);
			this._interfacDRSTextBox.Name = "_interfacDRSTextBox";
			this._interfacDRSTextBox.Size = new System.Drawing.Size(360, 20);
			this._interfacDRSTextBox.TabIndex = 18;
			// 
			// ProjectSettingsForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(574, 140);
			this.Controls.Add(this.groupBox1);
			this.Controls.Add(this._cancelButton);
			this.Controls.Add(this._okButton);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MaximizeBox = false;
			this.Name = "ProjectSettingsForm";
			this.Text = "Projekteinstellungen";
			this.groupBox1.ResumeLayout(false);
			this.groupBox1.PerformLayout();
			this.ResumeLayout(false);

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
	}
}