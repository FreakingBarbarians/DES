namespace DESEditor
{
    partial class ActionOverviewPanel
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
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.nameLabel = new System.Windows.Forms.Label();
            this.nameText = new System.Windows.Forms.TextBox();
            this.targetTypeLabel = new System.Windows.Forms.Label();
            this.targetCombo = new System.Windows.Forms.ComboBox();
            this.rangeLabel = new System.Windows.Forms.Label();
            this.rangeNum = new System.Windows.Forms.NumericUpDown();
            this.altRangeLabel = new System.Windows.Forms.Label();
            this.altRangeNum = new System.Windows.Forms.NumericUpDown();
            this.descriptionLabel = new System.Windows.Forms.Label();
            this.descriptionText = new System.Windows.Forms.RichTextBox();
            ((System.ComponentModel.ISupportInitialize)(this.rangeNum)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.altRangeNum)).BeginInit();
            this.SuspendLayout();
            // 
            // nameLabel
            // 
            this.nameLabel.AutoSize = true;
            this.nameLabel.Location = new System.Drawing.Point(3, 20);
            this.nameLabel.Name = "nameLabel";
            this.nameLabel.Size = new System.Drawing.Size(35, 13);
            this.nameLabel.TabIndex = 0;
            this.nameLabel.Text = "Name";
            this.nameLabel.Click += new System.EventHandler(this.label1_Click);
            // 
            // nameText
            // 
            this.nameText.Location = new System.Drawing.Point(44, 17);
            this.nameText.Name = "nameText";
            this.nameText.Size = new System.Drawing.Size(635, 20);
            this.nameText.TabIndex = 1;
            this.nameText.TextChanged += new System.EventHandler(this.NameUpdate);
            // 
            // targetTypeLabel
            // 
            this.targetTypeLabel.AutoSize = true;
            this.targetTypeLabel.Location = new System.Drawing.Point(3, 56);
            this.targetTypeLabel.Name = "targetTypeLabel";
            this.targetTypeLabel.Size = new System.Drawing.Size(65, 13);
            this.targetTypeLabel.TabIndex = 2;
            this.targetTypeLabel.Text = "Target Type";
            // 
            // targetCombo
            // 
            this.targetCombo.AccessibleRole = System.Windows.Forms.AccessibleRole.None;
            this.targetCombo.FormattingEnabled = true;
            this.targetCombo.Items.AddRange(new object[] {
            "Touch",
            "Self",
            "Target",
            "Radius",
            "Burst",
            "Cone",
            "Line",
            "Wall",
            "Polygon"});
            this.targetCombo.Location = new System.Drawing.Point(74, 53);
            this.targetCombo.Name = "targetCombo";
            this.targetCombo.Size = new System.Drawing.Size(121, 21);
            this.targetCombo.TabIndex = 3;
            this.targetCombo.SelectedIndexChanged += new System.EventHandler(this.targetCombo_SelectedIndexChanged);
            // 
            // rangeLabel
            // 
            this.rangeLabel.AutoSize = true;
            this.rangeLabel.ForeColor = System.Drawing.SystemColors.ControlText;
            this.rangeLabel.Location = new System.Drawing.Point(3, 90);
            this.rangeLabel.Name = "rangeLabel";
            this.rangeLabel.Size = new System.Drawing.Size(48, 13);
            this.rangeLabel.TabIndex = 4;
            this.rangeLabel.Text = "Range 1";
            // 
            // rangeNum
            // 
            this.rangeNum.Location = new System.Drawing.Point(57, 88);
            this.rangeNum.Name = "rangeNum";
            this.rangeNum.Size = new System.Drawing.Size(120, 20);
            this.rangeNum.TabIndex = 5;
            this.rangeNum.ValueChanged += new System.EventHandler(this.rangeNum_ValueChanged);
            // 
            // altRangeLabel
            // 
            this.altRangeLabel.AutoSize = true;
            this.altRangeLabel.ForeColor = System.Drawing.SystemColors.ControlText;
            this.altRangeLabel.Location = new System.Drawing.Point(3, 119);
            this.altRangeLabel.Name = "altRangeLabel";
            this.altRangeLabel.Size = new System.Drawing.Size(48, 13);
            this.altRangeLabel.TabIndex = 6;
            this.altRangeLabel.Text = "Range 1";
            // 
            // altRangeNum
            // 
            this.altRangeNum.Location = new System.Drawing.Point(57, 117);
            this.altRangeNum.Name = "altRangeNum";
            this.altRangeNum.Size = new System.Drawing.Size(120, 20);
            this.altRangeNum.TabIndex = 7;
            this.altRangeNum.ValueChanged += new System.EventHandler(this.altRangeNum_ValueChanged);
            // 
            // descriptionLabel
            // 
            this.descriptionLabel.AutoSize = true;
            this.descriptionLabel.Location = new System.Drawing.Point(211, 56);
            this.descriptionLabel.Name = "descriptionLabel";
            this.descriptionLabel.Size = new System.Drawing.Size(60, 13);
            this.descriptionLabel.TabIndex = 8;
            this.descriptionLabel.Text = "Description";
            // 
            // descriptionText
            // 
            this.descriptionText.Location = new System.Drawing.Point(214, 72);
            this.descriptionText.Name = "descriptionText";
            this.descriptionText.Size = new System.Drawing.Size(465, 291);
            this.descriptionText.TabIndex = 9;
            this.descriptionText.Text = "";
            this.descriptionText.TextChanged += new System.EventHandler(this.descriptionText_TextChanged);
            // 
            // ActionOverviewPanel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.Controls.Add(this.descriptionText);
            this.Controls.Add(this.descriptionLabel);
            this.Controls.Add(this.altRangeNum);
            this.Controls.Add(this.altRangeLabel);
            this.Controls.Add(this.rangeNum);
            this.Controls.Add(this.rangeLabel);
            this.Controls.Add(this.targetCombo);
            this.Controls.Add(this.targetTypeLabel);
            this.Controls.Add(this.nameText);
            this.Controls.Add(this.nameLabel);
            this.MinimumSize = new System.Drawing.Size(722, 396);
            this.Name = "ActionOverviewPanel";
            this.Size = new System.Drawing.Size(722, 396);
            this.Load += new System.EventHandler(this.ActionOverviewPanel_Load);
            ((System.ComponentModel.ISupportInitialize)(this.rangeNum)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.altRangeNum)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label nameLabel;
        private System.Windows.Forms.TextBox nameText;
        private System.Windows.Forms.Label targetTypeLabel;
        private System.Windows.Forms.ComboBox targetCombo;
        private System.Windows.Forms.Label rangeLabel;
        private System.Windows.Forms.NumericUpDown rangeNum;
        private System.Windows.Forms.Label altRangeLabel;
        private System.Windows.Forms.NumericUpDown altRangeNum;
        private System.Windows.Forms.Label descriptionLabel;
        private System.Windows.Forms.RichTextBox descriptionText;
    }
}
