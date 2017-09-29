using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Runtime.Serialization;
using System.IO;   
using System.Runtime.Serialization.Formatters.Binary;
using DES;

namespace DESEditor
{

    public partial class Main : Form
    {

        ActionOverviewPanel AOPanel;
        RequirementPanel RQPanel;
        EffectsPanel EFPanel;

        public Dictionary<string, ActionTemplateWrapper> Data;
        public Dictionary<string, EffectTemplateWrapper> effects;
        public Dictionary<string, ActionRequirementWrapper> Requirements;
        public static Main Current;
        public TreeView TV;
        public TreeNode workingDataNode;
        public TreeNode contextDataNode;

        public static VM vm;

        IDPool Ids;

        public Main()
        {
            InitializeComponent();
            Data = new Dictionary<string, ActionTemplateWrapper>();
            effects = new Dictionary<string, EffectTemplateWrapper>();
            Requirements = new Dictionary<string, ActionRequirementWrapper>();
            AOPanel = new ActionOverviewPanel();
            RQPanel = new RequirementPanel();
            EFPanel = new EffectsPanel();
            Ids = new IDPool(1000);
            Current = this;
            TV = tv;
            workingDataNode = new TreeNode(); // dummy vars;
            saveActionDialog.DefaultExt = ".ACT";
            openFileDialog1.DefaultExt = ".ACT";

            vm = new VM();
        }

        private void Main_Load(object sender, EventArgs e)
        {

        }

        private void menuStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void folderBrowserDialog1_HelpRequest(object sender, EventArgs e)
        {

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void TreeViewMouseClick(object sender, EventArgs e)
        {
            TreeNode node = tv.GetNodeAt(((MouseEventArgs)e).Location);

            if (((MouseEventArgs)e).Button == MouseButtons.Right)
            {
                HideAllContextMenus();
                switch (node.Level) {
                    case 0:
                        break;
                    case 1:
                        if (node.Text.Equals("Requirements"))
                        {
                            AddRequirementContext.Show(Cursor.Position);
                            contextDataNode = node;
                            tv.SelectedNode = node;
                        }
                        else
                        {
                            // display effect contextual menu
                            AddEffectContext.Show(Cursor.Position);
                            tv.SelectedNode = node;
                            contextDataNode = node;
                        }
                        break;
                    case 2:
                        if (node.Parent.Text != "Requirements")
                        {
                            EffectContext.Show(Cursor.Position);
                            contextDataNode = node;
                            tv.SelectedNode = node;
                        }
                        else {

                        }
                        break;
                }

            }
            else
            {
                if (node != null && node.Level == 0)
                {
                    HideAllDynamicMenus();
                    workingDataNode = node;
                    ShowAO(Data[node.Name]);
                }
                else if (node != null && node.Level == 2) {
                    if (node.Parent.Text != "Requirements")
                    {
                        HideAllDynamicMenus();
                        workingDataNode = node;
                        ShowEP(effects[node.Name]);
                    }
                    else {
                        HideAllDynamicMenus();
                        workingDataNode = node;
                        ShowRQ(Requirements[node.Name]);
                    }
                }
            }
        }

        private void loadActionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            openFileDialog1.ShowDialog();
        }

        private void openFileDialog1_FileOk(object sender, CancelEventArgs e)
        {
            //BinaryFormatter f = new BinaryFormatter();
            //Stream s = openFileDialog1.OpenFile();
            //// maybe add some security here.
            //ActionTemplate act = (ActionTemplate) f.Deserialize(s);

            //TreeNode t = tv.Nodes.Add("New Action");
            //t.Text = act.Name;
            //t.Name = Ids.Get().ToString();
            //Console.Out.WriteLine(t.Name);
            //Data.Add(t.Name, act);

            //TreeNode Req;
            //TreeNode Effects;

            //(Req = t.Nodes.Add("Requirements")).ForeColor = Color.Blue;
            //(Effects = t.Nodes.Add("Effects")).ForeColor = Color.Blue;
            //foreach (ActionRequirement r in act.Requirements) {
            //    TreeNode node = Req.Nodes.Add("");
            //    node.Name = Ids.Get().ToString();
            //    node.Text = r.Requirement;
            //    Requirements.Add(node.Name, r);
            //}

            //foreach (EffectTemplate eft in act.Effects) {
            //    TreeNode node = Effects.Nodes.Add("");
            //    node.Name = Ids.Get().ToString();
            //    node.Text = eft.Name;
            //    effects.Add(node.Name, eft);
            //}

        }

        private void NewAction(object sender, EventArgs e)
        {
            HideAllDynamicMenus();
            HideAllContextMenus();

            TreeNode t = tv.Nodes.Add("New Action");
            t.Name = Ids.Get().ToString();
            Console.Out.WriteLine(t.Name);
            t.Nodes.Add("Requirements").ForeColor = Color.Blue;
            t.Nodes.Add("Effects").ForeColor = Color.Blue;
            
            ActionTemplate AT = new ActionTemplate();
            ActionTemplateWrapper ATW = new ActionTemplateWrapper();
            ATW.ActionTemplate = AT;

            AT.Name = "New Action";
            AT.Description = "Type here!";
            Data.Add(t.Name, ATW);
            tv.SelectedNode = t;
            workingDataNode = t;
            ShowAO(ATW);
        }

