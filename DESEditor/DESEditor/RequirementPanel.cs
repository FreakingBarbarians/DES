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
    public partial class RequirementPanel : UserControl
    {

        ActionRequirement workingData;

        public RequirementPanel()
        {
            InitializeComponent();
        }

        private void RequirementPanel_Load(object sender, EventArgs e)
        {
            
        }

        public void Populate(ActionRequirement RQ) {
            requirementBox.Text = RQ.Requirement;
            amountNum.Value = RQ.amount;
            consumeCheck.Checked = RQ.consume;
            workingData = RQ;
        }

        private void requirementBox_TextChanged(object sender, EventArgs e)
        {
            if (!Visible) {
                return;
            }
            workingData.Requirement = requirementBox.Text;
            Main.Current.workingDataNode.Text = requirementBox.Text;
        }

        private void amountNum_ValueChanged(object sender, EventArgs e)
        {
            if (!Visible)
            {
                return;
            }
            workingData.amount = (int)amountNum.Value;
        }

        private void consumeCheck_CheckedChanged(object sender, EventArgs e)
        {
            if (!Visible)
            {
                return;
            }
            workingData.consume = consumeCheck.Checked;
        }
    }
}
