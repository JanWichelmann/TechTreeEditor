namespace X2AddOnTechTreeEditor
{
	partial class EditGraphicsForm
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(EditGraphicsForm));
			this._bottomPanel = new System.Windows.Forms.Panel();
			this._saveButton = new System.Windows.Forms.Button();
			this._closeButton = new System.Windows.Forms.Button();
			this._mainPanel = new System.Windows.Forms.Panel();
			this._attackSoundView = new System.Windows.Forms.DataGridView();
			this._attackSoundAngleColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this._attackSound1IDColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this._attackSound1DelayColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this._attackSound2IDColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this._attackSound2DelayColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this._attackSound3IDColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this._attackSound3DelayColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.label4 = new System.Windows.Forms.Label();
			this._deltaView = new System.Windows.Forms.DataGridView();
			this._deltaIDColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this._deltaXColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this._deltaYColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this._name2TextBox = new System.Windows.Forms.TextBox();
			this.label3 = new System.Windows.Forms.Label();
			this._name1TextBox = new System.Windows.Forms.TextBox();
			this.label2 = new System.Windows.Forms.Label();
			this._filterTextBox = new System.Windows.Forms.TextBox();
			this.label1 = new System.Windows.Forms.Label();
			this._attackSoundField = new X2AddOnTechTreeEditor.Controls.CheckBoxFieldControl();
			this._unknown3Field = new X2AddOnTechTreeEditor.Controls.NumberFieldControl();
			this._unknown2Field = new X2AddOnTechTreeEditor.Controls.NumberFieldControl();
			this._unknown1Field = new X2AddOnTechTreeEditor.Controls.NumberFieldControl();
			this._layerField = new X2AddOnTechTreeEditor.Controls.NumberFieldControl();
			this._speedField = new X2AddOnTechTreeEditor.Controls.NumberFieldControl();
			this._rainbowField = new X2AddOnTechTreeEditor.Controls.NumberFieldControl();
			this._mirrorField = new X2AddOnTechTreeEditor.Controls.NumberFieldControl();
			this._sequenceTypeField = new X2AddOnTechTreeEditor.Controls.NumberFieldControl();
			this._coord4Field = new X2AddOnTechTreeEditor.Controls.NumberFieldControl();
			this._coord3Field = new X2AddOnTechTreeEditor.Controls.NumberFieldControl();
			this._coord2Field = new X2AddOnTechTreeEditor.Controls.NumberFieldControl();
			this._coord1Field = new X2AddOnTechTreeEditor.Controls.NumberFieldControl();
			this._playerColorField = new X2AddOnTechTreeEditor.Controls.NumberFieldControl();
			this._soundIDField = new X2AddOnTechTreeEditor.Controls.NumberFieldControl();
			this._idField = new X2AddOnTechTreeEditor.Controls.NumberFieldControl();
			this._angleCountField = new X2AddOnTechTreeEditor.Controls.NumberFieldControl();
			this._replayDelayField = new X2AddOnTechTreeEditor.Controls.NumberFieldControl();
			this._replayCheckBox = new X2AddOnTechTreeEditor.Controls.CheckBoxFieldControl();
			this._frameCountField = new X2AddOnTechTreeEditor.Controls.NumberFieldControl();
			this._frameRateField = new X2AddOnTechTreeEditor.Controls.NumberFieldControl();
			this._slpField = new X2AddOnTechTreeEditor.Controls.NumberFieldControl();
			this._graphicListBox = new X2AddOnTechTreeEditor.DoubleBufferedListBox();
			this._newGraphicButton = new System.Windows.Forms.Button();
			this._deleteGraphicButton = new System.Windows.Forms.Button();
			this._bottomPanel.SuspendLayout();
			this._mainPanel.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this._attackSoundView)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this._deltaView)).BeginInit();
			this.SuspendLayout();
			// 
			// _bottomPanel
			// 
			this._bottomPanel.Controls.Add(this._saveButton);
			this._bottomPanel.Controls.Add(this._closeButton);
			this._bottomPanel.Dock = System.Windows.Forms.DockStyle.Bottom;
			this._bottomPanel.Location = new System.Drawing.Point(0, 606);
			this._bottomPanel.Name = "_bottomPanel";
			this._bottomPanel.Size = new System.Drawing.Size(1003, 45);
			this._bottomPanel.TabIndex = 2;
			// 
			// _saveButton
			// 
			this._saveButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this._saveButton.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			this._saveButton.Location = new System.Drawing.Point(663, 10);
			this._saveButton.Name = "_saveButton";
			this._saveButton.Size = new System.Drawing.Size(161, 23);
			this._saveButton.TabIndex = 4;
			this._saveButton.Text = "Änderungen speichern";
			this._saveButton.UseVisualStyleBackColor = true;
			this._saveButton.Click += new System.EventHandler(this._saveButton_Click);
			// 
			// _closeButton
			// 
			this._closeButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this._closeButton.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			this._closeButton.Location = new System.Drawing.Point(830, 10);
			this._closeButton.Name = "_closeButton";
			this._closeButton.Size = new System.Drawing.Size(161, 23);
			this._closeButton.TabIndex = 3;
			this._closeButton.Text = "Fertig";
			this._closeButton.UseVisualStyleBackColor = true;
			this._closeButton.Click += new System.EventHandler(this._closeButton_Click);
			// 
			// _mainPanel
			// 
			this._mainPanel.Controls.Add(this._attackSoundField);
			this._mainPanel.Controls.Add(this._attackSoundView);
			this._mainPanel.Controls.Add(this.label4);
			this._mainPanel.Controls.Add(this._deltaView);
			this._mainPanel.Controls.Add(this._unknown3Field);
			this._mainPanel.Controls.Add(this._unknown2Field);
			this._mainPanel.Controls.Add(this._unknown1Field);
			this._mainPanel.Controls.Add(this._layerField);
			this._mainPanel.Controls.Add(this._speedField);
			this._mainPanel.Controls.Add(this._rainbowField);
			this._mainPanel.Controls.Add(this._mirrorField);
			this._mainPanel.Controls.Add(this._sequenceTypeField);
			this._mainPanel.Controls.Add(this._coord4Field);
			this._mainPanel.Controls.Add(this._coord3Field);
			this._mainPanel.Controls.Add(this._coord2Field);
			this._mainPanel.Controls.Add(this._coord1Field);
			this._mainPanel.Controls.Add(this._playerColorField);
			this._mainPanel.Controls.Add(this._soundIDField);
			this._mainPanel.Controls.Add(this._idField);
			this._mainPanel.Controls.Add(this._angleCountField);
			this._mainPanel.Controls.Add(this._replayDelayField);
			this._mainPanel.Controls.Add(this._replayCheckBox);
			this._mainPanel.Controls.Add(this._frameCountField);
			this._mainPanel.Controls.Add(this._frameRateField);
			this._mainPanel.Controls.Add(this._slpField);
			this._mainPanel.Controls.Add(this._name2TextBox);
			this._mainPanel.Controls.Add(this.label3);
			this._mainPanel.Controls.Add(this._name1TextBox);
			this._mainPanel.Controls.Add(this.label2);
			this._mainPanel.Controls.Add(this._filterTextBox);
			this._mainPanel.Controls.Add(this.label1);
			this._mainPanel.Controls.Add(this._newGraphicButton);
			this._mainPanel.Controls.Add(this._deleteGraphicButton);
			this._mainPanel.Dock = System.Windows.Forms.DockStyle.Fill;
			this._mainPanel.Location = new System.Drawing.Point(230, 0);
			this._mainPanel.Name = "_mainPanel";
			this._mainPanel.Size = new System.Drawing.Size(773, 606);
			this._mainPanel.TabIndex = 4;
			// 
			// _attackSoundView
			// 
			this._attackSoundView.AllowUserToAddRows = false;
			this._attackSoundView.AllowUserToDeleteRows = false;
			this._attackSoundView.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
			this._attackSoundView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			this._attackSoundView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this._attackSoundAngleColumn,
            this._attackSound1IDColumn,
            this._attackSound1DelayColumn,
            this._attackSound2IDColumn,
            this._attackSound2DelayColumn,
            this._attackSound3IDColumn,
            this._attackSound3DelayColumn});
			this._attackSoundView.Location = new System.Drawing.Point(118, 323);
			this._attackSoundView.Name = "_attackSoundView";
			this._attackSoundView.Size = new System.Drawing.Size(644, 219);
			this._attackSoundView.TabIndex = 18;
			this._attackSoundView.CellValidating += new System.Windows.Forms.DataGridViewCellValidatingEventHandler(this._attackSoundView_CellValidating);
			this._attackSoundView.CellValueChanged += new System.Windows.Forms.DataGridViewCellEventHandler(this._attackSoundView_CellValueChanged);
			// 
			// _attackSoundAngleColumn
			// 
			this._attackSoundAngleColumn.FillWeight = 10F;
			this._attackSoundAngleColumn.HeaderText = "Achse";
			this._attackSoundAngleColumn.Name = "_attackSoundAngleColumn";
			this._attackSoundAngleColumn.ReadOnly = true;
			// 
			// _attackSound1IDColumn
			// 
			this._attackSound1IDColumn.FillWeight = 14F;
			this._attackSound1IDColumn.HeaderText = "ID #1";
			this._attackSound1IDColumn.Name = "_attackSound1IDColumn";
			this._attackSound1IDColumn.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
			// 
			// _attackSound1DelayColumn
			// 
			this._attackSound1DelayColumn.FillWeight = 16F;
			this._attackSound1DelayColumn.HeaderText = "Verzögerung #1";
			this._attackSound1DelayColumn.Name = "_attackSound1DelayColumn";
			this._attackSound1DelayColumn.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
			// 
			// _attackSound2IDColumn
			// 
			this._attackSound2IDColumn.FillWeight = 14F;
			this._attackSound2IDColumn.HeaderText = "ID #2";
			this._attackSound2IDColumn.Name = "_attackSound2IDColumn";
			this._attackSound2IDColumn.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
			// 
			// _attackSound2DelayColumn
			// 
			this._attackSound2DelayColumn.FillWeight = 16F;
			this._attackSound2DelayColumn.HeaderText = "Verzögerung #2";
			this._attackSound2DelayColumn.Name = "_attackSound2DelayColumn";
			this._attackSound2DelayColumn.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
			// 
			// _attackSound3IDColumn
			// 
			this._attackSound3IDColumn.FillWeight = 14F;
			this._attackSound3IDColumn.HeaderText = "ID #3";
			this._attackSound3IDColumn.Name = "_attackSound3IDColumn";
			this._attackSound3IDColumn.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
			// 
			// _attackSound3DelayColumn
			// 
			this._attackSound3DelayColumn.FillWeight = 16F;
			this._attackSound3DelayColumn.HeaderText = "Verzögerung #3";
			this._attackSound3DelayColumn.Name = "_attackSound3DelayColumn";
			this._attackSound3DelayColumn.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
			// 
			// label4
			// 
			this.label4.AutoSize = true;
			this.label4.Location = new System.Drawing.Point(414, 16);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(40, 13);
			this.label4.TabIndex = 50;
			this.label4.Text = "Deltas:";
			// 
			// _deltaView
			// 
			this._deltaView.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
			this._deltaView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			this._deltaView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this._deltaIDColumn,
            this._deltaXColumn,
            this._deltaYColumn});
			this._deltaView.Location = new System.Drawing.Point(417, 37);
			this._deltaView.Name = "_deltaView";
			this._deltaView.Size = new System.Drawing.Size(345, 280);
			this._deltaView.TabIndex = 10;
			this._deltaView.CellValidating += new System.Windows.Forms.DataGridViewCellValidatingEventHandler(this._deltaView_CellValidating);
			this._deltaView.CellValueChanged += new System.Windows.Forms.DataGridViewCellEventHandler(this._deltaView_CellValueChanged);
			this._deltaView.RowsRemoved += new System.Windows.Forms.DataGridViewRowsRemovedEventHandler(this._deltaView_RowsRemoved);
			// 
			// _deltaIDColumn
			// 
			this._deltaIDColumn.FillWeight = 34F;
			this._deltaIDColumn.HeaderText = "Grafik-ID";
			this._deltaIDColumn.Name = "_deltaIDColumn";
			this._deltaIDColumn.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
			// 
			// _deltaXColumn
			// 
			this._deltaXColumn.FillWeight = 33F;
			this._deltaXColumn.HeaderText = "X";
			this._deltaXColumn.Name = "_deltaXColumn";
			this._deltaXColumn.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
			// 
			// _deltaYColumn
			// 
			this._deltaYColumn.FillWeight = 33F;
			this._deltaYColumn.HeaderText = "Y";
			this._deltaYColumn.Name = "_deltaYColumn";
			this._deltaYColumn.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
			// 
			// _name2TextBox
			// 
			this._name2TextBox.Location = new System.Drawing.Point(266, 37);
			this._name2TextBox.MaxLength = 12;
			this._name2TextBox.Name = "_name2TextBox";
			this._name2TextBox.Size = new System.Drawing.Size(145, 20);
			this._name2TextBox.TabIndex = 28;
			this._name2TextBox.TextChanged += new System.EventHandler(this._name2TextBox_TextChanged);
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.Location = new System.Drawing.Point(213, 40);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(47, 13);
			this.label3.TabIndex = 27;
			this.label3.Text = "Name 2:";
			// 
			// _name1TextBox
			// 
			this._name1TextBox.Location = new System.Drawing.Point(62, 37);
			this._name1TextBox.MaxLength = 20;
			this._name1TextBox.Name = "_name1TextBox";
			this._name1TextBox.Size = new System.Drawing.Size(145, 20);
			this._name1TextBox.TabIndex = 26;
			this._name1TextBox.TextChanged += new System.EventHandler(this._name1TextBox_TextChanged);
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(9, 40);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(47, 13);
			this.label2.TabIndex = 25;
			this.label2.Text = "Name 1:";
			// 
			// _filterTextBox
			// 
			this._filterTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this._filterTextBox.Location = new System.Drawing.Point(44, 580);
			this._filterTextBox.Name = "_filterTextBox";
			this._filterTextBox.Size = new System.Drawing.Size(227, 20);
			this._filterTextBox.TabIndex = 24;
			this._filterTextBox.TextChanged += new System.EventHandler(this._filterTextBox_TextChanged);
			// 
			// label1
			// 
			this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(6, 583);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(32, 13);
			this.label1.TabIndex = 23;
			this.label1.Text = "Filter:";
			// 
			// _attackSoundField
			// 
			this._attackSoundField.Location = new System.Drawing.Point(9, 323);
			this._attackSoundField.Name = "_attackSoundField";
			this._attackSoundField.NameString = "Angriffssounds:";
			this._attackSoundField.Size = new System.Drawing.Size(103, 20);
			this._attackSoundField.TabIndex = 51;
			this._attackSoundField.Value = false;
			this._attackSoundField.ValueChanged += new X2AddOnTechTreeEditor.Controls.CheckBoxFieldControl.ValueChangedEventHandler(this._attackSoundField_ValueChanged);
			// 
			// _unknown3Field
			// 
			this._unknown3Field.Location = new System.Drawing.Point(213, 297);
			this._unknown3Field.Name = "_unknown3Field";
			this._unknown3Field.NameString = "Unbekannt 3:";
			this._unknown3Field.Size = new System.Drawing.Size(198, 20);
			this._unknown3Field.TabIndex = 49;
			this._unknown3Field.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
			this._unknown3Field.ValueChanged += new X2AddOnTechTreeEditor.Controls.NumberFieldControl.ValueChangedEventHandler(this._unknown3Field_ValueChanged);
			// 
			// _unknown2Field
			// 
			this._unknown2Field.Location = new System.Drawing.Point(9, 297);
			this._unknown2Field.Name = "_unknown2Field";
			this._unknown2Field.NameString = "Unbekannt 2:";
			this._unknown2Field.Size = new System.Drawing.Size(198, 20);
			this._unknown2Field.TabIndex = 48;
			this._unknown2Field.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
			this._unknown2Field.ValueChanged += new X2AddOnTechTreeEditor.Controls.NumberFieldControl.ValueChangedEventHandler(this._unknown2Field_ValueChanged);
			// 
			// _unknown1Field
			// 
			this._unknown1Field.Location = new System.Drawing.Point(213, 271);
			this._unknown1Field.Name = "_unknown1Field";
			this._unknown1Field.NameString = "Unbekannt 1:";
			this._unknown1Field.Size = new System.Drawing.Size(198, 20);
			this._unknown1Field.TabIndex = 47;
			this._unknown1Field.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
			this._unknown1Field.ValueChanged += new X2AddOnTechTreeEditor.Controls.NumberFieldControl.ValueChangedEventHandler(this._unknown1Field_ValueChanged);
			// 
			// _layerField
			// 
			this._layerField.Location = new System.Drawing.Point(9, 271);
			this._layerField.Name = "_layerField";
			this._layerField.NameString = "Ebene:";
			this._layerField.Size = new System.Drawing.Size(198, 20);
			this._layerField.TabIndex = 46;
			this._layerField.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
			this._layerField.ValueChanged += new X2AddOnTechTreeEditor.Controls.NumberFieldControl.ValueChangedEventHandler(this._layerField_ValueChanged);
			// 
			// _speedField
			// 
			this._speedField.Location = new System.Drawing.Point(213, 245);
			this._speedField.Name = "_speedField";
			this._speedField.NameString = "Geschwindigkeits-Mult.:";
			this._speedField.Size = new System.Drawing.Size(198, 20);
			this._speedField.TabIndex = 45;
			this._speedField.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
			this._speedField.ValueChanged += new X2AddOnTechTreeEditor.Controls.NumberFieldControl.ValueChangedEventHandler(this._speedField_ValueChanged);
			// 
			// _rainbowField
			// 
			this._rainbowField.Location = new System.Drawing.Point(9, 245);
			this._rainbowField.Name = "_rainbowField";
			this._rainbowField.NameString = "Farbwechsel:";
			this._rainbowField.Size = new System.Drawing.Size(198, 20);
			this._rainbowField.TabIndex = 44;
			this._rainbowField.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
			this._rainbowField.ValueChanged += new X2AddOnTechTreeEditor.Controls.NumberFieldControl.ValueChangedEventHandler(this._rainbowField_ValueChanged);
			// 
			// _mirrorField
			// 
			this._mirrorField.Location = new System.Drawing.Point(213, 219);
			this._mirrorField.Name = "_mirrorField";
			this._mirrorField.NameString = "Spiegelmodus:";
			this._mirrorField.Size = new System.Drawing.Size(198, 20);
			this._mirrorField.TabIndex = 43;
			this._mirrorField.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
			this._mirrorField.ValueChanged += new X2AddOnTechTreeEditor.Controls.NumberFieldControl.ValueChangedEventHandler(this._mirrorField_ValueChanged);
			// 
			// _sequenceTypeField
			// 
			this._sequenceTypeField.Location = new System.Drawing.Point(9, 219);
			this._sequenceTypeField.Name = "_sequenceTypeField";
			this._sequenceTypeField.NameString = "Sequenz-Typ:";
			this._sequenceTypeField.Size = new System.Drawing.Size(198, 20);
			this._sequenceTypeField.TabIndex = 42;
			this._sequenceTypeField.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
			this._sequenceTypeField.ValueChanged += new X2AddOnTechTreeEditor.Controls.NumberFieldControl.ValueChangedEventHandler(this._sequenceTypeField_ValueChanged);
			// 
			// _coord4Field
			// 
			this._coord4Field.Location = new System.Drawing.Point(213, 193);
			this._coord4Field.Name = "_coord4Field";
			this._coord4Field.NameString = "Koordinaten 4:";
			this._coord4Field.Size = new System.Drawing.Size(198, 20);
			this._coord4Field.TabIndex = 41;
			this._coord4Field.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
			this._coord4Field.ValueChanged += new X2AddOnTechTreeEditor.Controls.NumberFieldControl.ValueChangedEventHandler(this._coord4Field_ValueChanged);
			// 
			// _coord3Field
			// 
			this._coord3Field.Location = new System.Drawing.Point(9, 193);
			this._coord3Field.Name = "_coord3Field";
			this._coord3Field.NameString = "Koordinaten 3:";
			this._coord3Field.Size = new System.Drawing.Size(198, 20);
			this._coord3Field.TabIndex = 40;
			this._coord3Field.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
			this._coord3Field.ValueChanged += new X2AddOnTechTreeEditor.Controls.NumberFieldControl.ValueChangedEventHandler(this._coord3Field_ValueChanged);
			// 
			// _coord2Field
			// 
			this._coord2Field.Location = new System.Drawing.Point(213, 167);
			this._coord2Field.Name = "_coord2Field";
			this._coord2Field.NameString = "Koordinaten 2:";
			this._coord2Field.Size = new System.Drawing.Size(198, 20);
			this._coord2Field.TabIndex = 39;
			this._coord2Field.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
			this._coord2Field.ValueChanged += new X2AddOnTechTreeEditor.Controls.NumberFieldControl.ValueChangedEventHandler(this._coord2Field_ValueChanged);
			// 
			// _coord1Field
			// 
			this._coord1Field.Location = new System.Drawing.Point(9, 167);
			this._coord1Field.Name = "_coord1Field";
			this._coord1Field.NameString = "Koordinaten 1:";
			this._coord1Field.Size = new System.Drawing.Size(198, 20);
			this._coord1Field.TabIndex = 38;
			this._coord1Field.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
			this._coord1Field.ValueChanged += new X2AddOnTechTreeEditor.Controls.NumberFieldControl.ValueChangedEventHandler(this._coord1Field_ValueChanged);
			// 
			// _playerColorField
			// 
			this._playerColorField.Location = new System.Drawing.Point(213, 115);
			this._playerColorField.Name = "_playerColorField";
			this._playerColorField.NameString = "Feste Spielerfarbe: ";
			this._playerColorField.Size = new System.Drawing.Size(198, 20);
			this._playerColorField.TabIndex = 37;
			this._playerColorField.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
			this._playerColorField.ValueChanged += new X2AddOnTechTreeEditor.Controls.NumberFieldControl.ValueChangedEventHandler(this._playerColorField_ValueChanged);
			// 
			// _soundIDField
			// 
			this._soundIDField.Location = new System.Drawing.Point(9, 115);
			this._soundIDField.Name = "_soundIDField";
			this._soundIDField.NameString = "Sound-ID:";
			this._soundIDField.Size = new System.Drawing.Size(198, 20);
			this._soundIDField.TabIndex = 36;
			this._soundIDField.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
			this._soundIDField.ValueChanged += new X2AddOnTechTreeEditor.Controls.NumberFieldControl.ValueChangedEventHandler(this._soundIDField_ValueChanged);
			// 
			// _idField
			// 
			this._idField.Location = new System.Drawing.Point(9, 12);
			this._idField.Name = "_idField";
			this._idField.NameString = "ID: ";
			this._idField.Size = new System.Drawing.Size(103, 20);
			this._idField.TabIndex = 35;
			this._idField.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
			this._idField.ValueChanged += new X2AddOnTechTreeEditor.Controls.NumberFieldControl.ValueChangedEventHandler(this._idField_ValueChanged);
			// 
			// _angleCountField
			// 
			this._angleCountField.Location = new System.Drawing.Point(213, 89);
			this._angleCountField.Name = "_angleCountField";
			this._angleCountField.NameString = "Achsen-Anzahl:";
			this._angleCountField.Size = new System.Drawing.Size(198, 20);
			this._angleCountField.TabIndex = 34;
			this._angleCountField.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
			this._angleCountField.ValueChanged += new X2AddOnTechTreeEditor.Controls.NumberFieldControl.ValueChangedEventHandler(this._angleCountField_ValueChanged);
			// 
			// _replayDelayField
			// 
			this._replayDelayField.Location = new System.Drawing.Point(98, 140);
			this._replayDelayField.Name = "_replayDelayField";
			this._replayDelayField.NameString = "=> Verzögerung bei Wiederholung:";
			this._replayDelayField.Size = new System.Drawing.Size(222, 20);
			this._replayDelayField.TabIndex = 33;
			this._replayDelayField.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
			this._replayDelayField.ValueChanged += new X2AddOnTechTreeEditor.Controls.NumberFieldControl.ValueChangedEventHandler(this._replayDelayField_ValueChanged);
			// 
			// _replayCheckBox
			// 
			this._replayCheckBox.Location = new System.Drawing.Point(12, 141);
			this._replayCheckBox.Name = "_replayCheckBox";
			this._replayCheckBox.NameString = "Wiederholen?";
			this._replayCheckBox.Size = new System.Drawing.Size(133, 20);
			this._replayCheckBox.TabIndex = 32;
			this._replayCheckBox.Value = false;
			this._replayCheckBox.ValueChanged += new X2AddOnTechTreeEditor.Controls.CheckBoxFieldControl.ValueChangedEventHandler(this._replayCheckBox_ValueChanged);
			// 
			// _frameCountField
			// 
			this._frameCountField.Location = new System.Drawing.Point(213, 63);
			this._frameCountField.Name = "_frameCountField";
			this._frameCountField.NameString = "Frame-Anzahl (pro Achse):";
			this._frameCountField.Size = new System.Drawing.Size(198, 20);
			this._frameCountField.TabIndex = 31;
			this._frameCountField.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
			this._frameCountField.ValueChanged += new X2AddOnTechTreeEditor.Controls.NumberFieldControl.ValueChangedEventHandler(this._frameCountField_ValueChanged);
			// 
			// _frameRateField
			// 
			this._frameRateField.Location = new System.Drawing.Point(9, 89);
			this._frameRateField.Name = "_frameRateField";
			this._frameRateField.NameString = "Frame-Verweildauer:";
			this._frameRateField.Size = new System.Drawing.Size(198, 20);
			this._frameRateField.TabIndex = 30;
			this._frameRateField.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
			this._frameRateField.ValueChanged += new X2AddOnTechTreeEditor.Controls.NumberFieldControl.ValueChangedEventHandler(this._frameRateField_ValueChanged);
			// 
			// _slpField
			// 
			this._slpField.Location = new System.Drawing.Point(9, 63);
			this._slpField.Name = "_slpField";
			this._slpField.NameString = "SLP-ID: ";
			this._slpField.Size = new System.Drawing.Size(198, 20);
			this._slpField.TabIndex = 29;
			this._slpField.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
			this._slpField.ValueChanged += new X2AddOnTechTreeEditor.Controls.NumberFieldControl.ValueChangedEventHandler(this._slpField_ValueChanged);
			// 
			// _graphicListBox
			// 
			this._graphicListBox.Dock = System.Windows.Forms.DockStyle.Left;
			this._graphicListBox.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
			this._graphicListBox.FormattingEnabled = true;
			this._graphicListBox.Location = new System.Drawing.Point(0, 0);
			this._graphicListBox.Name = "_graphicListBox";
			this._graphicListBox.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
			this._graphicListBox.Size = new System.Drawing.Size(230, 606);
			this._graphicListBox.TabIndex = 3;
			this._graphicListBox.SelectedIndexChanged += new System.EventHandler(this._graphicListBox_SelectedIndexChanged);
			// 
			// _newGraphicButton
			// 
			this._newGraphicButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this._newGraphicButton.BackgroundImage = global::X2AddOnTechTreeEditor.Icons.NewGraphic;
			this._newGraphicButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
			this._newGraphicButton.FlatAppearance.BorderSize = 0;
			this._newGraphicButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this._newGraphicButton.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			this._newGraphicButton.Location = new System.Drawing.Point(9, 542);
			this._newGraphicButton.Name = "_newGraphicButton";
			this._newGraphicButton.Size = new System.Drawing.Size(32, 32);
			this._newGraphicButton.TabIndex = 22;
			this._newGraphicButton.UseVisualStyleBackColor = true;
			this._newGraphicButton.Click += new System.EventHandler(this._newGraphicButton_Click);
			// 
			// _deleteGraphicButton
			// 
			this._deleteGraphicButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this._deleteGraphicButton.BackgroundImage = global::X2AddOnTechTreeEditor.Icons.DeleteGraphic;
			this._deleteGraphicButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
			this._deleteGraphicButton.FlatAppearance.BorderSize = 0;
			this._deleteGraphicButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this._deleteGraphicButton.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			this._deleteGraphicButton.Location = new System.Drawing.Point(47, 542);
			this._deleteGraphicButton.Name = "_deleteGraphicButton";
			this._deleteGraphicButton.Size = new System.Drawing.Size(32, 32);
			this._deleteGraphicButton.TabIndex = 21;
			this._deleteGraphicButton.UseVisualStyleBackColor = true;
			this._deleteGraphicButton.Click += new System.EventHandler(this._deleteGraphicButton_Click);
			// 
			// EditGraphicsForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(1003, 651);
			this.Controls.Add(this._mainPanel);
			this.Controls.Add(this._graphicListBox);
			this.Controls.Add(this._bottomPanel);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.Name = "EditGraphicsForm";
			this.Text = "Grafiken bearbeiten";
			this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.EditGraphicsForm_FormClosing);
			this._bottomPanel.ResumeLayout(false);
			this._mainPanel.ResumeLayout(false);
			this._mainPanel.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this._attackSoundView)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this._deltaView)).EndInit();
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.Panel _bottomPanel;
		private System.Windows.Forms.Button _closeButton;
		private DoubleBufferedListBox _graphicListBox;
		private System.Windows.Forms.Panel _mainPanel;
		private System.Windows.Forms.Button _newGraphicButton;
		private System.Windows.Forms.Button _deleteGraphicButton;
		private System.Windows.Forms.TextBox _filterTextBox;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Button _saveButton;
		private System.Windows.Forms.TextBox _name1TextBox;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.TextBox _name2TextBox;
		private System.Windows.Forms.Label label3;
		private Controls.NumberFieldControl _slpField;
		private Controls.NumberFieldControl _frameRateField;
		private Controls.NumberFieldControl _frameCountField;
		private Controls.CheckBoxFieldControl _replayCheckBox;
		private Controls.NumberFieldControl _replayDelayField;
		private Controls.NumberFieldControl _angleCountField;
		private Controls.NumberFieldControl _idField;
		private Controls.NumberFieldControl _playerColorField;
		private Controls.NumberFieldControl _soundIDField;
		private Controls.NumberFieldControl _coord4Field;
		private Controls.NumberFieldControl _coord3Field;
		private Controls.NumberFieldControl _coord2Field;
		private Controls.NumberFieldControl _coord1Field;
		private Controls.NumberFieldControl _speedField;
		private Controls.NumberFieldControl _rainbowField;
		private Controls.NumberFieldControl _mirrorField;
		private Controls.NumberFieldControl _sequenceTypeField;
		private Controls.NumberFieldControl _unknown3Field;
		private Controls.NumberFieldControl _unknown2Field;
		private Controls.NumberFieldControl _unknown1Field;
		private Controls.NumberFieldControl _layerField;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.DataGridView _deltaView;
		private System.Windows.Forms.DataGridViewTextBoxColumn _deltaIDColumn;
		private System.Windows.Forms.DataGridViewTextBoxColumn _deltaXColumn;
		private System.Windows.Forms.DataGridViewTextBoxColumn _deltaYColumn;
		private System.Windows.Forms.DataGridView _attackSoundView;
		private System.Windows.Forms.DataGridViewTextBoxColumn _attackSoundAngleColumn;
		private System.Windows.Forms.DataGridViewTextBoxColumn _attackSound1IDColumn;
		private System.Windows.Forms.DataGridViewTextBoxColumn _attackSound1DelayColumn;
		private System.Windows.Forms.DataGridViewTextBoxColumn _attackSound2IDColumn;
		private System.Windows.Forms.DataGridViewTextBoxColumn _attackSound2DelayColumn;
		private System.Windows.Forms.DataGridViewTextBoxColumn _attackSound3IDColumn;
		private System.Windows.Forms.DataGridViewTextBoxColumn _attackSound3DelayColumn;
		private Controls.CheckBoxFieldControl _attackSoundField;
	}
}