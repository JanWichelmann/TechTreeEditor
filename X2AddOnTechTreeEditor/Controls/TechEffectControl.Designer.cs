namespace X2AddOnTechTreeEditor.Controls
{
	partial class TechEffectControl
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
			this._effectOptionsPanel = new System.Windows.Forms.Panel();
			this._effectTypeLabel = new System.Windows.Forms.Label();
			this._effectTypeComboBox = new System.Windows.Forms.ComboBox();
			this._sortEffectsButton = new System.Windows.Forms.Button();
			this._newEffectButton = new System.Windows.Forms.Button();
			this._deleteEffectButton = new System.Windows.Forms.Button();
			this._effectDownButton = new System.Windows.Forms.Button();
			this._effectUpButton = new System.Windows.Forms.Button();
			this._resourceLabel = new System.Windows.Forms.Label();
			this._resourceComboBox = new System.Windows.Forms.ComboBox();
			this._armourClassLabel = new System.Windows.Forms.Label();
			this._armourClassComboBox = new System.Windows.Forms.ComboBox();
			this._attributeLabel = new System.Windows.Forms.Label();
			this._attributeComboBox = new System.Windows.Forms.ComboBox();
			this._classLabel = new System.Windows.Forms.Label();
			this._classComboBox = new System.Windows.Forms.ComboBox();
			this._effectsListBox = new X2AddOnTechTreeEditor.DoubleBufferedListBox();
			this._valueField = new X2AddOnTechTreeEditor.Controls.NumberFieldControl();
			this._modeCheckBox = new X2AddOnTechTreeEditor.Controls.CheckBoxFieldControl();
			this._researchField = new X2AddOnTechTreeEditor.Controls.DropDownFieldControl();
			this._destUnitField = new X2AddOnTechTreeEditor.Controls.DropDownFieldControl();
			this._unitField = new X2AddOnTechTreeEditor.Controls.DropDownFieldControl();
			this._effectOptionsPanel.SuspendLayout();
			this.SuspendLayout();
			// 
			// _effectOptionsPanel
			// 
			this._effectOptionsPanel.Controls.Add(this._effectTypeLabel);
			this._effectOptionsPanel.Controls.Add(this._effectTypeComboBox);
			this._effectOptionsPanel.Controls.Add(this._sortEffectsButton);
			this._effectOptionsPanel.Controls.Add(this._newEffectButton);
			this._effectOptionsPanel.Controls.Add(this._deleteEffectButton);
			this._effectOptionsPanel.Controls.Add(this._effectDownButton);
			this._effectOptionsPanel.Controls.Add(this._effectUpButton);
			this._effectOptionsPanel.Controls.Add(this._resourceLabel);
			this._effectOptionsPanel.Controls.Add(this._resourceComboBox);
			this._effectOptionsPanel.Controls.Add(this._armourClassLabel);
			this._effectOptionsPanel.Controls.Add(this._armourClassComboBox);
			this._effectOptionsPanel.Controls.Add(this._attributeLabel);
			this._effectOptionsPanel.Controls.Add(this._attributeComboBox);
			this._effectOptionsPanel.Controls.Add(this._classLabel);
			this._effectOptionsPanel.Controls.Add(this._classComboBox);
			this._effectOptionsPanel.Controls.Add(this._valueField);
			this._effectOptionsPanel.Controls.Add(this._modeCheckBox);
			this._effectOptionsPanel.Controls.Add(this._researchField);
			this._effectOptionsPanel.Controls.Add(this._destUnitField);
			this._effectOptionsPanel.Controls.Add(this._unitField);
			this._effectOptionsPanel.Dock = System.Windows.Forms.DockStyle.Right;
			this._effectOptionsPanel.Location = new System.Drawing.Point(481, 0);
			this._effectOptionsPanel.Name = "_effectOptionsPanel";
			this._effectOptionsPanel.Size = new System.Drawing.Size(400, 413);
			this._effectOptionsPanel.TabIndex = 6;
			// 
			// _effectTypeLabel
			// 
			this._effectTypeLabel.AutoSize = true;
			this._effectTypeLabel.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			this._effectTypeLabel.Location = new System.Drawing.Point(6, 9);
			this._effectTypeLabel.Name = "_effectTypeLabel";
			this._effectTypeLabel.Size = new System.Drawing.Size(59, 13);
			this._effectTypeLabel.TabIndex = 23;
			this._effectTypeLabel.Text = "Effekt-Typ:";
			// 
			// _effectTypeComboBox
			// 
			this._effectTypeComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this._effectTypeComboBox.FormattingEnabled = true;
			this._effectTypeComboBox.Location = new System.Drawing.Point(89, 6);
			this._effectTypeComboBox.Name = "_effectTypeComboBox";
			this._effectTypeComboBox.Size = new System.Drawing.Size(297, 21);
			this._effectTypeComboBox.TabIndex = 22;
			this._effectTypeComboBox.SelectedIndexChanged += new System.EventHandler(this._effectTypeComboBox_SelectedIndexChanged);
			// 
			// _sortEffectsButton
			// 
			this._sortEffectsButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this._sortEffectsButton.BackgroundImage = global::X2AddOnTechTreeEditor.Icons.SortTechEffects;
			this._sortEffectsButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
			this._sortEffectsButton.FlatAppearance.BorderSize = 0;
			this._sortEffectsButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this._sortEffectsButton.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			this._sortEffectsButton.Location = new System.Drawing.Point(82, 376);
			this._sortEffectsButton.Name = "_sortEffectsButton";
			this._sortEffectsButton.Size = new System.Drawing.Size(52, 32);
			this._sortEffectsButton.TabIndex = 21;
			this._sortEffectsButton.UseVisualStyleBackColor = true;
			this._sortEffectsButton.Click += new System.EventHandler(this._sortEffectsButton_Click);
			// 
			// _newEffectButton
			// 
			this._newEffectButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this._newEffectButton.BackgroundImage = global::X2AddOnTechTreeEditor.Icons.NewTechEffect;
			this._newEffectButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
			this._newEffectButton.FlatAppearance.BorderSize = 0;
			this._newEffectButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this._newEffectButton.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			this._newEffectButton.Location = new System.Drawing.Point(44, 338);
			this._newEffectButton.Name = "_newEffectButton";
			this._newEffectButton.Size = new System.Drawing.Size(32, 32);
			this._newEffectButton.TabIndex = 20;
			this._newEffectButton.UseVisualStyleBackColor = true;
			this._newEffectButton.Click += new System.EventHandler(this._newEffectButton_Click);
			// 
			// _deleteEffectButton
			// 
			this._deleteEffectButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this._deleteEffectButton.BackgroundImage = global::X2AddOnTechTreeEditor.Icons.DeleteTechEffect;
			this._deleteEffectButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
			this._deleteEffectButton.FlatAppearance.BorderSize = 0;
			this._deleteEffectButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this._deleteEffectButton.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			this._deleteEffectButton.Location = new System.Drawing.Point(44, 376);
			this._deleteEffectButton.Name = "_deleteEffectButton";
			this._deleteEffectButton.Size = new System.Drawing.Size(32, 32);
			this._deleteEffectButton.TabIndex = 19;
			this._deleteEffectButton.UseVisualStyleBackColor = true;
			this._deleteEffectButton.Click += new System.EventHandler(this._deleteEffectButton_Click);
			// 
			// _effectDownButton
			// 
			this._effectDownButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this._effectDownButton.BackgroundImage = global::X2AddOnTechTreeEditor.Icons.DownTechEffect;
			this._effectDownButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
			this._effectDownButton.FlatAppearance.BorderSize = 0;
			this._effectDownButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this._effectDownButton.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			this._effectDownButton.Location = new System.Drawing.Point(6, 376);
			this._effectDownButton.Name = "_effectDownButton";
			this._effectDownButton.Size = new System.Drawing.Size(32, 32);
			this._effectDownButton.TabIndex = 18;
			this._effectDownButton.UseVisualStyleBackColor = true;
			this._effectDownButton.Click += new System.EventHandler(this._effectDownButton_Click);
			// 
			// _effectUpButton
			// 
			this._effectUpButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this._effectUpButton.BackgroundImage = global::X2AddOnTechTreeEditor.Icons.UpTechEffect;
			this._effectUpButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
			this._effectUpButton.FlatAppearance.BorderSize = 0;
			this._effectUpButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this._effectUpButton.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			this._effectUpButton.Location = new System.Drawing.Point(6, 338);
			this._effectUpButton.Name = "_effectUpButton";
			this._effectUpButton.Size = new System.Drawing.Size(32, 32);
			this._effectUpButton.TabIndex = 17;
			this._effectUpButton.UseVisualStyleBackColor = true;
			this._effectUpButton.Click += new System.EventHandler(this._effectUpButton_Click);
			// 
			// _resourceLabel
			// 
			this._resourceLabel.AutoSize = true;
			this._resourceLabel.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			this._resourceLabel.Location = new System.Drawing.Point(6, 195);
			this._resourceLabel.Name = "_resourceLabel";
			this._resourceLabel.Size = new System.Drawing.Size(61, 13);
			this._resourceLabel.TabIndex = 16;
			this._resourceLabel.Text = "Ressource:";
			// 
			// _resourceComboBox
			// 
			this._resourceComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this._resourceComboBox.FormattingEnabled = true;
			this._resourceComboBox.Location = new System.Drawing.Point(100, 192);
			this._resourceComboBox.Name = "_resourceComboBox";
			this._resourceComboBox.Size = new System.Drawing.Size(204, 21);
			this._resourceComboBox.TabIndex = 15;
			this._resourceComboBox.SelectedIndexChanged += new System.EventHandler(this._resourceComboBox_SelectedIndexChanged);
			// 
			// _armourClassLabel
			// 
			this._armourClassLabel.AutoSize = true;
			this._armourClassLabel.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			this._armourClassLabel.Location = new System.Drawing.Point(6, 168);
			this._armourClassLabel.Name = "_armourClassLabel";
			this._armourClassLabel.Size = new System.Drawing.Size(85, 13);
			this._armourClassLabel.TabIndex = 14;
			this._armourClassLabel.Text = "Rüstungsklasse:";
			// 
			// _armourClassComboBox
			// 
			this._armourClassComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this._armourClassComboBox.FormattingEnabled = true;
			this._armourClassComboBox.Location = new System.Drawing.Point(100, 165);
			this._armourClassComboBox.Name = "_armourClassComboBox";
			this._armourClassComboBox.Size = new System.Drawing.Size(204, 21);
			this._armourClassComboBox.TabIndex = 13;
			this._armourClassComboBox.SelectedIndexChanged += new System.EventHandler(this._armourClassComboBox_SelectedIndexChanged);
			// 
			// _attributeLabel
			// 
			this._attributeLabel.AutoSize = true;
			this._attributeLabel.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			this._attributeLabel.Location = new System.Drawing.Point(6, 141);
			this._attributeLabel.Name = "_attributeLabel";
			this._attributeLabel.Size = new System.Drawing.Size(43, 13);
			this._attributeLabel.TabIndex = 12;
			this._attributeLabel.Text = "Attribut:";
			// 
			// _attributeComboBox
			// 
			this._attributeComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this._attributeComboBox.FormattingEnabled = true;
			this._attributeComboBox.Location = new System.Drawing.Point(100, 138);
			this._attributeComboBox.Name = "_attributeComboBox";
			this._attributeComboBox.Size = new System.Drawing.Size(204, 21);
			this._attributeComboBox.TabIndex = 11;
			this._attributeComboBox.SelectedIndexChanged += new System.EventHandler(this._attributeComboBox_SelectedIndexChanged);
			// 
			// _classLabel
			// 
			this._classLabel.AutoSize = true;
			this._classLabel.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			this._classLabel.Location = new System.Drawing.Point(6, 114);
			this._classLabel.Name = "_classLabel";
			this._classLabel.Size = new System.Drawing.Size(41, 13);
			this._classLabel.TabIndex = 10;
			this._classLabel.Text = "Klasse:";
			// 
			// _classComboBox
			// 
			this._classComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this._classComboBox.FormattingEnabled = true;
			this._classComboBox.Location = new System.Drawing.Point(100, 111);
			this._classComboBox.Name = "_classComboBox";
			this._classComboBox.Size = new System.Drawing.Size(204, 21);
			this._classComboBox.TabIndex = 9;
			this._classComboBox.SelectedIndexChanged += new System.EventHandler(this._classComboBox_SelectedIndexChanged);
			// 
			// _effectsListBox
			// 
			this._effectsListBox.Dock = System.Windows.Forms.DockStyle.Fill;
			this._effectsListBox.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
			this._effectsListBox.HorizontalScrollbar = true;
			this._effectsListBox.Location = new System.Drawing.Point(0, 0);
			this._effectsListBox.Name = "_effectsListBox";
			this._effectsListBox.Size = new System.Drawing.Size(481, 413);
			this._effectsListBox.TabIndex = 5;
			this._effectsListBox.SelectedIndexChanged += new System.EventHandler(this._effectsListBox_SelectedIndexChanged);
			// 
			// _valueField
			// 
			this._valueField.Location = new System.Drawing.Point(296, 219);
			this._valueField.Name = "_valueField";
			this._valueField.NameString = "Wert:";
			this._valueField.Size = new System.Drawing.Size(91, 20);
			this._valueField.TabIndex = 8;
			this._valueField.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
			this._valueField.ValueChanged += new X2AddOnTechTreeEditor.Controls.NumberFieldControl.ValueChangedEventHandler(this._valueField_ValueChanged);
			// 
			// _modeCheckBox
			// 
			this._modeCheckBox.Location = new System.Drawing.Point(9, 219);
			this._modeCheckBox.Name = "_modeCheckBox";
			this._modeCheckBox.NameString = "Modus: [  ]  = Setzen, [X] = +/-";
			this._modeCheckBox.Size = new System.Drawing.Size(169, 20);
			this._modeCheckBox.TabIndex = 6;
			this._modeCheckBox.Value = false;
			this._modeCheckBox.ValueChanged += new X2AddOnTechTreeEditor.Controls.CheckBoxFieldControl.ValueChangedEventHandler(this._modeCheckBox_ValueChanged);
			// 
			// _researchField
			// 
			this._researchField.Location = new System.Drawing.Point(5, 85);
			this._researchField.Name = "_researchField";
			this._researchField.NameString = "Technologie:";
			this._researchField.Size = new System.Drawing.Size(381, 20);
			this._researchField.TabIndex = 2;
			this._researchField.Value = null;
			this._researchField.ValueChanged += new X2AddOnTechTreeEditor.Controls.DropDownFieldControl.ValueChangedEventHandler(this._researchField_ValueChanged);
			// 
			// _destUnitField
			// 
			this._destUnitField.Location = new System.Drawing.Point(5, 59);
			this._destUnitField.Name = "_destUnitField";
			this._destUnitField.NameString = "Upgrade:";
			this._destUnitField.Size = new System.Drawing.Size(381, 20);
			this._destUnitField.TabIndex = 1;
			this._destUnitField.Value = null;
			this._destUnitField.ValueChanged += new X2AddOnTechTreeEditor.Controls.DropDownFieldControl.ValueChangedEventHandler(this._destUnitField_ValueChanged);
			// 
			// _unitField
			// 
			this._unitField.Location = new System.Drawing.Point(6, 33);
			this._unitField.Name = "_unitField";
			this._unitField.NameString = "Einheit:";
			this._unitField.Size = new System.Drawing.Size(381, 20);
			this._unitField.TabIndex = 0;
			this._unitField.Value = null;
			this._unitField.ValueChanged += new X2AddOnTechTreeEditor.Controls.DropDownFieldControl.ValueChangedEventHandler(this._unitField_ValueChanged);
			// 
			// TechEffectControl
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this._effectsListBox);
			this.Controls.Add(this._effectOptionsPanel);
			this.Name = "TechEffectControl";
			this.Size = new System.Drawing.Size(881, 413);
			this._effectOptionsPanel.ResumeLayout(false);
			this._effectOptionsPanel.PerformLayout();
			this.ResumeLayout(false);

		}

		#endregion

		private DoubleBufferedListBox _effectsListBox;
		private System.Windows.Forms.Panel _effectOptionsPanel;
		private System.Windows.Forms.Label _effectTypeLabel;
		private System.Windows.Forms.ComboBox _effectTypeComboBox;
		private System.Windows.Forms.Button _sortEffectsButton;
		private System.Windows.Forms.Button _newEffectButton;
		private System.Windows.Forms.Button _deleteEffectButton;
		private System.Windows.Forms.Button _effectDownButton;
		private System.Windows.Forms.Button _effectUpButton;
		private System.Windows.Forms.Label _resourceLabel;
		private System.Windows.Forms.ComboBox _resourceComboBox;
		private System.Windows.Forms.Label _armourClassLabel;
		private System.Windows.Forms.ComboBox _armourClassComboBox;
		private System.Windows.Forms.Label _attributeLabel;
		private System.Windows.Forms.ComboBox _attributeComboBox;
		private System.Windows.Forms.Label _classLabel;
		private System.Windows.Forms.ComboBox _classComboBox;
		private Controls.NumberFieldControl _valueField;
		private Controls.CheckBoxFieldControl _modeCheckBox;
		private Controls.DropDownFieldControl _researchField;
		private Controls.DropDownFieldControl _destUnitField;
		private Controls.DropDownFieldControl _unitField;
	}
}
