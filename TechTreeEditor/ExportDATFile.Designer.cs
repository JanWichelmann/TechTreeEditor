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
			this._datTextBox = new System.Windows.Forms.TextBox();
			this._datButton = new System.Windows.Forms.Button();
			this._openDATDialog = new System.Windows.Forms.OpenFileDialog();
			this._finishButton = new System.Windows.Forms.Button();
			this._cancelButton = new System.Windows.Forms.Button();
			this._finishProgressBar = new System.Windows.Forms.ProgressBar();
			this._saveDATDialog = new System.Windows.Forms.SaveFileDialog();
			this.SuspendLayout();
			// 
			// label1
			// 
			resources.ApplyResources(this.label1, "label1");
			this.label1.Name = "label1";
			// 
			// _datTextBox
			// 
			resources.ApplyResources(this._datTextBox, "_datTextBox");
			this._datTextBox.Name = "_datTextBox";
			// 
			// _datButton
			// 
			resources.ApplyResources(this._datButton, "_datButton");
			this._datButton.Name = "_datButton";
			this._datButton.UseVisualStyleBackColor = true;
			this._datButton.Click += new System.EventHandler(this._datButton_Click);
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
			this._finishProgressBar.Style = System.Windows.Forms.ProgressBarStyle.Marquee;
			// 
			// _saveDATDialog
			// 
			resources.ApplyResources(this._saveDATDialog, "_saveDATDialog");
			// 
			// ExportDATFile
			// 
			resources.ApplyResources(this, "$this");
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this._finishProgressBar);
			this.Controls.Add(this._cancelButton);
			this.Controls.Add(this._finishButton);
			this.Controls.Add(this._datButton);
			this.Controls.Add(this._datTextBox);
			this.Controls.Add(this.label1);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.MaximizeBox = false;
			this.Name = "ExportDATFile";
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.TextBox _datTextBox;
		private System.Windows.Forms.Button _datButton;
		private System.Windows.Forms.OpenFileDialog _openDATDialog;
		private System.Windows.Forms.Button _finishButton;
		private System.Windows.Forms.Button _cancelButton;
		private System.Windows.Forms.SaveFileDialog _saveDATDialog;
		private System.Windows.Forms.ProgressBar _finishProgressBar;
	}
}