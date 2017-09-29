namespace DESEditor
{
    partial class EffectsPanel
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
            this.keywordText = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.procedureText = new System.Windows.Forms.RichTextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.nameText = new System.Windows.Forms.TextBox();
            this.compileButton = new System.Windows.Forms.Button();
            this.SavedChangesLabel = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(8, 40);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Keywords";
            // 
            // keywordText
            // 
            this.keywordText.Location = new System.Drawing.Point(67, 37);
            this.keywordText.Name = "keywordText";
            this.keywordText.Size = new System.Drawing.Size(639, 20);
            this.keywordText.TabIndex = 1;
            this.keywordText.TextChanged += new System.EventHandler(this.keywordText_TextChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(8, 64);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(56, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Procedure";
            // 
            // procedureText
            // 
            this.procedureText.Font = new System.Drawing.Font("Courier New", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.procedureText.Location = new System.Drawing.Point(11, 80);
            this.procedureText.Name = "procedureText";
            this.procedureText.Size = new System.Drawing.Size(695, 262);
            this.procedureText.TabIndex = 3;
            this.procedureText.Text = "";
            this.procedureText.TextChanged += new System.EventHandler(this.procedureText_TextChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(8, 12);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(35, 13);
            this.label3.TabIndex = 4;
            this.label3.Text = "Name";
            // 
            // nameText
            // 
            this.nameText.Location = new System.Drawing.Point(67, 9);
            this.nameText.Name = "nameText";
            this.nameText.Size = new System.Drawing.Size(125, 20);
            this.nameText.TabIndex = 5;
            this.nameText.TextChanged += new System.EventHandler(this.nameText_TextChanged);
            // 
            // compileButton
            // 
            this.compileButton.Location = new System.Drawing.Point(530, 348);
            this.compileButton.Name = "compileButton";
            this.compileButton.Size = new System.Drawing.Size(176, 32);
            this.compileButton.TabIndex = 6;
            this.compileButton.Text = "Compile";
            this.compileButton.UseVisualStyleBackColor = true;
            this.compileButton.Click += new System.EventHandler(this.CompileButton);
            // 
            // SavedChangesLabel
            // 
            this.SavedChangesLabel.AutoSize = true;
            this.SavedChangesLabel.ForeColor = System.Drawing.Color.Green;
            this.SavedChangesLabel.Location = new System.Drawing.Point(8, 358);
            this.SavedChangesLabel.Name = "SavedChangesLabel";
            this.SavedChangesLabel.Size = new System.Drawing.Size(66, 13);
            this.SavedChangesLabel.TabIndex = 7;
            this.SavedChangesLabel.Text = "No Changes";
            // 
            // EffectsPanel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.Controls.Add(this.SavedChangesLabel);
            this.Controls.Add(this.compileButton);
            this.Controls.Add(this.nameText);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.procedureText);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.keywordText);
            this.Controls.Add(this.label1);
            this.MinimumSize = new System.Drawing.Size(722, 396);
            this.Name = "EffectsPanel";
            this.Size = new System.Drawing.Size(722, 396);
            this.Load += new System.EventHandler(this.EffectsPanel_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox keywordText;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.RichTextBox procedureText;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox nameText;
        private System.Windows.Forms.Button compileButton;
        private System.Windows.Forms.Label SavedChangesLabel;
    }
}
