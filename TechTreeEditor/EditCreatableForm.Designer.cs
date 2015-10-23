namespace TechTreeEditor
{
	partial class EditCreatableForm
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(EditCreatableForm));
			this._otherUnitsGroupBox = new System.Windows.Forms.GroupBox();
			this.label6 = new System.Windows.Forms.Label();
			this._deadUnitComboBox = new System.Windows.Forms.ComboBox();
			this.label4 = new System.Windows.Forms.Label();
			this._dropSite2ComboBox = new System.Windows.Forms.ComboBox();
			this._dropSite1ComboBox = new System.Windows.Forms.ComboBox();
			this.label5 = new System.Windows.Forms.Label();
			this.label3 = new System.Windows.Forms.Label();
			this._trackUnitComboBox = new System.Windows.Forms.ComboBox();
			this._projDuplUnitComboBox = new System.Windows.Forms.ComboBox();
			this.label2 = new System.Windows.Forms.Label();
			this.label1 = new System.Windows.Forms.Label();
			this._projUnitComboBox = new System.Windows.Forms.ComboBox();
			this._childrenGroupBox = new System.Windows.Forms.GroupBox();
			this._childrenView = new System.Windows.Forms.DataGridView();
			this._closeButton = new System.Windows.Forms.Button();
			this._childrenViewButtonColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this._childrenViewChildColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this._otherUnitsGroupBox.SuspendLayout();
			this._childrenGroupBox.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this._childrenView)).BeginInit();
			this.SuspendLayout();
			// 
			// _otherUnitsGroupBox
			// 
			resources.ApplyResources(this._otherUnitsGroupBox, "_otherUnitsGroupBox");
			this._otherUnitsGroupBox.Controls.Add(this.label6);
			this._otherUnitsGroupBox.Controls.Add(this._deadUnitComboBox);
			this._otherUnitsGroupBox.Controls.Add(this.label4);
			this._otherUnitsGroupBox.Controls.Add(this._dropSite2ComboBox);
			this._otherUnitsGroupBox.Controls.Add(this._dropSite1ComboBox);
			this._otherUnitsGroupBox.Controls.Add(this.label5);
			this._otherUnitsGroupBox.Controls.Add(this.label3);
			this._otherUnitsGroupBox.Controls.Add(this._trackUnitComboBox);
			this._otherUnitsGroupBox.Controls.Add(this._projDuplUnitComboBox);
			this._otherUnitsGroupBox.Controls.Add(this.label2);
			this._otherUnitsGroupBox.Controls.Add(this.label1);
			this._otherUnitsGroupBox.Controls.Add(this._projUnitComboBox);
			this._otherUnitsGroupBox.Name = "_otherUnitsGroupBox";
			this._otherUnitsGroupBox.TabStop = false;
			// 
			// label6
			// 
			resources.ApplyResources(this.label6, "label6");
			this.label6.Name = "label6";
			// 
			// _deadUnitComboBox
			// 
			resources.ApplyResources(this._deadUnitComboBox, "_deadUnitComboBox");
			this._deadUnitComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this._deadUnitComboBox.FormattingEnabled = true;
			this._deadUnitComboBox.Name = "_deadUnitComboBox";
			this._deadUnitComboBox.Sorted = true;
			this._deadUnitComboBox.SelectedIndexChanged += new System.EventHandler(this._deadUnitComboBox_SelectedIndexChanged);
			// 
			// label4
			// 
			resources.ApplyResources(this.label4, "label4");
			this.label4.Name = "label4";
			// 
			// _dropSite2ComboBox
			// 
			resources.ApplyResources(this._dropSite2ComboBox, "_dropSite2ComboBox");
			this._dropSite2ComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this._dropSite2ComboBox.FormattingEnabled = true;
			this._dropSite2ComboBox.Name = "_dropSite2ComboBox";
			this._dropSite2ComboBox.Sorted = true;
			this._dropSite2ComboBox.SelectedIndexChanged += new System.EventHandler(this._dropSite2ComboBox_SelectedIndexChanged);
			// 
			// _dropSite1ComboBox
			// 
			resources.ApplyResources(this._dropSite1ComboBox, "_dropSite1ComboBox");
			this._dropSite1ComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this._dropSite1ComboBox.FormattingEnabled = true;
			this._dropSite1ComboBox.Name = "_dropSite1ComboBox";
			this._dropSite1ComboBox.Sorted = true;
			this._dropSite1ComboBox.SelectedIndexChanged += new System.EventHandler(this._dropSite1ComboBox_SelectedIndexChanged);
			// 
			// label5
			// 
			resources.ApplyResources(this.label5, "label5");
			this.label5.Name = "label5";
			// 
			// label3
			// 
			resources.ApplyResources(this.label3, "label3");
			this.label3.Name = "label3";
			// 
			// _trackUnitComboBox
			// 
			resources.ApplyResources(this._trackUnitComboBox, "_trackUnitComboBox");
			this._trackUnitComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this._trackUnitComboBox.FormattingEnabled = true;
			this._trackUnitComboBox.Name = "_trackUnitComboBox";
			this._trackUnitComboBox.Sorted = true;
			this._trackUnitComboBox.SelectedIndexChanged += new System.EventHandler(this._trackUnitComboBox_SelectedIndexChanged);
			// 
			// _projDuplUnitComboBox
			// 
			resources.ApplyResources(this._projDuplUnitComboBox, "_projDuplUnitComboBox");
			this._projDuplUnitComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this._projDuplUnitComboBox.FormattingEnabled = true;
			this._projDuplUnitComboBox.Name = "_projDuplUnitComboBox";
			this._projDuplUnitComboBox.Sorted = true;
			this._projDuplUnitComboBox.SelectedIndexChanged += new System.EventHandler(this._projDuplUnitComboBox_SelectedIndexChanged);
			// 
			// label2
			// 
			resources.ApplyResources(this.label2, "label2");
			this.label2.Name = "label2";
			// 
			// label1
			// 
			resources.ApplyResources(this.label1, "label1");
			this.label1.Name = "label1";
			// 
			// _projUnitComboBox
			// 
			resources.ApplyResources(this._projUnitComboBox, "_projUnitComboBox");
			this._projUnitComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this._projUnitComboBox.FormattingEnabled = true;
			this._projUnitComboBox.Name = "_projUnitComboBox";
			this._projUnitComboBox.Sorted = true;
			this._projUnitComboBox.SelectedIndexChanged += new System.EventHandler(this._projUnitComboBox_SelectedIndexChanged);
			// 
			// _childrenGroupBox
			// 
			resources.ApplyResources(this._childrenGroupBox, "_childrenGroupBox");
			this._childrenGroupBox.Controls.Add(this._childrenView);
			this._childrenGroupBox.Name = "_childrenGroupBox";
			this._childrenGroupBox.TabStop = false;
			// 
			// _childrenView
			// 
			resources.ApplyResources(this._childrenView, "_childrenView");
			this._childrenView.AllowUserToAddRows = false;
			this._childrenView.AllowUserToDeleteRows = false;
			this._childrenView.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
			this._childrenView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			this._childrenView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this._childrenViewButtonColumn,
            this._childrenViewChildColumn});
			this._childrenView.Name = "_childrenView";
			this._childrenView.CellValidating += new System.Windows.Forms.DataGridViewCellValidatingEventHandler(this._childrenView_CellValidating);
			// 
			// _closeButton
			// 
			resources.ApplyResources(this._closeButton, "_closeButton");
			this._closeButton.Name = "_closeButton";
			this._closeButton.UseVisualStyleBackColor = true;
			this._closeButton.Click += new System.EventHandler(this._closeButton_Click);
			// 
			// _childrenViewButtonColumn
			// 
			this._childrenViewButtonColumn.FillWeight = 20F;
			resources.ApplyResources(this._childrenViewButtonColumn, "_childrenViewButtonColumn");
			this._childrenViewButtonColumn.Name = "_childrenViewButtonColumn";
			// 
			// _childrenViewChildColumn
			// 
			this._childrenViewChildColumn.FillWeight = 80F;
			resources.ApplyResources(this._childrenViewChildColumn, "_childrenViewChildColumn");
			this._childrenViewChildColumn.Name = "_childrenViewChildColumn";
			this._childrenViewChildColumn.ReadOnly = true;
			// 
			// EditCreatableForm
			// 
			resources.ApplyResources(this, "$this");
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this._closeButton);
			this.Controls.Add(this._childrenGroupBox);
			this.Controls.Add(this._otherUnitsGroupBox);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
			this.MaximizeBox = false;
			this.Name = "EditCreatableForm";
			this._otherUnitsGroupBox.ResumeLayout(false);
			this._otherUnitsGroupBox.PerformLayout();
			this._childrenGroupBox.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this._childrenView)).EndInit();
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.GroupBox _otherUnitsGroupBox;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.ComboBox _trackUnitComboBox;
		private System.Windows.Forms.ComboBox _projDuplUnitComboBox;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.ComboBox _projUnitComboBox;
		private System.Windows.Forms.GroupBox _childrenGroupBox;
		private System.Windows.Forms.DataGridView _childrenView;
		private System.Windows.Forms.Button _closeButton;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.ComboBox _dropSite2ComboBox;
		private System.Windows.Forms.ComboBox _dropSite1ComboBox;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.Label label6;
		private System.Windows.Forms.ComboBox _deadUnitComboBox;
		private System.Windows.Forms.DataGridViewTextBoxColumn _childrenViewButtonColumn;
		private System.Windows.Forms.DataGridViewTextBoxColumn _childrenViewChildColumn;
	}
}