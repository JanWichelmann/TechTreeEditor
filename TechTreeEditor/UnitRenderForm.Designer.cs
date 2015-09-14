namespace TechTreeEditor
{
	partial class UnitRenderForm
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(UnitRenderForm));
			this._bottomPanel = new System.Windows.Forms.Panel();
			this._closeButton = new System.Windows.Forms.Button();
			this._drawPanel = new OpenTK.GLControl();
			this._topPanel = new System.Windows.Forms.Panel();
			this._graphicsDRSTextBox = new System.Windows.Forms.TextBox();
			this.label1 = new System.Windows.Forms.Label();
			this._openGraphicsDRSDialog = new System.Windows.Forms.OpenFileDialog();
			this._graphicsDRSButton = new System.Windows.Forms.Button();
			this._loadDRSButton = new System.Windows.Forms.Button();
			this._bottomPanel.SuspendLayout();
			this._topPanel.SuspendLayout();
			this.SuspendLayout();
			// 
			// _bottomPanel
			// 
			this._bottomPanel.Controls.Add(this._closeButton);
			this._bottomPanel.Dock = System.Windows.Forms.DockStyle.Bottom;
			this._bottomPanel.Location = new System.Drawing.Point(0, 538);
			this._bottomPanel.Name = "_bottomPanel";
			this._bottomPanel.Size = new System.Drawing.Size(967, 50);
			this._bottomPanel.TabIndex = 1;
			// 
			// _closeButton
			// 
			this._closeButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this._closeButton.Location = new System.Drawing.Point(792, 15);
			this._closeButton.Name = "_closeButton";
			this._closeButton.Size = new System.Drawing.Size(163, 23);
			this._closeButton.TabIndex = 0;
			this._closeButton.Text = "Schließen";
			this._closeButton.UseVisualStyleBackColor = true;
			this._closeButton.Click += new System.EventHandler(this._closeButton_Click);
			// 
			// _drawPanel
			// 
			this._drawPanel.BackColor = System.Drawing.Color.Black;
			this._drawPanel.Dock = System.Windows.Forms.DockStyle.Fill;
			this._drawPanel.Location = new System.Drawing.Point(0, 94);
			this._drawPanel.Name = "_drawPanel";
			this._drawPanel.Size = new System.Drawing.Size(967, 444);
			this._drawPanel.TabIndex = 2;
			this._drawPanel.VSync = false;
			this._drawPanel.Paint += new System.Windows.Forms.PaintEventHandler(this._drawPanel_Paint);
			this._drawPanel.Resize += new System.EventHandler(this._drawPanel_Resize);
			// 
			// _topPanel
			// 
			this._topPanel.Controls.Add(this._loadDRSButton);
			this._topPanel.Controls.Add(this._graphicsDRSButton);
			this._topPanel.Controls.Add(this.label1);
			this._topPanel.Controls.Add(this._graphicsDRSTextBox);
			this._topPanel.Dock = System.Windows.Forms.DockStyle.Top;
			this._topPanel.Location = new System.Drawing.Point(0, 0);
			this._topPanel.Name = "_topPanel";
			this._topPanel.Size = new System.Drawing.Size(967, 94);
			this._topPanel.TabIndex = 2;
			// 
			// _graphicsDRSTextBox
			// 
			this._graphicsDRSTextBox.Location = new System.Drawing.Point(96, 12);
			this._graphicsDRSTextBox.Name = "_graphicsDRSTextBox";
			this._graphicsDRSTextBox.Size = new System.Drawing.Size(314, 20);
			this._graphicsDRSTextBox.TabIndex = 0;
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(12, 15);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(78, 13);
			this.label1.TabIndex = 1;
			this.label1.Text = "Graphics-DRS:";
			// 
			// _openGraphicsDRSDialog
			// 
			this._openGraphicsDRSDialog.Filter = "DRS-Dateien (*.drs)|*.drs";
			this._openGraphicsDRSDialog.Title = "Graphics-DRS-Datei öffnen...";
			// 
			// _graphicsDRSButton
			// 
			this._graphicsDRSButton.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			this._graphicsDRSButton.Location = new System.Drawing.Point(416, 10);
			this._graphicsDRSButton.Name = "_graphicsDRSButton";
			this._graphicsDRSButton.Size = new System.Drawing.Size(31, 23);
			this._graphicsDRSButton.TabIndex = 18;
			this._graphicsDRSButton.Text = "...";
			this._graphicsDRSButton.UseVisualStyleBackColor = true;
			this._graphicsDRSButton.Click += new System.EventHandler(this._graphicsDRSButton_Click);
			// 
			// _loadDRSButton
			// 
			this._loadDRSButton.Location = new System.Drawing.Point(453, 10);
			this._loadDRSButton.Name = "_loadDRSButton";
			this._loadDRSButton.Size = new System.Drawing.Size(75, 23);
			this._loadDRSButton.TabIndex = 19;
			this._loadDRSButton.Text = "Laden!";
			this._loadDRSButton.UseVisualStyleBackColor = true;
			this._loadDRSButton.Click += new System.EventHandler(this._loadDRSButton_Click);
			// 
			// UnitRenderForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(967, 588);
			this.Controls.Add(this._drawPanel);
			this.Controls.Add(this._topPanel);
			this.Controls.Add(this._bottomPanel);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.Name = "UnitRenderForm";
			this.Text = "UnitRenderForm";
			this.Load += new System.EventHandler(this.UnitRenderForm_Load);
			this._bottomPanel.ResumeLayout(false);
			this._topPanel.ResumeLayout(false);
			this._topPanel.PerformLayout();
			this.ResumeLayout(false);

		}

		#endregion
		private System.Windows.Forms.Panel _bottomPanel;
		private System.Windows.Forms.Button _closeButton;
		private OpenTK.GLControl _drawPanel;
		private System.Windows.Forms.Panel _topPanel;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.TextBox _graphicsDRSTextBox;
		private System.Windows.Forms.Button _graphicsDRSButton;
		private System.Windows.Forms.OpenFileDialog _openGraphicsDRSDialog;
		private System.Windows.Forms.Button _loadDRSButton;
	}
}