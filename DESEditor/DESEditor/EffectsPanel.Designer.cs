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
            this.label1.Click += new System.EventHandler(this.label1_Click_1);
            // 
            // keywordText
            // 
            this.keywordText.Location = new System.Drawing.Point(67, 37);
            this.keywordText.Name = "keywordText";
            this.keywordText.Size = new System.Drawing.Size(639, 20);
            this.keywordText.TabIndex = 1;
            this.keywordText.LocationChanged += new System.EventHandler(this.textBox1_LocationChanged);
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
            this.procedureText.Location = new System.Drawing.Point(11, 80);
            this.procedureText.Name = "procedureText";
            this.procedureText.Size = new System.Drawing.Size(695, 301);
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
            // EffectsPanel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.Controls.Add(this.nameText);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.procedureText);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.keywordText);
            this.Controls.Add(this.label1);
            this.MinimumSize = new System.Drawing.Size(722, 396);
            this.Name = "EffectsPanel";
            this.Size = new System.Drawing.Size(722, 396);
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
    }
}
