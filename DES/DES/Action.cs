using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DES
{
    [Serializable]
    public struct ActionDispatchInfo
    {
        // info
    }

    [Serializable]
    public struct ActionVariable {
        public string[] Keywords;
        public byte[] Data;
        public Type dataType;
    }

    [Serializable]
    public class ActionRequirement
    {
        public string Requirement;
        public int amount;
        public bool consume;

        public ActionRequirement()
        {
            Requirement = "";
            amount = 0;
            consume = false;
        }
    }

    [Serializable]
    public class Action
    {
        public Dictionary<string, ActionVariable> LocalVars;
        public ActionTemplate Source;
        public ActionDispatchInfo Info;
        public Item[] Targets;
        public Character Caster;

        public Action(ActionTemplate Source, Character Caster, Item[] Targets) {

            this.Source = Source;
            this.Caster = Caster;
            this.Targets = Targets;

            LocalVars = new Dictionary<string, ActionVariable>();
            foreach (string key in Source.LocalVars.Keys) {
                LocalVars.Add(key, Utils.CloneActionVariable(Source.LocalVars[key]));
            }
        }

        public void Run() {
            foreach (Item Target in Targets) {
                Target.ActionSys.ProcessAction(this);
            }
        }
    }
}