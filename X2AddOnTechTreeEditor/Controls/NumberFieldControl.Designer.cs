namespace X2AddOnTechTreeEditor.Controls
{
	partial class NumberFieldControl
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
			this._valueTextBox = new System.Windows.Forms.TextBox();
			this._nameLabel = new System.Windows.Forms.Label();
			this.SuspendLayout();
			// 
			// _valueTextBox
			// 
			this._valueTextBox.Dock = System.Windows.Forms.DockStyle.Right;
			this._valueTextBox.Location = new System.Drawing.Point(132, 0);
			this._valueTextBox.Margin = new System.Windows.Forms.Padding(3, 0, 3, 0);
			this._valueTextBox.MaxLength = 7;
			this._valueTextBox.Name = "_valueTextBox";
			this._valueTextBox.Size = new System.Drawing.Size(50, 20);
			this._valueTextBox.TabIndex = 0;
			this._valueTextBox.Text = "0000000";
			this._valueTextBox.TextChanged += new System.EventHandler(this._valueTextBox_TextChanged);
			// 
			// _nameLabel
			// 
			this._nameLabel.Dock = System.Windows.Forms.DockStyle.Fill;
			this._nameLabel.Location = new System.Drawing.Point(0, 0);
			this._nameLabel.Margin = new System.Windows.Forms.Padding(3);
			this._nameLabel.Name = "_nameLabel";
			this._nameLabel.Padding = new System.Windows.Forms.Padding(0, 3, 0, 0);
			this._nameLabel.Size = new System.Drawing.Size(132, 20);
			this._nameLabel.TabIndex = 1;
			this._nameLabel.Text = "Eine Zahl eingeben: ";
			// 
			// NumberFieldControl
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this._nameLabel);
			this.Controls.Add(this._valueTextBox);
			this.Name = "NumberFieldControl";
			this.Size = new System.Drawing.Size(182, 20);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.TextBox _valueTextBox;
		private System.Windows.Forms.Label _nameLabel;
	}
}
