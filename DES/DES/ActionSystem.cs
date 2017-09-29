using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DES
{
    [Serializable]
    public partial class ActionEffectSystem // maybe we just combine them?
    {

        public List<Action> ActiveActions;
        public List<Action> RemovedAction;
        public List<Effect> RemovedEffects;

        public List<Effect> RanEffects;

        public Item Owner;

        Dictionary<Effect, Action> EffectActionMap;
        Dictionary<Action, int> ActionEffectCounter;

        public ActionEffectSystem(Item owner) {

            this.Owner = owner;

            ActiveActions = new List<Action>();
            RemovedAction = new List<Action>();
            RemovedEffects = new List<Effect>();
            RanEffects = new List<Effect>();

            EffectActionMap = new Dictionary<Effect, Action>();
            ActionEffectCounter = new Dictionary<Action, int>();
        }

        public void ProcessAction(Action a) {

            foreach (EffectTemplate EffectTemplate in a.Source.Effects) {
                Effect e = new Effect(EffectTemplate, a);

                EffectActionMap[e] = a;
                if (ActionEffectCounter.ContainsKey(a))
                {
                    ActionEffectCounter[a]++;
                }
                else {
                    ActionEffectCounter[a] = 1;
                }
            }

            // add action to a dictionary
            // run add action listener
            // if action is still in the dictionary send to effects
            // notify that the action is done

        }

        public void RemoveEffect(Effect e) {
            // notify that the action is done

            e.ExecutionMode = ExecutionMode.END;
            VM.Current.Execute(e, this.Owner);

            RemovedEffects.Add(e);
            ActionEffectCounter[e.Owner]--;
            if (ActionEffectCounter[e.Owner] < 1) {
                ActionEffectCounter.Remove(e.Owner);
                RemoveAction(e.Owner);
            }
        }

        private void DeleteEffect(Effect e) {
            EffectActionMap.Remove(e);
        }

        public void RemoveAction(Action a) {
            RemovedAction.Add(a);
        }

        private void DeleteAction(Action a) {
            ActiveActions.Remove(a);
            ActionEffectCounter.Remove(a);
        }

        public void ProcessEffects() {
            foreach (Action removeThis in RemovedAction) {
                DeleteAction(removeThis);
            }
            RemovedAction.Clear();

            foreach (Effect e in EffectActionMap.Keys) {
                if (!RemovedEffects.Contains(e) && !RanEffects.Contains(e)) {
                    VM.Current.Execute(e, this.Owner);
                    RanEffects.Add(e);
                }
            }

            foreach (Effect e in RemovedEffects) {
                DeleteEffect(e);
            }
            RanEffects.Clear();
            RemovedEffects.Clear();
        }
    }
}
