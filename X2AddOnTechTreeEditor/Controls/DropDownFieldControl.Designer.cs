namespace X2AddOnTechTreeEditor.Controls
{
	partial class DropDownFieldControl
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
			this._nameLabel = new System.Windows.Forms.Label();
			this._idTextBox = new System.Windows.Forms.TextBox();
			this.SuspendLayout();
			// 
			// _nameLabel
			// 
			this._nameLabel.Dock = System.Windows.Forms.DockStyle.Left;
			this._nameLabel.Location = new System.Drawing.Point(0, 0);
			this._nameLabel.Margin = new System.Windows.Forms.Padding(3);
			this._nameLabel.Name = "_nameLabel";
			this._nameLabel.Padding = new System.Windows.Forms.Padding(0, 3, 0, 0);
			this._nameLabel.Size = new System.Drawing.Size(83, 20);
			this._nameLabel.TabIndex = 1;
			this._nameLabel.Text = "Angriff-Grafik:";
			// 
			// _idTextBox
			// 
			this._idTextBox.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
			this._idTextBox.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.CustomSource;
			this._idTextBox.Dock = System.Windows.Forms.DockStyle.Fill;
			this._idTextBox.Location = new System.Drawing.Point(83, 0);
			this._idTextBox.Name = "_idTextBox";
			this._idTextBox.Size = new System.Drawing.Size(215, 20);
			this._idTextBox.TabIndex = 2;
			this._idTextBox.TextChanged += new System.EventHandler(this._idTextBox_TextChanged);
			this._idTextBox.KeyUp += new System.Windows.Forms.KeyEventHandler(this._idTextBox_KeyUp);
			this._idTextBox.Leave += new System.EventHandler(this._idTextBox_Leave);
			// 
			// DropDownFieldControl
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this._idTextBox);
			this.Controls.Add(this._nameLabel);
			this.Name = "DropDownFieldControl";
			this.Size = new System.Drawing.Size(298, 20);
			this.Load += new System.EventHandler(this.DropDownFieldControl_Load);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Label _nameLabel;
		private System.Windows.Forms.TextBox _idTextBox;
	}
}
