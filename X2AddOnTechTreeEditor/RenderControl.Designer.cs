namespace X2AddOnTechTreeEditor
{
	partial class RenderControl
	{
		/// <summary> 
		/// Erforderliche Designervariable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary> 
		/// Verwendete Ressourcen bereinigen.
		/// </summary>
		/// <param name="disposing">True, wenn verwaltete Ressourcen gelöscht werden sollen; andernfalls False.</param>
		protected override void Dispose(bool disposing)
		{
			if(disposing && (components != null))
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Vom Komponenten-Designer generierter Code

		/// <summary> 
		/// Erforderliche Methode für die Designerunterstützung. 
		/// Der Inhalt der Methode darf nicht mit dem Code-Editor geändert werden.
		/// </summary>
		private void InitializeComponent()
		{
			this._drawPanel = new OpenTK.GLControl();
			this._drawPanelScrollBar = new System.Windows.Forms.HScrollBar();
			this.SuspendLayout();
			// 
			// _drawPanel
			// 
			this._drawPanel.BackColor = System.Drawing.Color.Black;
			this._drawPanel.Cursor = System.Windows.Forms.Cursors.Cross;
			this._drawPanel.Dock = System.Windows.Forms.DockStyle.Fill;
			this._drawPanel.Location = new System.Drawing.Point(0, 0);
			this._drawPanel.Name = "_drawPanel";
			this._drawPanel.Size = new System.Drawing.Size(559, 283);
			this._drawPanel.TabIndex = 2;
			this._drawPanel.VSync = false;
			this._drawPanel.Paint += new System.Windows.Forms.PaintEventHandler(this._drawPanel_Paint);
			this._drawPanel.DoubleClick += new System.EventHandler(this._drawPanel_DoubleClick);
			this._drawPanel.KeyDown += new System.Windows.Forms.KeyEventHandler(this._drawPanel_KeyDown);
			this._drawPanel.MouseClick += new System.Windows.Forms.MouseEventHandler(this._drawPanel_MouseClick);
			this._drawPanel.MouseMove += new System.Windows.Forms.MouseEventHandler(this._drawPanel_MouseMove);
			this._drawPanel.Resize += new System.EventHandler(this._drawPanel_Resize);
			// 
			// _drawPanelScrollBar
			// 
			this._drawPanelScrollBar.Dock = System.Windows.Forms.DockStyle.Bottom;
			this._drawPanelScrollBar.LargeChange = 1;
			this._drawPanelScrollBar.Location = new System.Drawing.Point(0, 283);
			this._drawPanelScrollBar.Name = "_drawPanelScrollBar";
			this._drawPanelScrollBar.Size = new System.Drawing.Size(559, 17);
			this._drawPanelScrollBar.TabIndex = 3;
			this._drawPanelScrollBar.ValueChanged += new System.EventHandler(this._drawPanelScrollBar_ValueChanged);
			// 
			// RenderControl
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this._drawPanel);
			this.Controls.Add(this._drawPanelScrollBar);
			this.Name = "RenderControl";
			this.Size = new System.Drawing.Size(559, 300);
			this.Load += new System.EventHandler(this.RenderControl_Load);
			this.ResumeLayout(false);

		}

		#endregion

		private OpenTK.GLControl _drawPanel;
		private System.Windows.Forms.HScrollBar _drawPanelScrollBar;
	}
}
