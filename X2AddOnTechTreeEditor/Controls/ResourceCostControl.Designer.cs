namespace X2AddOnTechTreeEditor.Controls
{
	partial class ResourceCostControl
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
			this._amountTextBox = new System.Windows.Forms.TextBox();
			this._nameLabel = new System.Windows.Forms.Label();
			this._enableCheckBox = new System.Windows.Forms.CheckBox();
			this._typeComboBox = new System.Windows.Forms.ComboBox();
			this.SuspendLayout();
			// 
			// _amountTextBox
			// 
			this._amountTextBox.Dock = System.Windows.Forms.DockStyle.Right;
			this._amountTextBox.Location = new System.Drawing.Point(306, 0);
			this._amountTextBox.Margin = new System.Windows.Forms.Padding(3, 0, 3, 0);
			this._amountTextBox.MaxLength = 6;
			this._amountTextBox.Name = "_amountTextBox";
			this._amountTextBox.Size = new System.Drawing.Size(43, 20);
			this._amountTextBox.TabIndex = 0;
			this._amountTextBox.Text = "000000";
			this._amountTextBox.TextChanged += new System.EventHandler(this._amountTextBox_TextChanged);
			// 
			// _nameLabel
			// 
			this._nameLabel.Dock = System.Windows.Forms.DockStyle.Fill;
			this._nameLabel.Location = new System.Drawing.Point(0, 0);
			this._nameLabel.Margin = new System.Windows.Forms.Padding(3);
			this._nameLabel.Name = "_nameLabel";
			this._nameLabel.Padding = new System.Windows.Forms.Padding(0, 3, 0, 0);
			this._nameLabel.Size = new System.Drawing.Size(98, 21);
			this._nameLabel.TabIndex = 1;
			this._nameLabel.Text = "Kosten 1:";
			// 
			// _enableCheckBox
			// 
			this._enableCheckBox.AutoSize = true;
			this._enableCheckBox.Checked = true;
			this._enableCheckBox.CheckState = System.Windows.Forms.CheckState.Checked;
			this._enableCheckBox.Dock = System.Windows.Forms.DockStyle.Right;
			this._enableCheckBox.Location = new System.Drawing.Point(98, 0);
			this._enableCheckBox.Name = "_enableCheckBox";
			this._enableCheckBox.Size = new System.Drawing.Size(15, 21);
			this._enableCheckBox.TabIndex = 2;
			this._enableCheckBox.UseVisualStyleBackColor = true;
			this._enableCheckBox.CheckedChanged += new System.EventHandler(this._enableCheckBox_CheckedChanged);
			// 
			// _typeComboBox
			// 
			this._typeComboBox.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
			this._typeComboBox.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
			this._typeComboBox.Dock = System.Windows.Forms.DockStyle.Right;
			this._typeComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this._typeComboBox.FormattingEnabled = true;
			this._typeComboBox.Location = new System.Drawing.Point(113, 0);
			this._typeComboBox.Name = "_typeComboBox";
			this._typeComboBox.Size = new System.Drawing.Size(193, 21);
			this._typeComboBox.TabIndex = 3;
			this._typeComboBox.SelectedIndexChanged += new System.EventHandler(this._typeComboBox_SelectedIndexChanged);
			// 
			// ResourceCostControl
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this._nameLabel);
			this.Controls.Add(this._enableCheckBox);
			this.Controls.Add(this._typeComboBox);
			this.Controls.Add(this._amountTextBox);
			this.Name = "ResourceCostControl";
			this.Size = new System.Drawing.Size(349, 21);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.TextBox _amountTextBox;
		private System.Windows.Forms.Label _nameLabel;
		private System.Windows.Forms.CheckBox _enableCheckBox;
		private System.Windows.Forms.ComboBox _typeComboBox;
	}
}
