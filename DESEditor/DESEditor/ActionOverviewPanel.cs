using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DES;

namespace DESEditor
{
    public partial class ActionOverviewPanel : UserControl
    {

        public ActionTemplateWrapper workingData;

        public ActionOverviewPanel()
        {
            InitializeComponent();
            workingData = new ActionTemplateWrapper();
        }

        private void label1_Click(object sender, EventArgs e)
        {
        }

        public void Populate(ActionTemplateWrapper ATW) {
            nameText.Text = ATW.ActionTemplate.Name;
            descriptionText.Text = ATW.ActionTemplate.Description;
            targetCombo.SelectedIndex = ATW.ActionTemplate.TargetType;
            rangeNum.Value = ATW.ActionTemplate.Range;
            altRangeNum.Value = ATW.ActionTemplate.Range2;
            workingData = ATW;
        }

        private void targetCombo_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!Visible) {
                return;
            }

            workingData.ActionTemplate.TargetType = targetCombo.SelectedIndex;

        }

        private void ActionOverviewPanel_Load(object sender, EventArgs e)
        {

        }

        private void NameUpdate(object sender, EventArgs e)
        {
            if (!Visible) {
                return;
            }
            workingData.ActionTemplate.Name = nameText.Text;
            Main.Current.workingDataNode.Text = nameText.Text;
        }

        private void descriptionText_TextChanged(object sender, EventArgs e)
        {
            if (!Visible) {
                return;
            }
            workingData.ActionTemplate.Description = descriptionText.Text;

        }

        private void rangeNum_ValueChanged(object sender, EventArgs e)
        {
            if (!Visible) {
                return;
            }

            workingData.ActionTemplate.Range = (int) rangeNum.Value;
        }

        private void altRangeNum_ValueChanged(object sender, EventArgs e)
        {
            workingData.ActionTemplate.Range2 = (int) altRangeNum.Value;
        }
    }
}
