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
			resources.ApplyResources(this._iconBox, "_iconBox");
			this._iconBox.Image = global::TechTreeEditor.Icons.Symbol64;
			this._iconBox.Name = "_iconBox";
			this._iconBox.TabStop = false;
			// 
			// _titleLabel
			// 
			resources.ApplyResources(this._titleLabel, "_titleLabel");
			this._titleLabel.Name = "_titleLabel";
			// 
			// _versionLabel
			// 
			resources.ApplyResources(this._versionLabel, "_versionLabel");
			this._versionLabel.Name = "_versionLabel";
			// 
			// _copyrightLabel
			// 
			resources.ApplyResources(this._copyrightLabel, "_copyrightLabel");
			this._copyrightLabel.Name = "_copyrightLabel";
			// 
			// _creditsTextBox
			// 
			resources.ApplyResources(this._creditsTextBox, "_creditsTextBox");
			this._creditsTextBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
			this._creditsTextBox.Cursor = System.Windows.Forms.Cursors.Default;
			this._creditsTextBox.Name = "_creditsTextBox";
			this._creditsTextBox.ReadOnly = true;
			this._creditsTextBox.LinkClicked += new System.Windows.Forms.LinkClickedEventHandler(this._creditsTextBox_LinkClicked);
			// 
			// _closeButton
			// 
			resources.ApplyResources(this._closeButton, "_closeButton");
			this._closeButton.Name = "_closeButton";
			this._closeButton.UseVisualStyleBackColor = true;
			this._closeButton.Click += new System.EventHandler(this._closeButton_Click);
			// 
			// AboutForm
			// 
			resources.ApplyResources(this, "$this");
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this._closeButton);
			this.Controls.Add(this._creditsTextBox);
			this.Controls.Add(this._copyrightLabel);
			this.Controls.Add(this._versionLabel);
			this.Controls.Add(this._titleLabel);
			this.Controls.Add(this._iconBox);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
			this.MaximizeBox = false;
			this.Name = "AboutForm";
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