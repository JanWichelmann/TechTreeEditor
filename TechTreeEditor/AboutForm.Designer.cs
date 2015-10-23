namespace TechTreeEditor
{
	partial class AboutForm
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AboutForm));
			this._iconBox = new System.Windows.Forms.PictureBox();
			this._titleLabel = new System.Windows.Forms.Label();
			this._versionLabel = new System.Windows.Forms.Label();
			this._copyrightLabel = new System.Windows.Forms.Label();
			this._creditsTextBox = new System.Windows.Forms.RichTextBox();
			this._closeButton = new System.Windows.Forms.Button();
			((System.ComponentModel.ISupportInitialize)(this._iconBox)).BeginInit();
			this.SuspendLayout();
			// 
			// _iconBox
			// 
			this._iconBox.Image = global::TechTreeEditor.Icons.Symbol64;
			this._iconBox.Location = new System.Drawing.Point(12, 12);
			this._iconBox.Name = "_iconBox";
			this._iconBox.Size = new System.Drawing.Size(64, 64);
			this._iconBox.TabIndex = 0;
			this._iconBox.TabStop = false;
			// 
			// _titleLabel
			// 
			this._titleLabel.AutoSize = true;
			this._titleLabel.Font = new System.Drawing.Font("Arial", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this._titleLabel.Location = new System.Drawing.Point(82, 12);
			this._titleLabel.Name = "_titleLabel";
			this._titleLabel.Size = new System.Drawing.Size(154, 22);
			this._titleLabel.TabIndex = 1;
			this._titleLabel.Text = "TechTreeEditor";
			// 
			// _versionLabel
			// 
			this._versionLabel.AutoSize = true;
			this._versionLabel.Location = new System.Drawing.Point(85, 38);
			this._versionLabel.Name = "_versionLabel";
			this._versionLabel.Size = new System.Drawing.Size(78, 13);
			this._versionLabel.TabIndex = 2;
			this._versionLabel.Text = "Version 1.2.3-4";
			// 
			// _copyrightLabel
			// 
			this._copyrightLabel.AutoSize = true;
			this._copyrightLabel.Location = new System.Drawing.Point(85, 63);
			this._copyrightLabel.Name = "_copyrightLabel";
			this._copyrightLabel.Size = new System.Drawing.Size(330, 13);
			this._copyrightLabel.TabIndex = 3;
			this._copyrightLabel.Text = "Software von Jan Wichelmann mit Icons von Sonja Jäckle, (c) 2015.";
			// 
			// _creditsTextBox
			// 
			this._creditsTextBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
			this._creditsTextBox.BulletIndent = 4;
			this._creditsTextBox.Cursor = System.Windows.Forms.Cursors.Default;
			this._creditsTextBox.Location = new System.Drawing.Point(12, 95);
			this._creditsTextBox.Name = "_creditsTextBox";
			this._creditsTextBox.ReadOnly = true;
			this._creditsTextBox.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.Vertical;
			this._creditsTextBox.Size = new System.Drawing.Size(728, 326);
			this._creditsTextBox.TabIndex = 4;
			this._creditsTextBox.Text = "blub";
			this._creditsTextBox.LinkClicked += new System.Windows.Forms.LinkClickedEventHandler(this._creditsTextBox_LinkClicked);
			// 
			// _closeButton
			// 
			this._closeButton.Location = new System.Drawing.Point(594, 427);
			this._closeButton.Name = "_closeButton";
			this._closeButton.Size = new System.Drawing.Size(146, 23);
			this._closeButton.TabIndex = 5;
			this._closeButton.Text = "Schließen";
			this._closeButton.UseVisualStyleBackColor = true;
			this._closeButton.Click += new System.EventHandler(this._closeButton_Click);
			// 
			// AboutForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(752, 462);
			this.Controls.Add(this._closeButton);
			this.Controls.Add(this._creditsTextBox);
			this.Controls.Add(this._copyrightLabel);
			this.Controls.Add(this._versionLabel);
			this.Controls.Add(this._titleLabel);
			this.Controls.Add(this._iconBox);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MaximizeBox = false;
			this.Name = "AboutForm";
			this.Text = "Informationen zum TechTreeEditor";
			((System.ComponentModel.ISupportInitialize)(this._iconBox)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.PictureBox _iconBox;
		private System.Windows.Forms.Label _titleLabel;
		private System.Windows.Forms.Label _versionLabel;
		private System.Windows.Forms.Label _copyrightLabel;
		private System.Windows.Forms.RichTextBox _creditsTextBox;
		private System.Windows.Forms.Button _closeButton;
	}
}