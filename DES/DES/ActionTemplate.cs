using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DES
{
    public struct ActionInfo {
        // kill count
        // damage done etc
    }

    public class ActionTemplate
    {
        public Dictionary<string, ActionVariable> LocalVars;
        public EffectTemplate[] Effects;

        public ActionTemplate() {
            LocalVars = new Dictionary<string, ActionVariable>();
            Effects = new EffectTemplate[0];
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
