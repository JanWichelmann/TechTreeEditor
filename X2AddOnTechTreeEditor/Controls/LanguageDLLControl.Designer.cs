namespace X2AddOnTechTreeEditor.Controls
{
	partial class LanguageDLLControl
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
			this._idTextBox = new System.Windows.Forms.TextBox();
			this._nameLabel = new System.Windows.Forms.Label();
			this._valueLabel = new System.Windows.Forms.Label();
			this.SuspendLayout();
			// 
			// _idTextBox
			// 
			this._idTextBox.Dock = System.Windows.Forms.DockStyle.Left;
			this._idTextBox.Location = new System.Drawing.Point(100, 0);
			this._idTextBox.Margin = new System.Windows.Forms.Padding(3, 0, 3, 0);
			this._idTextBox.MaxLength = 6;
			this._idTextBox.Name = "_idTextBox";
			this._idTextBox.Size = new System.Drawing.Size(43, 20);
			this._idTextBox.TabIndex = 0;
			this._idTextBox.Text = "000000";
			this._idTextBox.TextChanged += new System.EventHandler(this._idTextBox_TextChanged);
			// 
			// _nameLabel
			// 
			this._nameLabel.Dock = System.Windows.Forms.DockStyle.Left;
			this._nameLabel.Location = new System.Drawing.Point(0, 0);
			this._nameLabel.Margin = new System.Windows.Forms.Padding(3);
			this._nameLabel.Name = "_nameLabel";
			this._nameLabel.Padding = new System.Windows.Forms.Padding(0, 3, 0, 0);
			this._nameLabel.Size = new System.Drawing.Size(100, 20);
			this._nameLabel.TabIndex = 1;
			this._nameLabel.Text = "DLL-Beschreibung: ";
			// 
			// _valueLabel
			// 
			this._valueLabel.AutoEllipsis = true;
			this._valueLabel.Dock = System.Windows.Forms.DockStyle.Fill;
			this._valueLabel.Location = new System.Drawing.Point(143, 0);
			this._valueLabel.Name = "_valueLabel";
			this._valueLabel.Padding = new System.Windows.Forms.Padding(3, 3, 0, 0);
			this._valueLabel.Size = new System.Drawing.Size(272, 20);
			this._valueLabel.TabIndex = 2;
			this._valueLabel.Text = "Wert...";
			// 
			// LanguageDLLControl
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this._valueLabel);
			this.Controls.Add(this._idTextBox);
			this.Controls.Add(this._nameLabel);
			this.Name = "LanguageDLLControl";
			this.Size = new System.Drawing.Size(415, 20);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.TextBox _idTextBox;
		private System.Windows.Forms.Label _nameLabel;
		private System.Windows.Forms.Label _valueLabel;
	}
}
