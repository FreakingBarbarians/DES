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

        public Dictionary<string, ActionTemplate> Data;
        public Dictionary<string, Effect> effects;
        public Dictionary<string, ActionRequirement> Requirements;
        public static Main Current;
        public TreeView TV;
        public TreeNode workingDataNode;
        public TreeNode contextDataNode;

        IDPool Ids;

        public Main()
        {
            InitializeComponent();
            Data = new Dictionary<string, ActionTemplate>();
            effects = new Dictionary<string, Effect>();
            Requirements = new Dictionary<string, ActionRequirement>();
            AOPanel = new ActionOverviewPanel();
            RQPanel = new RequirementPanel();
            EFPanel = new EffectsPanel();
            Ids = new IDPool(1000);
            Current = this;
            TV = tv;
            workingDataNode = new TreeNode(); // dummy vars;
            saveFileDialog1.DefaultExt = ".ACT";
            openFileDialog1.DefaultExt = ".ACT";
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
            BinaryFormatter f = new BinaryFormatter();
            Stream s = openFileDialog1.OpenFile();
            // maybe add some security here.
            ActionTemplate act = (ActionTemplate) f.Deserialize(s);

            TreeNode t = tv.Nodes.Add("New Action");
            t.Text = act.Name;
            t.Name = Ids.Get().ToString();
            Console.Out.WriteLine(t.Name);
            Data.Add(t.Name, act);

            TreeNode Req;
            TreeNode Int;
            TreeNode Tick;
            TreeNode Trig;
            TreeNode End;
            (Req = t.Nodes.Add("Requirements")).ForeColor = Color.Blue;
            (Int = t.Nodes.Add("InitialEffects")).ForeColor = Color.Blue;
            (Tick = t.Nodes.Add("TickingEffects")).ForeColor = Color.Blue;
            (Trig = t.Nodes.Add("TriggeredEffects")).ForeColor = Color.Blue;
            (End = t.Nodes.Add("EndEffects")).ForeColor = Color.Blue;

            foreach (ActionRequirement r in act.Requirements) {
                TreeNode node = Req.Nodes.Add("");
                node.Name = Ids.Get().ToString();
                node.Text = r.Requirement;
                Requirements.Add(node.Name, r);
            }

            foreach (Effect eff in act.InitialEffects) {
                TreeNode node = Int.Nodes.Add(eff.Name);
                node.Name = Ids.Get().ToString();
                effects.Add(node.Name, eff);
            }

            foreach (Effect eff in act.TickingEffects) {
                TreeNode node = Tick.Nodes.Add(eff.Name);
                node.Name = Ids.Get().ToString();
                effects.Add(node.Name, eff);
            }

            foreach (Effect eff in act.TriggeredEffects)
            {
                TreeNode node = Trig.Nodes.Add(eff.Name);
                node.Name = Ids.Get().ToString();
                effects.Add(node.Name, eff);
            }

            foreach (Effect eff in act.EndEffects)
            {
                TreeNode node = End.Nodes.Add(eff.Name);
                node.Name = Ids.Get().ToString();
                effects.Add(node.Name, eff);
            }
        }

        private void NewAction(object sender, EventArgs e)
        {
            HideAllDynamicMenus();

            TreeNode t = tv.Nodes.Add("New Action");
            t.Name = Ids.Get().ToString();
            Console.Out.WriteLine(t.Name);
            t.Nodes.Add("Requirements").ForeColor = Color.Blue;
            t.Nodes.Add("InitialEffects").ForeColor = Color.Blue;
            t.Nodes.Add("TickingEffects").ForeColor = Color.Blue;
            t.Nodes.Add("TriggeredEffects").ForeColor = Color.Blue;
            t.Nodes.Add("EndEffects").ForeColor = Color.Blue;
            
            ActionTemplate AT = new ActionTemplate();
            AT.Name = "New Action";
            AT.Description = "Type here!";
            Data.Add(t.Name, AT);
            tv.SelectedNode = t;
            workingDataNode = t;

            ShowAO(AT);
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

        private void ShowAO(ActionTemplate AT) {
            AOPanel.Parent = DynamicSpace;
            AOPanel.Dock = DockStyle.Fill;
            AOPanel.Populate(AT);
            AOPanel.Show();
        }

        private void ShowEP(Effect EF) {
            EFPanel.Parent = DynamicSpace;
            EFPanel.Dock = DockStyle.Fill;
            EFPanel.Populate(EF);
            EFPanel.Show();
        }

        private void ShowRQ(ActionRequirement RQ) {
            RQPanel.Parent = DynamicSpace;
            RQPanel.Dock = DockStyle.Fill;
            RQPanel.Populate(RQ);
            RQPanel.Show();
        }

        private void addRequirementToolStripMenuItem_Click(object sender, EventArgs e)
        {
            HideAllDynamicMenus();
            HideAllContextMenus();
            ActionTemplate at = Data[contextDataNode.Parent.Name];
            TreeNode t = contextDataNode.Nodes.Add("NewRequirement");
            t.Name = Ids.Get().ToString();
            ActionRequirement rq = new ActionRequirement();
            rq.Requirement = "new requirement";
            rq.amount = 0;
            rq.consume = false;
            Requirements.Add(t.Name, rq);
            at.Requirements.Add(rq);
            ShowRQ(rq);
        }

        private void testAddToolStripMenuItem_Click(object sender, EventArgs e)
        {
            HideAllDynamicMenus();
            HideAllContextMenus();
            TreeNode t = contextDataNode.Nodes.Add("NewEffect");
            workingDataNode = t;
            t.Name = Ids.Get().ToString();
            Effect eff;
            if (t.Parent.Text == "InitialEffects")
            {
                eff = new Effect();
                Data[t.Parent.Parent.Name].InitialEffects.Add(eff);

            }
            else if (t.Parent.Text == "TickingEffects") {
                eff = new Effect();
                Data[t.Parent.Parent.Name].TickingEffects.Add(eff);

            }
            else if (t.Parent.Text == "TriggeredEffects") {
                eff = new Effect();
                Data[t.Parent.Parent.Name].TriggeredEffects.Add(eff);
            }
            else {
                eff = new Effect();
                Data[t.Parent.Parent.Name].EndEffects.Add(eff);
            }
            effects.Add(t.Name, eff);
            ShowEP(eff);
        }

        private void deleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ActionTemplate owner = Data[contextDataNode.Parent.Parent.Name];
            Effect toBeDeleted = effects[contextDataNode.Name];
            effects.Remove(contextDataNode.Name);
            string type = contextDataNode.Parent.Text;
            switch (type) {
                case "InitialEffects":
                    owner.InitialEffects.Remove(toBeDeleted);
                    break;
                case "TickingEffects":
                    owner.TickingEffects.Remove(toBeDeleted);
                    break;
                case "TriggeredEffects":
                    owner.TriggeredEffects.Remove(toBeDeleted);
                    break;
                case "EndEffects":
                    owner.EndEffects.Remove(toBeDeleted);
                    break;
            }
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
            
            saveFileDialog1.ShowDialog();
        }

        private void saveFileDialog1_FileOk(object sender, CancelEventArgs e)
        {
            ActionTemplate AT = Data[tv.SelectedNode.Name];
            Stream s = saveFileDialog1.OpenFile();
            BinaryFormatter bn = new BinaryFormatter();
            bn.Serialize(s, AT);
            s.Close();
        }


    }
}