        private void HideAllContextMenus() {
            AddEffectContext.Hide();
            AddRequirementContext.Hide();
            EffectContext.Hide();
        }

        private void HideAllDynamicMenus() {
            AOPanel.Hide();
            EFPanel.Hide();
            RQPanel.Hide();
        }

        private void ShowAO(ActionTemplateWrapper ATW) {
            AOPanel.Parent = DynamicSpace;
            AOPanel.Dock = DockStyle.Fill;
            AOPanel.Populate(ATW);
            AOPanel.Show();
        }

        private void ShowEP(EffectTemplateWrapper EF) {
            EFPanel.Parent = DynamicSpace;
            EFPanel.Dock = DockStyle.Fill;
            EFPanel.Populate(EF);
            EFPanel.Show();
        }

        private void ShowRQ(ActionRequirementWrapper RQW) {
            RQPanel.Parent = DynamicSpace;
            RQPanel.Dock = DockStyle.Fill;
            RQPanel.Populate(RQW);
            RQPanel.Show();
        }

        private void AddRequirementToolstripItem(object sender, EventArgs e)
        {
            HideAllDynamicMenus();
            HideAllContextMenus();
            ActionTemplateWrapper ATW = Data[contextDataNode.Parent.Name];
            TreeNode t = contextDataNode.Nodes.Add("NewRequirement");
            t.Name = Ids.Get().ToString();

            ActionRequirement RQ = new ActionRequirement();
            RQ.Requirement = "new requirement";
            RQ.amount = 0;
            RQ.consume = false;

            ActionRequirementWrapper RQW = new ActionRequirementWrapper();
            RQW.ActionRequirement = RQ;

            Requirements.Add(t.Name, RQW);
            List<ActionRequirement> tempList = new List<ActionRequirement>(ATW.ActionTemplate.Requirements);
            tempList.Add(RQ);
            ATW.ActionTemplate.Requirements = tempList.ToArray();
            ShowRQ(RQW);
        }

        private void AddEffectToolstripItem(object sender, EventArgs e)
        {
            HideAllDynamicMenus();
            HideAllContextMenus();

            // action owner of new effect
            ActionTemplateWrapper ownerWrapper = Data[contextDataNode.Parent.Name];
            // new node in tree
            TreeNode t = contextDataNode.Nodes.Add("NewEffect");
            workingDataNode = t;
            // get id for this new node
            t.Name = Ids.Get().ToString();
            EffectTemplate eff = new EffectTemplate();

            EffectTemplateWrapper efp = new EffectTemplateWrapper();
            efp.EffectTemplate = eff;
            
            // add wrapper to dictionary
            effects.Add(t.Name, efp);

            // update owner action
            List<EffectTemplate> tempList = new List<EffectTemplate>(ownerWrapper.ActionTemplate.Effects);
            tempList.Add(eff);
            ownerWrapper.ActionTemplate.Effects = tempList.ToArray();

            // update owner wrapper
            ownerWrapper.effects.Add(efp);

            ShowEP(efp);
        }

        private void DeleteEffectToolstripItem(object sender, EventArgs e)
        {
            ActionTemplateWrapper ownerWrapper = Data[contextDataNode.Parent.Parent.Name];
            EffectTemplateWrapper toBeDeleted = effects[contextDataNode.Name];
            effects.Remove(contextDataNode.Name);

            List<EffectTemplate> tempList = new List<EffectTemplate>(ownerWrapper.ActionTemplate.Effects);
            tempList.Remove(toBeDeleted.EffectTemplate);
            ownerWrapper.ActionTemplate.Effects = tempList.ToArray();

            Ids.Free(int.Parse(contextDataNode.Name));
            contextDataNode.Parent.Nodes.Remove(contextDataNode);
        }

        private void saveCurrentActionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            TreeNode t = tv.SelectedNode;

            if (t == null) {
                return;
            }

            while (t.Level > 0) {
                t = t.Parent;
            }
            tv.SelectedNode = t;
            
            saveActionDialog.ShowDialog();
        }

        private void exportCurrentAction(object sender, EventArgs e)
        {
            TreeNode t = tv.SelectedNode;

            if (t == null)
            {
                return;
            }

            while (t.Level > 0)
            {
                t = t.Parent;
            }
            tv.SelectedNode = t;

            exportActionDialog.ShowDialog();
        }

        private void saveFileDialog1_FileOk(object sender, CancelEventArgs e)
        {
            saveActionDialog.DefaultExt = ".ACTE";
            ActionTemplateWrapper ATW = Data[tv.SelectedNode.Name];
            Stream s = saveActionDialog.OpenFile();
            BinaryFormatter bn = new BinaryFormatter();
            bn.Serialize(s, ATW);
            s.Close();
        }

        private void exportActionDialog_FileOk(object sender, CancelEventArgs e)
        {
            saveActionDialog.DefaultExt = ".ACT";
            ActionTemplateWrapper ATW = Data[tv.SelectedNode.Name];
            Stream s = exportActionDialog.OpenFile();
            BinaryFormatter bn = new BinaryFormatter();
            bn.Serialize(s, ATW.ActionTemplate);
            s.Close();
        }

        private void DynamicSpace_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
