using System;
using System.Collections.Generic;

// 2 types. 
// Object action affects objects.
// Action action affects actions.

// Actions are decoded processed + applied by action system
namespace DES
{
    [Serializable]
    public class ActionRequirement
    {
        public string Requirement;
        public int amount;
        public bool consume;

        public ActionRequirement() {
            Requirement = "";
            amount = 0;
            consume = false;
        }
    }

    [Serializable]
    public class ActionTemplate
    {
        public string Name;
        public string Description;
        public int TargetType;
        public int Range;
        public int Range2; // used for AOE and burst stuff.
                           // Subaction?
        public List<Effect> InitialEffects;
        public List<Effect> TickingEffects;
        public List<Effect> TriggeredEffects;
        public List<Effect> EndEffects;
        public List<ActionRequirement> Requirements;

        public ActionTemplate()
        {
            // serializable
            InitialEffects = new List<Effect>();
            TickingEffects = new List<Effect>();
            TriggeredEffects = new List<Effect>();
            EndEffects = new List<Effect>();
            Requirements = new List<ActionRequirement>();
        }

        public Action DispatchAction(Item caster, List<Item> targets)
        {
            return new Action(this, caster, targets).Run();
        }
    }

    [Serializable]
    public class Action
    {
        public ActionTemplate Source;
        public List<Item> Targets;
        public Item Caster;

        public Action(ActionTemplate Source, Item Caster, List<Item> Targets)
        {
            this.Source = Source; this.Targets = Targets; this.Caster = Caster;
        }

        public Action Run()
        {

            foreach (Item target in Targets)
            {
                foreach (Effect eff in Source.InitialEffects)
                {
                    target.Effects.ProcessEffect(eff);
                }
                foreach (Effect eff in Source.TriggeredEffects)
                {
                    target.Effects.AddTriggeredEffect(eff);
                }
                foreach (Effect eff in Source.TickingEffects)
                {
                    target.Effects.AddTickingEffect(eff);
                }
                foreach (Effect eff in Source.EndEffects)
                {
                    target.Effects.AddEndEffect(eff);
                }
            }
            return this;
        }
    }
}
// first design the language.

// redisign this from scratch