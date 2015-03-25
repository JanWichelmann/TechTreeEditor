namespace X2AddOnTechTreeEditor.Controls
{
	partial class GraphicControl
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
			this._idComboBox = new System.Windows.Forms.ComboBox();
			this.SuspendLayout();
			// 
			// _nameLabel
			// 
			this._nameLabel.Dock = System.Windows.Forms.DockStyle.Left;
			this._nameLabel.Location = new System.Drawing.Point(0, 0);
			this._nameLabel.Margin = new System.Windows.Forms.Padding(3);
			this._nameLabel.Name = "_nameLabel";
			this._nameLabel.Padding = new System.Windows.Forms.Padding(0, 3, 0, 0);
			this._nameLabel.Size = new System.Drawing.Size(95, 20);
			this._nameLabel.TabIndex = 1;
			this._nameLabel.Text = "Angriff-Grafik:";
			// 
			// _idComboBox
			// 
			this._idComboBox.Dock = System.Windows.Forms.DockStyle.Fill;
			this._idComboBox.Location = new System.Drawing.Point(95, 0);
			this._idComboBox.Name = "_idComboBox";
			this._idComboBox.Size = new System.Drawing.Size(203, 21);
			this._idComboBox.TabIndex = 2;
			this._idComboBox.SelectedValueChanged += new System.EventHandler(this._idComboBox_SelectedValueChanged);
			// 
			// GraphicControl
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this._idComboBox);
			this.Controls.Add(this._nameLabel);
			this.Name = "GraphicControl";
			this.Size = new System.Drawing.Size(298, 20);
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.Label _nameLabel;
		private System.Windows.Forms.ComboBox _idComboBox;
	}
}
