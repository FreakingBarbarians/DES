using System;
using System.Reflection;
using System.Collections.Generic;
using System.Windows.Forms;
using DES;
using MyUtils;

namespace DESEditor
{
    public partial class EffectsPanel : UserControl
    {

        EffectTemplateWrapper workingData;

        public bool CodeDirty = false;

        public EffectsPanel()
        {
            InitializeComponent();
            workingData = new EffectTemplateWrapper();
        }

        private void procedureText_TextChanged(object sender, EventArgs e)
        {
            CodeDirty = true;
            SavedChangesLabel.Text = "Unsaved Changes";
            SavedChangesLabel.ForeColor = System.Drawing.Color.Red;
        }

        private void nameText_TextChanged(object sender, EventArgs e)
        {
            if (!Visible)
            {
                return;
            }
            workingData.EffectTemplate.Name = nameText.Text;
            Main.Current.workingDataNode.Text = nameText.Text;
        }

        private void keywordText_TextChanged(object sender, EventArgs e)
        {
        }

        public void Populate(EffectTemplateWrapper e)
        {
            workingData = e;
            keywordText.Text = Utils.ListToString(workingData.EffectTemplate.Keywords);
            procedureText.Text = workingData.RawCode;
            nameText.Text = workingData.EffectTemplate.Name;
        }

        private void EffectsPanel_Load(object sender, EventArgs e)
        {

        }

        private void CompileButton(object sender, EventArgs e)
        {
            Tuple<byte[][], int[][]> code;
            if (!Visible)
            {
                return;
            }
            try
            {
                code = VM.Compile(procedureText.Text);
            }
            catch (Exception error)
            {
                SavedChangesLabel.Text = error.Message;
                SavedChangesLabel.ForeColor = System.Drawing.Color.Red;
                return;
            }

            workingData.EffectTemplate.Instruction = code.Item1;
            workingData.EffectTemplate.ArgIndex = code.Item2;
            workingData.RawCode = procedureText.Text;
            CodeDirty = false;
            SavedChangesLabel.Text = "No Changes";
            SavedChangesLabel.ForeColor = System.Drawing.Color.Green;

        }
    }
}
