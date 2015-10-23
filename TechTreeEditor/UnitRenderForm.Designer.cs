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
			this.groupBox5 = new System.Windows.Forms.GroupBox();
			this._showFrameNumbersCheckBox = new System.Windows.Forms.CheckBox();
			this._showProjectilePointCheckBox = new System.Windows.Forms.CheckBox();
			this._showCenterPointCheckBox = new System.Windows.Forms.CheckBox();
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
			this._bottomPanel.SuspendLayout();
			this.groupBox5.SuspendLayout();
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
			this.SuspendLayout();
			// 
			// _bottomPanel
			// 
			resources.ApplyResources(this._bottomPanel, "_bottomPanel");
			this._bottomPanel.Controls.Add(this.groupBox5);
			this._bottomPanel.Controls.Add(this.groupBox4);
			this._bottomPanel.Controls.Add(this.groupBox3);
			this._bottomPanel.Controls.Add(this._closeButton);
			this._bottomPanel.Name = "_bottomPanel";
			// 
			// groupBox5
			// 
			resources.ApplyResources(this.groupBox5, "groupBox5");
			this.groupBox5.Controls.Add(this._showFrameNumbersCheckBox);
			this.groupBox5.Controls.Add(this._showProjectilePointCheckBox);
			this.groupBox5.Controls.Add(this._showCenterPointCheckBox);
			this.groupBox5.Name = "groupBox5";
			this.groupBox5.TabStop = false;
			// 
			// _showFrameNumbersCheckBox
			// 
			resources.ApplyResources(this._showFrameNumbersCheckBox, "_showFrameNumbersCheckBox");
			this._showFrameNumbersCheckBox.Name = "_showFrameNumbersCheckBox";
			this._showFrameNumbersCheckBox.UseVisualStyleBackColor = true;
			this._showFrameNumbersCheckBox.CheckedChanged += new System.EventHandler(this._showFrameNumbersCheckBox_CheckedChanged);
			// 
			// _showProjectilePointCheckBox
			// 
			resources.ApplyResources(this._showProjectilePointCheckBox, "_showProjectilePointCheckBox");
			this._showProjectilePointCheckBox.Name = "_showProjectilePointCheckBox";
			this._showProjectilePointCheckBox.UseVisualStyleBackColor = true;
			this._showProjectilePointCheckBox.CheckedChanged += new System.EventHandler(this._showProjectilePointCheckBox_CheckedChanged);
			// 
			// _showCenterPointCheckBox
			// 
			resources.ApplyResources(this._showCenterPointCheckBox, "_showCenterPointCheckBox");
			this._showCenterPointCheckBox.Name = "_showCenterPointCheckBox";
			this._showCenterPointCheckBox.UseVisualStyleBackColor = true;
			this._showCenterPointCheckBox.CheckedChanged += new System.EventHandler(this._showCenterPointCheckBox_CheckedChanged);
			// 
			// groupBox4
			// 
			resources.ApplyResources(this.groupBox4, "groupBox4");
			this.groupBox4.Controls.Add(this._radiusCustom2Field);
			this.groupBox4.Controls.Add(this._radiusCustom1Field);
			this.groupBox4.Controls.Add(this._radiusCustomCheckBox);
			this.groupBox4.Controls.Add(this._radiusSelectionCheckBox);
			this.groupBox4.Controls.Add(this._radiusEditorCheckBox);
			this.groupBox4.Controls.Add(this._radiusSizeCheckBox);
			this.groupBox4.Name = "groupBox4";
			this.groupBox4.TabStop = false;
			// 
			// _radiusCustom2Field
			// 
			resources.ApplyResources(this._radiusCustom2Field, "_radiusCustom2Field");
			this._radiusCustom2Field.DecimalPlaces = 2;
			this._radiusCustom2Field.Increment = new decimal(new int[] {
            1,
            0,
            0,
            131072});
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
			this._radiusCustom2Field.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
			this._radiusCustom2Field.ValueChanged += new System.EventHandler(this._radiusCustom2Field_ValueChanged);
			// 
			// _radiusCustom1Field
			// 
			resources.ApplyResources(this._radiusCustom1Field, "_radiusCustom1Field");
			this._radiusCustom1Field.DecimalPlaces = 2;
			this._radiusCustom1Field.Increment = new decimal(new int[] {
            1,
            0,
            0,
            131072});
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
			this._radiusCustom1Field.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
			this._radiusCustom1Field.ValueChanged += new System.EventHandler(this._radiusCustom1Field_ValueChanged);
			// 
			// _radiusCustomCheckBox
			// 
			resources.ApplyResources(this._radiusCustomCheckBox, "_radiusCustomCheckBox");
			this._radiusCustomCheckBox.Name = "_radiusCustomCheckBox";
			this._radiusCustomCheckBox.UseVisualStyleBackColor = true;
			this._radiusCustomCheckBox.CheckedChanged += new System.EventHandler(this._radiusCustomCheckBox_CheckedChanged);
			// 
			// _radiusSelectionCheckBox
			// 
			resources.ApplyResources(this._radiusSelectionCheckBox, "_radiusSelectionCheckBox");
			this._radiusSelectionCheckBox.Name = "_radiusSelectionCheckBox";
			this._radiusSelectionCheckBox.UseVisualStyleBackColor = true;
			this._radiusSelectionCheckBox.CheckedChanged += new System.EventHandler(this._radiusSelectionCheckBox_CheckedChanged);
			// 
			// _radiusEditorCheckBox
			// 
			resources.ApplyResources(this._radiusEditorCheckBox, "_radiusEditorCheckBox");
			this._radiusEditorCheckBox.Name = "_radiusEditorCheckBox";
			this._radiusEditorCheckBox.UseVisualStyleBackColor = true;
			this._radiusEditorCheckBox.CheckedChanged += new System.EventHandler(this._radiusEditorCheckBox_CheckedChanged);
			// 
			// _radiusSizeCheckBox
			// 
			resources.ApplyResources(this._radiusSizeCheckBox, "_radiusSizeCheckBox");
			this._radiusSizeCheckBox.Name = "_radiusSizeCheckBox";
			this._radiusSizeCheckBox.UseVisualStyleBackColor = true;
			this._radiusSizeCheckBox.CheckedChanged += new System.EventHandler(this._radiusSizeCheckBox_CheckedChanged);
			// 
			// groupBox3
			// 
			resources.ApplyResources(this.groupBox3, "groupBox3");
			this.groupBox3.Controls.Add(this._centerUnitButton);
			this.groupBox3.Controls.Add(this.label3);
			this.groupBox3.Controls.Add(this._angleField);
			this.groupBox3.Controls.Add(this.label2);
			this.groupBox3.Controls.Add(this._zoomField);
			this.groupBox3.Name = "groupBox3";
			this.groupBox3.TabStop = false;
			// 
			// _centerUnitButton
			// 
			resources.ApplyResources(this._centerUnitButton, "_centerUnitButton");
			this._centerUnitButton.Name = "_centerUnitButton";
			this._centerUnitButton.UseVisualStyleBackColor = true;
			this._centerUnitButton.Click += new System.EventHandler(this._centerUnitButton_Click);
			// 
			// label3
			// 
			resources.ApplyResources(this.label3, "label3");
			this.label3.Name = "label3";
			// 
			// _angleField
			// 
			resources.ApplyResources(this._angleField, "_angleField");
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
			this._angleField.ValueChanged += new System.EventHandler(this._angleField_ValueChanged);
			// 
			// label2
			// 
			resources.ApplyResources(this.label2, "label2");
			this.label2.Name = "label2";
			// 
			// _zoomField
			// 
			resources.ApplyResources(this._zoomField, "_zoomField");
			this._zoomField.DecimalPlaces = 1;
			this._zoomField.Increment = new decimal(new int[] {
            1,
            0,
            0,
            65536});
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
			this._zoomField.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
			this._zoomField.ValueChanged += new System.EventHandler(this._zoomField_ValueChanged);
			// 
			// _closeButton
			// 
			resources.ApplyResources(this._closeButton, "_closeButton");
			this._closeButton.Name = "_closeButton";
			this._closeButton.UseVisualStyleBackColor = true;
			this._closeButton.Click += new System.EventHandler(this._closeButton_Click);
			// 
			// _drawPanel
			// 
			resources.ApplyResources(this._drawPanel, "_drawPanel");
			this._drawPanel.BackColor = System.Drawing.Color.Black;
			this._drawPanel.Name = "_drawPanel";
			this._drawPanel.VSync = false;
			this._drawPanel.Paint += new System.Windows.Forms.PaintEventHandler(this._drawPanel_Paint);
			this._drawPanel.MouseDown += new System.Windows.Forms.MouseEventHandler(this._drawPanel_MouseDown);
			this._drawPanel.MouseMove += new System.Windows.Forms.MouseEventHandler(this._drawPanel_MouseMove);
			this._drawPanel.MouseUp += new System.Windows.Forms.MouseEventHandler(this._drawPanel_MouseUp);
			this._drawPanel.Resize += new System.EventHandler(this._drawPanel_Resize);
			// 
			// _topPanel
			// 
			resources.ApplyResources(this._topPanel, "_topPanel");
			this._topPanel.Controls.Add(this._stopButton);
			this._topPanel.Controls.Add(this._playButton);
			this._topPanel.Controls.Add(this.groupBox2);
			this._topPanel.Controls.Add(this.groupBox1);
			this._topPanel.Controls.Add(this._loadDRSButton);
			this._topPanel.Controls.Add(this._graphicsDRSButton);
			this._topPanel.Controls.Add(this.label1);
			this._topPanel.Controls.Add(this._graphicsDRSTextBox);
			this._topPanel.Name = "_topPanel";
			// 
			// _stopButton
			// 
			resources.ApplyResources(this._stopButton, "_stopButton");
			this._stopButton.FlatAppearance.BorderSize = 0;
			this._stopButton.Image = global::TechTreeEditor.Icons.RenderStop;
			this._stopButton.Name = "_stopButton";
			this._stopButton.UseVisualStyleBackColor = true;
			this._stopButton.Click += new System.EventHandler(this._stopButton_Click);
			// 
			// _playButton
			// 
			resources.ApplyResources(this._playButton, "_playButton");
			this._playButton.FlatAppearance.BorderSize = 0;
			this._playButton.Image = global::TechTreeEditor.Icons.RenderStart;
			this._playButton.Name = "_playButton";
			this._playButton.UseVisualStyleBackColor = true;
			this._playButton.Click += new System.EventHandler(this._playButton_Click);
			// 
			// groupBox2
			// 
			resources.ApplyResources(this.groupBox2, "groupBox2");
			this.groupBox2.Controls.Add(this._speedCustomField);
			this.groupBox2.Controls.Add(this._speedCustomButton);
			this.groupBox2.Controls.Add(this._speedFastButton);
			this.groupBox2.Controls.Add(this._speedNormalButton);
			this.groupBox2.Controls.Add(this._speedSlowButton);
			this.groupBox2.Name = "groupBox2";
			this.groupBox2.TabStop = false;
			// 
			// _speedCustomField
			// 
			resources.ApplyResources(this._speedCustomField, "_speedCustomField");
			this._speedCustomField.DecimalPlaces = 2;
			this._speedCustomField.Increment = new decimal(new int[] {
            1,
            0,
            0,
            131072});
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
			this._speedCustomField.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
			this._speedCustomField.ValueChanged += new System.EventHandler(this._speedCustomField_ValueChanged);
			// 
			// _speedCustomButton
			// 
			resources.ApplyResources(this._speedCustomButton, "_speedCustomButton");
			this._speedCustomButton.Name = "_speedCustomButton";
			this._speedCustomButton.UseVisualStyleBackColor = true;
			this._speedCustomButton.CheckedChanged += new System.EventHandler(this._speedCustomButton_CheckedChanged);
			// 
			// _speedFastButton
			// 
			resources.ApplyResources(this._speedFastButton, "_speedFastButton");
			this._speedFastButton.Name = "_speedFastButton";
			this._speedFastButton.UseVisualStyleBackColor = true;
			this._speedFastButton.CheckedChanged += new System.EventHandler(this._speedFastButton_CheckedChanged);
			// 
			// _speedNormalButton
			// 
			resources.ApplyResources(this._speedNormalButton, "_speedNormalButton");
			this._speedNormalButton.Name = "_speedNormalButton";
			this._speedNormalButton.UseVisualStyleBackColor = true;
			this._speedNormalButton.CheckedChanged += new System.EventHandler(this._speedNormalButton_CheckedChanged);
			// 
			// _speedSlowButton
			// 
			resources.ApplyResources(this._speedSlowButton, "_speedSlowButton");
			this._speedSlowButton.Checked = true;
			this._speedSlowButton.Name = "_speedSlowButton";
			this._speedSlowButton.TabStop = true;
			this._speedSlowButton.UseVisualStyleBackColor = true;
			this._speedSlowButton.CheckedChanged += new System.EventHandler(this._speedSlowButton_CheckedChanged);
			// 
			// groupBox1
			// 
			resources.ApplyResources(this.groupBox1, "groupBox1");
			this.groupBox1.Controls.Add(this._graSnowCheckBox);
			this.groupBox1.Controls.Add(this._graStandingButton);
			this.groupBox1.Controls.Add(this._graFallingButton);
			this.groupBox1.Controls.Add(this._graMovingButton);
			this.groupBox1.Controls.Add(this._graAttackingButton);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.TabStop = false;
			// 
			// _graSnowCheckBox
			// 
			resources.ApplyResources(this._graSnowCheckBox, "_graSnowCheckBox");
			this._graSnowCheckBox.Name = "_graSnowCheckBox";
			this._graSnowCheckBox.UseVisualStyleBackColor = true;
			this._graSnowCheckBox.CheckedChanged += new System.EventHandler(this._graSnowCheckBox_CheckedChanged);
			// 
			// _graStandingButton
			// 
			resources.ApplyResources(this._graStandingButton, "_graStandingButton");
			this._graStandingButton.Name = "_graStandingButton";
			this._graStandingButton.UseVisualStyleBackColor = true;
			this._graStandingButton.CheckedChanged += new System.EventHandler(this._graStandingButton_CheckedChanged);
			// 
			// _graFallingButton
			// 
			resources.ApplyResources(this._graFallingButton, "_graFallingButton");
			this._graFallingButton.Name = "_graFallingButton";
			this._graFallingButton.UseVisualStyleBackColor = true;
			this._graFallingButton.CheckedChanged += new System.EventHandler(this._graFallingButton_CheckedChanged);
			// 
			// _graMovingButton
			// 
			resources.ApplyResources(this._graMovingButton, "_graMovingButton");
			this._graMovingButton.Name = "_graMovingButton";
			this._graMovingButton.UseVisualStyleBackColor = true;
			this._graMovingButton.CheckedChanged += new System.EventHandler(this._graMovingButton_CheckedChanged);
			// 
			// _graAttackingButton
			// 
			resources.ApplyResources(this._graAttackingButton, "_graAttackingButton");
			this._graAttackingButton.Checked = true;
			this._graAttackingButton.Name = "_graAttackingButton";
			this._graAttackingButton.TabStop = true;
			this._graAttackingButton.UseVisualStyleBackColor = true;
			this._graAttackingButton.CheckedChanged += new System.EventHandler(this._graAttackingButton_CheckedChanged);
			// 
			// _loadDRSButton
			// 
			resources.ApplyResources(this._loadDRSButton, "_loadDRSButton");
			this._loadDRSButton.Name = "_loadDRSButton";
			this._loadDRSButton.UseVisualStyleBackColor = true;
			this._loadDRSButton.Click += new System.EventHandler(this._loadDRSButton_Click);
			// 
			// _graphicsDRSButton
			// 
			resources.ApplyResources(this._graphicsDRSButton, "_graphicsDRSButton");
			this._graphicsDRSButton.Name = "_graphicsDRSButton";
			this._graphicsDRSButton.UseVisualStyleBackColor = true;
			this._graphicsDRSButton.Click += new System.EventHandler(this._graphicsDRSButton_Click);
			// 
			// label1
			// 
			resources.ApplyResources(this.label1, "label1");
			this.label1.Name = "label1";
			// 
			// _graphicsDRSTextBox
			// 
			resources.ApplyResources(this._graphicsDRSTextBox, "_graphicsDRSTextBox");
			this._graphicsDRSTextBox.Name = "_graphicsDRSTextBox";
			// 
			// _openGraphicsDRSDialog
			// 
			resources.ApplyResources(this._openGraphicsDRSDialog, "_openGraphicsDRSDialog");
			// 
			// _renderTimer
			// 
			this._renderTimer.Tick += new System.EventHandler(this._renderTimer_Tick);
			// 
			// UnitRenderForm
			// 
			resources.ApplyResources(this, "$this");
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this._drawPanel);
			this.Controls.Add(this._topPanel);
			this.Controls.Add(this._bottomPanel);
			this.Name = "UnitRenderForm";
			this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.UnitRenderForm_FormClosing);
			this.Load += new System.EventHandler(this.UnitRenderForm_Load);
			this._bottomPanel.ResumeLayout(false);
			this.groupBox5.ResumeLayout(false);
			this.groupBox5.PerformLayout();
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
		private System.Windows.Forms.CheckBox _showFrameNumbersCheckBox;
	}
}