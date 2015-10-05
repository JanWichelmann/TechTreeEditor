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
			this.components = new System.ComponentModel.Container();
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(UnitRenderForm));
			this._bottomPanel = new System.Windows.Forms.Panel();
			this.groupBox4 = new System.Windows.Forms.GroupBox();
			this._radiusCustom2Field = new System.Windows.Forms.NumericUpDown();
			this._radiusCustom1Field = new System.Windows.Forms.NumericUpDown();
			this._radiusCustomCheckBox = new System.Windows.Forms.CheckBox();
			this._radiusSelectionCheckBox = new System.Windows.Forms.CheckBox();
			this._radiusEditorCheckBox = new System.Windows.Forms.CheckBox();
			this._radiusSizeCheckBox = new System.Windows.Forms.CheckBox();
			this.groupBox3 = new System.Windows.Forms.GroupBox();
			this._centerUnitButton = new System.Windows.Forms.Button();
			this.label3 = new System.Windows.Forms.Label();
			this._angleField = new System.Windows.Forms.NumericUpDown();
			this.label2 = new System.Windows.Forms.Label();
			this._zoomField = new System.Windows.Forms.NumericUpDown();
			this._closeButton = new System.Windows.Forms.Button();
			this._drawPanel = new OpenTK.GLControl();
			this._topPanel = new System.Windows.Forms.Panel();
			this._stopButton = new System.Windows.Forms.Button();
			this._playButton = new System.Windows.Forms.Button();
			this.groupBox2 = new System.Windows.Forms.GroupBox();
			this._speedCustomField = new System.Windows.Forms.NumericUpDown();
			this._speedCustomButton = new System.Windows.Forms.RadioButton();
			this._speedFastButton = new System.Windows.Forms.RadioButton();
			this._speedNormalButton = new System.Windows.Forms.RadioButton();
			this._speedSlowButton = new System.Windows.Forms.RadioButton();
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this._graSnowCheckBox = new System.Windows.Forms.CheckBox();
			this._graStandingButton = new System.Windows.Forms.RadioButton();
			this._graFallingButton = new System.Windows.Forms.RadioButton();
			this._graMovingButton = new System.Windows.Forms.RadioButton();
			this._graAttackingButton = new System.Windows.Forms.RadioButton();
			this._loadDRSButton = new System.Windows.Forms.Button();
			this._graphicsDRSButton = new System.Windows.Forms.Button();
			this.label1 = new System.Windows.Forms.Label();
			this._graphicsDRSTextBox = new System.Windows.Forms.TextBox();
			this._openGraphicsDRSDialog = new System.Windows.Forms.OpenFileDialog();
			this._renderTimer = new System.Windows.Forms.Timer(this.components);
			this.groupBox5 = new System.Windows.Forms.GroupBox();
			this._showCenterPointCheckBox = new System.Windows.Forms.CheckBox();
			this._showProjectilePointCheckBox = new System.Windows.Forms.CheckBox();
			this._bottomPanel.SuspendLayout();
			this.groupBox4.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this._radiusCustom2Field)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this._radiusCustom1Field)).BeginInit();
			this.groupBox3.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this._angleField)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this._zoomField)).BeginInit();
			this._topPanel.SuspendLayout();
			this.groupBox2.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this._speedCustomField)).BeginInit();
			this.groupBox1.SuspendLayout();
			this.groupBox5.SuspendLayout();
			this.SuspendLayout();
			// 
			// _bottomPanel
			// 
			this._bottomPanel.Controls.Add(this.groupBox5);
			this._bottomPanel.Controls.Add(this.groupBox4);
			this._bottomPanel.Controls.Add(this.groupBox3);
			this._bottomPanel.Controls.Add(this._closeButton);
			this._bottomPanel.Dock = System.Windows.Forms.DockStyle.Bottom;
			this._bottomPanel.Location = new System.Drawing.Point(0, 520);
			this._bottomPanel.Name = "_bottomPanel";
			this._bottomPanel.Size = new System.Drawing.Size(1129, 90);
			this._bottomPanel.TabIndex = 1;
			// 
			// groupBox4
			// 
			this.groupBox4.Controls.Add(this._radiusCustom2Field);
			this.groupBox4.Controls.Add(this._radiusCustom1Field);
			this.groupBox4.Controls.Add(this._radiusCustomCheckBox);
			this.groupBox4.Controls.Add(this._radiusSelectionCheckBox);
			this.groupBox4.Controls.Add(this._radiusEditorCheckBox);
			this.groupBox4.Controls.Add(this._radiusSizeCheckBox);
			this.groupBox4.Location = new System.Drawing.Point(352, 3);
			this.groupBox4.Name = "groupBox4";
			this.groupBox4.Size = new System.Drawing.Size(366, 46);
			this.groupBox4.TabIndex = 2;
			this.groupBox4.TabStop = false;
			this.groupBox4.Text = "Grenzen";
			// 
			// _radiusCustom2Field
			// 
			this._radiusCustom2Field.DecimalPlaces = 2;
			this._radiusCustom2Field.Enabled = false;
			this._radiusCustom2Field.Increment = new decimal(new int[] {
            1,
            0,
            0,
            131072});
			this._radiusCustom2Field.Location = new System.Drawing.Point(319, 19);
			this._radiusCustom2Field.Maximum = new decimal(new int[] {
            20,
            0,
            0,
            0});
			this._radiusCustom2Field.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            131072});
			this._radiusCustom2Field.Name = "_radiusCustom2Field";
			this._radiusCustom2Field.Size = new System.Drawing.Size(43, 20);
			this._radiusCustom2Field.TabIndex = 9;
			this._radiusCustom2Field.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
			this._radiusCustom2Field.ValueChanged += new System.EventHandler(this._radiusCustom2Field_ValueChanged);
			// 
			// _radiusCustom1Field
			// 
			this._radiusCustom1Field.DecimalPlaces = 2;
			this._radiusCustom1Field.Enabled = false;
			this._radiusCustom1Field.Increment = new decimal(new int[] {
            1,
            0,
            0,
            131072});
			this._radiusCustom1Field.Location = new System.Drawing.Point(270, 19);
			this._radiusCustom1Field.Maximum = new decimal(new int[] {
            20,
            0,
            0,
            0});
			this._radiusCustom1Field.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            131072});
			this._radiusCustom1Field.Name = "_radiusCustom1Field";
			this._radiusCustom1Field.Size = new System.Drawing.Size(43, 20);
			this._radiusCustom1Field.TabIndex = 8;
			this._radiusCustom1Field.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
			this._radiusCustom1Field.ValueChanged += new System.EventHandler(this._radiusCustom1Field_ValueChanged);
			// 
			// _radiusCustomCheckBox
			// 
			this._radiusCustomCheckBox.AutoSize = true;
			this._radiusCustomCheckBox.Location = new System.Drawing.Point(198, 21);
			this._radiusCustomCheckBox.Name = "_radiusCustomCheckBox";
			this._radiusCustomCheckBox.Size = new System.Drawing.Size(66, 17);
			this._radiusCustomCheckBox.TabIndex = 3;
			this._radiusCustomCheckBox.Text = "Manuell:";
			this._radiusCustomCheckBox.UseVisualStyleBackColor = true;
			this._radiusCustomCheckBox.CheckedChanged += new System.EventHandler(this._radiusCustomCheckBox_CheckedChanged);
			// 
			// _radiusSelectionCheckBox
			// 
			this._radiusSelectionCheckBox.AutoSize = true;
			this._radiusSelectionCheckBox.Location = new System.Drawing.Point(126, 21);
			this._radiusSelectionCheckBox.Name = "_radiusSelectionCheckBox";
			this._radiusSelectionCheckBox.Size = new System.Drawing.Size(66, 17);
			this._radiusSelectionCheckBox.TabIndex = 2;
			this._radiusSelectionCheckBox.Text = "Auswahl";
			this._radiusSelectionCheckBox.UseVisualStyleBackColor = true;
			this._radiusSelectionCheckBox.CheckedChanged += new System.EventHandler(this._radiusSelectionCheckBox_CheckedChanged);
			// 
			// _radiusEditorCheckBox
			// 
			this._radiusEditorCheckBox.AutoSize = true;
			this._radiusEditorCheckBox.Location = new System.Drawing.Point(67, 21);
			this._radiusEditorCheckBox.Name = "_radiusEditorCheckBox";
			this._radiusEditorCheckBox.Size = new System.Drawing.Size(53, 17);
			this._radiusEditorCheckBox.TabIndex = 1;
			this._radiusEditorCheckBox.Text = "Editor";
			this._radiusEditorCheckBox.UseVisualStyleBackColor = true;
			this._radiusEditorCheckBox.CheckedChanged += new System.EventHandler(this._radiusEditorCheckBox_CheckedChanged);
			// 
			// _radiusSizeCheckBox
			// 
			this._radiusSizeCheckBox.AutoSize = true;
			this._radiusSizeCheckBox.Location = new System.Drawing.Point(6, 21);
			this._radiusSizeCheckBox.Name = "_radiusSizeCheckBox";
			this._radiusSizeCheckBox.Size = new System.Drawing.Size(55, 17);
			this._radiusSizeCheckBox.TabIndex = 0;
			this._radiusSizeCheckBox.Text = "Größe";
			this._radiusSizeCheckBox.UseVisualStyleBackColor = true;
			this._radiusSizeCheckBox.CheckedChanged += new System.EventHandler(this._radiusSizeCheckBox_CheckedChanged);
			// 
			// groupBox3
			// 
			this.groupBox3.Controls.Add(this._centerUnitButton);
			this.groupBox3.Controls.Add(this.label3);
			this.groupBox3.Controls.Add(this._angleField);
			this.groupBox3.Controls.Add(this.label2);
			this.groupBox3.Controls.Add(this._zoomField);
			this.groupBox3.Location = new System.Drawing.Point(3, 3);
			this.groupBox3.Name = "groupBox3";
			this.groupBox3.Size = new System.Drawing.Size(344, 46);
			this.groupBox3.TabIndex = 1;
			this.groupBox3.TabStop = false;
			this.groupBox3.Text = "Rendering";
			// 
			// _centerUnitButton
			// 
			this._centerUnitButton.Location = new System.Drawing.Point(197, 17);
			this._centerUnitButton.Name = "_centerUnitButton";
			this._centerUnitButton.Size = new System.Drawing.Size(139, 23);
			this._centerUnitButton.TabIndex = 4;
			this._centerUnitButton.Text = "Auf Einheit zentrieren";
			this._centerUnitButton.UseVisualStyleBackColor = true;
			this._centerUnitButton.Click += new System.EventHandler(this._centerUnitButton_Click);
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.Location = new System.Drawing.Point(102, 22);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(40, 13);
			this.label3.TabIndex = 3;
			this.label3.Text = "Achse:";
			// 
			// _angleField
			// 
			this._angleField.Location = new System.Drawing.Point(148, 19);
			this._angleField.Maximum = new decimal(new int[] {
            17,
            0,
            0,
            0});
			this._angleField.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            -2147483648});
			this._angleField.Name = "_angleField";
			this._angleField.Size = new System.Drawing.Size(43, 20);
			this._angleField.TabIndex = 2;
			this._angleField.ValueChanged += new System.EventHandler(this._angleField_ValueChanged);
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(10, 22);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(37, 13);
			this.label2.TabIndex = 1;
			this.label2.Text = "Zoom:";
			// 
			// _zoomField
			// 
			this._zoomField.DecimalPlaces = 1;
			this._zoomField.Increment = new decimal(new int[] {
            1,
            0,
            0,
            65536});
			this._zoomField.Location = new System.Drawing.Point(53, 19);
			this._zoomField.Maximum = new decimal(new int[] {
            30,
            0,
            0,
            0});
			this._zoomField.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            65536});
			this._zoomField.Name = "_zoomField";
			this._zoomField.Size = new System.Drawing.Size(43, 20);
			this._zoomField.TabIndex = 0;
			this._zoomField.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
			this._zoomField.ValueChanged += new System.EventHandler(this._zoomField_ValueChanged);
			// 
			// _closeButton
			// 
			this._closeButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this._closeButton.Location = new System.Drawing.Point(958, 58);
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
			this._drawPanel.Location = new System.Drawing.Point(0, 100);
			this._drawPanel.Name = "_drawPanel";
			this._drawPanel.Size = new System.Drawing.Size(1129, 420);
			this._drawPanel.TabIndex = 2;
			this._drawPanel.VSync = false;
			this._drawPanel.Paint += new System.Windows.Forms.PaintEventHandler(this._drawPanel_Paint);
			this._drawPanel.MouseDown += new System.Windows.Forms.MouseEventHandler(this._drawPanel_MouseDown);
			this._drawPanel.MouseMove += new System.Windows.Forms.MouseEventHandler(this._drawPanel_MouseMove);
			this._drawPanel.MouseUp += new System.Windows.Forms.MouseEventHandler(this._drawPanel_MouseUp);
			this._drawPanel.Resize += new System.EventHandler(this._drawPanel_Resize);
			// 
			// _topPanel
			// 
			this._topPanel.Controls.Add(this._stopButton);
			this._topPanel.Controls.Add(this._playButton);
			this._topPanel.Controls.Add(this.groupBox2);
			this._topPanel.Controls.Add(this.groupBox1);
			this._topPanel.Controls.Add(this._loadDRSButton);
			this._topPanel.Controls.Add(this._graphicsDRSButton);
			this._topPanel.Controls.Add(this.label1);
			this._topPanel.Controls.Add(this._graphicsDRSTextBox);
			this._topPanel.Dock = System.Windows.Forms.DockStyle.Top;
			this._topPanel.Location = new System.Drawing.Point(0, 0);
			this._topPanel.Name = "_topPanel";
			this._topPanel.Size = new System.Drawing.Size(1129, 100);
			this._topPanel.TabIndex = 2;
			// 
			// _stopButton
			// 
			this._stopButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this._stopButton.Enabled = false;
			this._stopButton.FlatAppearance.BorderSize = 0;
			this._stopButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this._stopButton.Image = global::TechTreeEditor.Icons.RenderStop;
			this._stopButton.Location = new System.Drawing.Point(1083, 12);
			this._stopButton.Name = "_stopButton";
			this._stopButton.Size = new System.Drawing.Size(32, 32);
			this._stopButton.TabIndex = 22;
			this._stopButton.UseVisualStyleBackColor = true;
			this._stopButton.Click += new System.EventHandler(this._stopButton_Click);
			// 
			// _playButton
			// 
			this._playButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this._playButton.Enabled = false;
			this._playButton.FlatAppearance.BorderSize = 0;
			this._playButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this._playButton.Image = global::TechTreeEditor.Icons.RenderStart;
			this._playButton.Location = new System.Drawing.Point(1043, 12);
			this._playButton.Name = "_playButton";
			this._playButton.Size = new System.Drawing.Size(32, 32);
			this._playButton.TabIndex = 8;
			this._playButton.UseVisualStyleBackColor = true;
			this._playButton.Click += new System.EventHandler(this._playButton_Click);
			// 
			// groupBox2
			// 
			this.groupBox2.Controls.Add(this._speedCustomField);
			this.groupBox2.Controls.Add(this._speedCustomButton);
			this.groupBox2.Controls.Add(this._speedFastButton);
			this.groupBox2.Controls.Add(this._speedNormalButton);
			this.groupBox2.Controls.Add(this._speedSlowButton);
			this.groupBox2.Location = new System.Drawing.Point(393, 38);
			this.groupBox2.Name = "groupBox2";
			this.groupBox2.Size = new System.Drawing.Size(333, 51);
			this.groupBox2.TabIndex = 21;
			this.groupBox2.TabStop = false;
			this.groupBox2.Text = "Animation";
			// 
			// _speedCustomField
			// 
			this._speedCustomField.DecimalPlaces = 2;
			this._speedCustomField.Enabled = false;
			this._speedCustomField.Increment = new decimal(new int[] {
            1,
            0,
            0,
            131072});
			this._speedCustomField.Location = new System.Drawing.Point(282, 21);
			this._speedCustomField.Maximum = new decimal(new int[] {
            5,
            0,
            0,
            0});
			this._speedCustomField.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            131072});
			this._speedCustomField.Name = "_speedCustomField";
			this._speedCustomField.Size = new System.Drawing.Size(43, 20);
			this._speedCustomField.TabIndex = 7;
			this._speedCustomField.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
			this._speedCustomField.ValueChanged += new System.EventHandler(this._speedCustomField_ValueChanged);
			// 
			// _speedCustomButton
			// 
			this._speedCustomButton.AutoSize = true;
			this._speedCustomButton.Location = new System.Drawing.Point(211, 22);
			this._speedCustomButton.Name = "_speedCustomButton";
			this._speedCustomButton.Size = new System.Drawing.Size(65, 17);
			this._speedCustomButton.TabIndex = 6;
			this._speedCustomButton.Text = "Manuell:";
			this._speedCustomButton.UseVisualStyleBackColor = true;
			this._speedCustomButton.CheckedChanged += new System.EventHandler(this._speedCustomButton_CheckedChanged);
			// 
			// _speedFastButton
			// 
			this._speedFastButton.AutoSize = true;
			this._speedFastButton.Location = new System.Drawing.Point(145, 22);
			this._speedFastButton.Name = "_speedFastButton";
			this._speedFastButton.Size = new System.Drawing.Size(60, 17);
			this._speedFastButton.TabIndex = 5;
			this._speedFastButton.Text = "Schnell";
			this._speedFastButton.UseVisualStyleBackColor = true;
			this._speedFastButton.CheckedChanged += new System.EventHandler(this._speedFastButton_CheckedChanged);
			// 
			// _speedNormalButton
			// 
			this._speedNormalButton.AutoSize = true;
			this._speedNormalButton.Location = new System.Drawing.Point(80, 22);
			this._speedNormalButton.Name = "_speedNormalButton";
			this._speedNormalButton.Size = new System.Drawing.Size(58, 17);
			this._speedNormalButton.TabIndex = 4;
			this._speedNormalButton.Text = "Normal";
			this._speedNormalButton.UseVisualStyleBackColor = true;
			this._speedNormalButton.CheckedChanged += new System.EventHandler(this._speedNormalButton_CheckedChanged);
			// 
			// _speedSlowButton
			// 
			this._speedSlowButton.AutoSize = true;
			this._speedSlowButton.Checked = true;
			this._speedSlowButton.Location = new System.Drawing.Point(6, 22);
			this._speedSlowButton.Name = "_speedSlowButton";
			this._speedSlowButton.Size = new System.Drawing.Size(68, 17);
			this._speedSlowButton.TabIndex = 3;
			this._speedSlowButton.TabStop = true;
			this._speedSlowButton.Text = "Langsam";
			this._speedSlowButton.UseVisualStyleBackColor = true;
			this._speedSlowButton.CheckedChanged += new System.EventHandler(this._speedSlowButton_CheckedChanged);
			// 
			// groupBox1
			// 
			this.groupBox1.Controls.Add(this._graSnowCheckBox);
			this.groupBox1.Controls.Add(this._graStandingButton);
			this.groupBox1.Controls.Add(this._graFallingButton);
			this.groupBox1.Controls.Add(this._graMovingButton);
			this.groupBox1.Controls.Add(this._graAttackingButton);
			this.groupBox1.Location = new System.Drawing.Point(12, 38);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(375, 51);
			this.groupBox1.TabIndex = 20;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "Gezeigte Grafik";
			// 
			// _graSnowCheckBox
			// 
			this._graSnowCheckBox.AutoSize = true;
			this._graSnowCheckBox.Location = new System.Drawing.Point(302, 23);
			this._graSnowCheckBox.Name = "_graSnowCheckBox";
			this._graSnowCheckBox.Size = new System.Drawing.Size(63, 17);
			this._graSnowCheckBox.TabIndex = 26;
			this._graSnowCheckBox.Text = "Schnee";
			this._graSnowCheckBox.UseVisualStyleBackColor = true;
			this._graSnowCheckBox.CheckedChanged += new System.EventHandler(this._graSnowCheckBox_CheckedChanged);
			// 
			// _graStandingButton
			// 
			this._graStandingButton.Appearance = System.Windows.Forms.Appearance.Button;
			this._graStandingButton.Location = new System.Drawing.Point(80, 19);
			this._graStandingButton.Name = "_graStandingButton";
			this._graStandingButton.Size = new System.Drawing.Size(68, 24);
			this._graStandingButton.TabIndex = 25;
			this._graStandingButton.Text = "Stehend";
			this._graStandingButton.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			this._graStandingButton.UseVisualStyleBackColor = true;
			this._graStandingButton.CheckedChanged += new System.EventHandler(this._graStandingButton_CheckedChanged);
			// 
			// _graFallingButton
			// 
			this._graFallingButton.Appearance = System.Windows.Forms.Appearance.Button;
			this._graFallingButton.Location = new System.Drawing.Point(228, 19);
			this._graFallingButton.Name = "_graFallingButton";
			this._graFallingButton.Size = new System.Drawing.Size(68, 24);
			this._graFallingButton.TabIndex = 23;
			this._graFallingButton.Text = "Sterbend";
			this._graFallingButton.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			this._graFallingButton.UseVisualStyleBackColor = true;
			this._graFallingButton.CheckedChanged += new System.EventHandler(this._graFallingButton_CheckedChanged);
			// 
			// _graMovingButton
			// 
			this._graMovingButton.Appearance = System.Windows.Forms.Appearance.Button;
			this._graMovingButton.Location = new System.Drawing.Point(154, 19);
			this._graMovingButton.Name = "_graMovingButton";
			this._graMovingButton.Size = new System.Drawing.Size(68, 24);
			this._graMovingButton.TabIndex = 22;
			this._graMovingButton.Text = "Laufend";
			this._graMovingButton.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			this._graMovingButton.UseVisualStyleBackColor = true;
			this._graMovingButton.CheckedChanged += new System.EventHandler(this._graMovingButton_CheckedChanged);
			// 
			// _graAttackingButton
			// 
			this._graAttackingButton.Appearance = System.Windows.Forms.Appearance.Button;
			this._graAttackingButton.Checked = true;
			this._graAttackingButton.Location = new System.Drawing.Point(6, 19);
			this._graAttackingButton.Name = "_graAttackingButton";
			this._graAttackingButton.Size = new System.Drawing.Size(68, 24);
			this._graAttackingButton.TabIndex = 21;
			this._graAttackingButton.TabStop = true;
			this._graAttackingButton.Text = "Angreifend";
			this._graAttackingButton.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			this._graAttackingButton.UseVisualStyleBackColor = true;
			this._graAttackingButton.CheckedChanged += new System.EventHandler(this._graAttackingButton_CheckedChanged);
			// 
			// _loadDRSButton
			// 
			this._loadDRSButton.Location = new System.Drawing.Point(451, 10);
			this._loadDRSButton.Name = "_loadDRSButton";
			this._loadDRSButton.Size = new System.Drawing.Size(75, 23);
			this._loadDRSButton.TabIndex = 19;
			this._loadDRSButton.Text = "Laden!";
			this._loadDRSButton.UseVisualStyleBackColor = true;
			this._loadDRSButton.Click += new System.EventHandler(this._loadDRSButton_Click);
			// 
			// _graphicsDRSButton
			// 
			this._graphicsDRSButton.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			this._graphicsDRSButton.Location = new System.Drawing.Point(414, 10);
			this._graphicsDRSButton.Name = "_graphicsDRSButton";
			this._graphicsDRSButton.Size = new System.Drawing.Size(31, 23);
			this._graphicsDRSButton.TabIndex = 18;
			this._graphicsDRSButton.Text = "...";
			this._graphicsDRSButton.UseVisualStyleBackColor = true;
			this._graphicsDRSButton.Click += new System.EventHandler(this._graphicsDRSButton_Click);
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
			// _graphicsDRSTextBox
			// 
			this._graphicsDRSTextBox.Location = new System.Drawing.Point(96, 12);
			this._graphicsDRSTextBox.Name = "_graphicsDRSTextBox";
			this._graphicsDRSTextBox.Size = new System.Drawing.Size(312, 20);
			this._graphicsDRSTextBox.TabIndex = 0;
			// 
			// _openGraphicsDRSDialog
			// 
			this._openGraphicsDRSDialog.Filter = "DRS-Dateien (*.drs)|*.drs";
			this._openGraphicsDRSDialog.Title = "Graphics-DRS-Datei öffnen...";
			// 
			// _renderTimer
			// 
			this._renderTimer.Tick += new System.EventHandler(this._renderTimer_Tick);
			// 
			// groupBox5
			// 
			this.groupBox5.Controls.Add(this._showProjectilePointCheckBox);
			this.groupBox5.Controls.Add(this._showCenterPointCheckBox);
			this.groupBox5.Location = new System.Drawing.Point(724, 3);
			this.groupBox5.Name = "groupBox5";
			this.groupBox5.Size = new System.Drawing.Size(396, 46);
			this.groupBox5.TabIndex = 23;
			this.groupBox5.TabStop = false;
			this.groupBox5.Text = "Markierungen";
			// 
			// _showCenterPointCheckBox
			// 
			this._showCenterPointCheckBox.AutoSize = true;
			this._showCenterPointCheckBox.Location = new System.Drawing.Point(6, 21);
			this._showCenterPointCheckBox.Name = "_showCenterPointCheckBox";
			this._showCenterPointCheckBox.Size = new System.Drawing.Size(81, 17);
			this._showCenterPointCheckBox.TabIndex = 0;
			this._showCenterPointCheckBox.Text = "Ankerpunkt";
			this._showCenterPointCheckBox.UseVisualStyleBackColor = true;
			this._showCenterPointCheckBox.CheckedChanged += new System.EventHandler(this._showCenterPointCheckBox_CheckedChanged);
			// 
			// _showProjectilePointCheckBox
			// 
			this._showProjectilePointCheckBox.AutoSize = true;
			this._showProjectilePointCheckBox.Enabled = false;
			this._showProjectilePointCheckBox.Location = new System.Drawing.Point(93, 21);
			this._showProjectilePointCheckBox.Name = "_showProjectilePointCheckBox";
			this._showProjectilePointCheckBox.Size = new System.Drawing.Size(99, 17);
			this._showProjectilePointCheckBox.TabIndex = 1;
			this._showProjectilePointCheckBox.Text = "Projektil-Spawn";
			this._showProjectilePointCheckBox.UseVisualStyleBackColor = true;
			this._showProjectilePointCheckBox.CheckedChanged += new System.EventHandler(this._showProjectilePointCheckBox_CheckedChanged);
			// 
			// UnitRenderForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(1129, 610);
			this.Controls.Add(this._drawPanel);
			this.Controls.Add(this._topPanel);
			this.Controls.Add(this._bottomPanel);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.Name = "UnitRenderForm";
			this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
			this.Text = "Einheiten-Renderer";
			this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.UnitRenderForm_FormClosing);
			this.Load += new System.EventHandler(this.UnitRenderForm_Load);
			this._bottomPanel.ResumeLayout(false);
			this.groupBox4.ResumeLayout(false);
			this.groupBox4.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this._radiusCustom2Field)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this._radiusCustom1Field)).EndInit();
			this.groupBox3.ResumeLayout(false);
			this.groupBox3.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this._angleField)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this._zoomField)).EndInit();
			this._topPanel.ResumeLayout(false);
			this._topPanel.PerformLayout();
			this.groupBox2.ResumeLayout(false);
			this.groupBox2.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this._speedCustomField)).EndInit();
			this.groupBox1.ResumeLayout(false);
			this.groupBox1.PerformLayout();
			this.groupBox5.ResumeLayout(false);
			this.groupBox5.PerformLayout();
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
		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.RadioButton _graStandingButton;
		private System.Windows.Forms.RadioButton _graFallingButton;
		private System.Windows.Forms.RadioButton _graMovingButton;
		private System.Windows.Forms.RadioButton _graAttackingButton;
		private System.Windows.Forms.GroupBox groupBox2;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.NumericUpDown _zoomField;
		private System.Windows.Forms.RadioButton _speedFastButton;
		private System.Windows.Forms.RadioButton _speedNormalButton;
		private System.Windows.Forms.RadioButton _speedSlowButton;
		private System.Windows.Forms.NumericUpDown _speedCustomField;
		private System.Windows.Forms.RadioButton _speedCustomButton;
		private System.Windows.Forms.Button _playButton;
		private System.Windows.Forms.Button _stopButton;
		private System.Windows.Forms.Timer _renderTimer;
		private System.Windows.Forms.GroupBox groupBox3;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.NumericUpDown _angleField;
		private System.Windows.Forms.Button _centerUnitButton;
		private System.Windows.Forms.GroupBox groupBox4;
		private System.Windows.Forms.CheckBox _radiusCustomCheckBox;
		private System.Windows.Forms.CheckBox _radiusSelectionCheckBox;
		private System.Windows.Forms.CheckBox _radiusEditorCheckBox;
		private System.Windows.Forms.CheckBox _radiusSizeCheckBox;
		private System.Windows.Forms.NumericUpDown _radiusCustom1Field;
		private System.Windows.Forms.NumericUpDown _radiusCustom2Field;
		private System.Windows.Forms.CheckBox _graSnowCheckBox;
		private System.Windows.Forms.GroupBox groupBox5;
		private System.Windows.Forms.CheckBox _showCenterPointCheckBox;
		private System.Windows.Forms.CheckBox _showProjectilePointCheckBox;
	}
}