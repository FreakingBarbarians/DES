namespace DESEditor
{
    partial class RequirementPanel
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
            this.label1 = new System.Windows.Forms.Label();
            this.requirementBox = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.amountNum = new System.Windows.Forms.NumericUpDown();
            this.consumeCheck = new System.Windows.Forms.CheckBox();
            ((System.ComponentModel.ISupportInitialize)(this.amountNum)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(3, 176);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(67, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Requirement";
            // 
            // requirementBox
            // 
            this.requirementBox.Location = new System.Drawing.Point(76, 173);
            this.requirementBox.Name = "requirementBox";
            this.requirementBox.Size = new System.Drawing.Size(277, 20);
            this.requirementBox.TabIndex = 1;
            this.requirementBox.TextChanged += new System.EventHandler(this.requirementBox_TextChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(360, 176);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(43, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Amount";
            // 
            // amountNum
            // 
            this.amountNum.Location = new System.Drawing.Point(409, 173);
            this.amountNum.Name = "amountNum";
            this.amountNum.Size = new System.Drawing.Size(120, 20);
            this.amountNum.TabIndex = 3;
            this.amountNum.ValueChanged += new System.EventHandler(this.amountNum_ValueChanged);
            // 
            // consumeCheck
            // 
            this.consumeCheck.AutoSize = true;
            this.consumeCheck.Location = new System.Drawing.Point(546, 176);
            this.consumeCheck.Name = "consumeCheck";
            this.consumeCheck.Size = new System.Drawing.Size(106, 17);
            this.consumeCheck.TabIndex = 4;
            this.consumeCheck.Text = "Should Consume";
            this.consumeCheck.UseVisualStyleBackColor = true;
            this.consumeCheck.CheckedChanged += new System.EventHandler(this.consumeCheck_CheckedChanged);
            // 
            // RequirementPanel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.Controls.Add(this.consumeCheck);
            this.Controls.Add(this.amountNum);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.requirementBox);
            this.Controls.Add(this.label1);
            this.MinimumSize = new System.Drawing.Size(722, 396);
            this.Name = "RequirementPanel";
            this.Size = new System.Drawing.Size(722, 396);
            this.Load += new System.EventHandler(this.RequirementPanel_Load);
            ((System.ComponentModel.ISupportInitialize)(this.amountNum)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox requirementBox;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.NumericUpDown amountNum;
        private System.Windows.Forms.CheckBox consumeCheck;
    }
}
