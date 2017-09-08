using System;
using System.Collections.Generic;
using System.Windows.Forms;
using DES;
using MyUtils;

namespace DESEditor
{
    public partial class EffectsPanel : UserControl
    {

        Effect workingData;

        public EffectsPanel()
        {
            InitializeComponent();
            workingData = new Effect();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click_1(object sender, EventArgs e)
        {

        }

        private void textBox1_LocationChanged(object sender, EventArgs e)
        {

        }

        private void procedureText_TextChanged(object sender, EventArgs e)
        {
            if (!Visible) {
                return;
            }
            string[] procedure = procedureText.Text.Split('\n');
            workingData.Procedure = procedure;

        }

        private void nameText_TextChanged(object sender, EventArgs e)
        {
            if (!Visible)
            {
                return;
            }
            workingData.Name = nameText.Text;
            Main.Current.workingDataNode.Text = nameText.Text;
        }

        private void keywordText_TextChanged(object sender, EventArgs e)
        {
            if (!Visible)
            {
                return;
            }
            string[] kw = keywordText.Text.Trim().Split(',');
            workingData.Keywords = new List<string>(kw);
        }

        public void Populate(Effect e) {
            workingData = e;
            keywordText.Text = Utils.ListToString(e.Keywords);
            procedureText.Text = "";
            for (int i = 0; i < e.Procedure.Length; i++) {
                procedureText.Text += e.Procedure[i];
                if (i != e.Procedure.Length - 1) {
                    procedureText.Text += '\n';
                }
            }
            nameText.Text = e.Name;
        }

    }
}
