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

        public ActionTemplate workingData;

        public ActionOverviewPanel()
        {
            InitializeComponent();
            workingData = new ActionTemplate();
        }

        private void label1_Click(object sender, EventArgs e)
        {
        }

        public void Populate(ActionTemplate AT) {
            nameText.Text = AT.Name;
            descriptionText.Text = AT.Description;
            targetCombo.SelectedIndex = AT.TargetType;
            rangeNum.Value = AT.Range;
            altRangeNum.Value = AT.Range2;
            workingData = AT;
        }

        private void targetCombo_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!Visible) {
                return;
            }

            workingData.TargetType = targetCombo.SelectedIndex;

        }

        private void ActionOverviewPanel_Load(object sender, EventArgs e)
        {

        }

        private void NameUpdate(object sender, EventArgs e)
        {
            if (!Visible) {
                return;
            }
            workingData.Name = nameText.Text;
            Main.Current.workingDataNode.Text = nameText.Text;
        }

        private void descriptionText_TextChanged(object sender, EventArgs e)
        {
            if (!Visible) {
                return;
            }
            workingData.Description = descriptionText.Text;

        }

        private void rangeNum_ValueChanged(object sender, EventArgs e)
        {
            if (!Visible) {
                return;
            }

            workingData.Range = (int) rangeNum.Value;
        }

        private void altRangeNum_ValueChanged(object sender, EventArgs e)
        {
            workingData.Range2 = (int) altRangeNum.Value;
        }
    }
}
