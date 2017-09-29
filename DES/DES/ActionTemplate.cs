using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DES
{
    [Serializable]
    public struct ActionInfo {
        // kill count
        // damage done etc
    }
    [Serializable]
    public class ActionTemplate
    {

        public string Name;
        public string Description;

        public int TargetType;
        public int Range;
        public int Range2;

        public Dictionary<string, ActionVariable> LocalVars;
        public EffectTemplate[] Effects;
        public ActionRequirement[] Requirements;

        public ActionTemplate() {
            LocalVars = new Dictionary<string, ActionVariable>();
            Effects = new EffectTemplate[0];
            Requirements = new ActionRequirement[0];
        }

        public Action DispatchAction(Item[] targets, Character Caster) {
            Action dispatched = new Action(this, Caster, targets);
            foreach (Item target in targets) {
                target.ActionSys.ProcessAction(dispatched);
            }
            return dispatched;
        }
    }
}
