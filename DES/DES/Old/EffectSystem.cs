using System;
using System.Collections.Generic;

using DES;

namespace DES {

[Serializable]
public class EffectSystem {
    public Item Owner;
    public List<Effect> TickingEffects;
    public Dictionary<string, List<Effect>> TriggerEffects;     // (TRIGGERING_KEYWORD, Effect)
    public List<Effect> TriggeredEffects;                       // effects that have been used already
    public Dictionary<Action, List<Effect>> ActionEffectMap;    // maps action to effects
    public Dictionary<Action, List<Effect>> EndEffect;          // action specific ending effects
    public List<Effect> ToRemove;

    public EffectSystem(Item Owner) {
        this.Owner = Owner;
        TickingEffects = new List<Effect>();
        TriggerEffects = new Dictionary<string, List<Effect>>();
        TriggeredEffects = new List<Effect>();
        ActionEffectMap = new Dictionary<Action, List<Effect>>();
        ToRemove = new List<Effect>();
        EndEffect = new Dictionary<Action, List<Effect>>();
    }

    public void AddTickingEffect(Effect ToAdd) {
        TickingEffects.Add(ToAdd);
        if (ActionEffectMap.ContainsKey(ToAdd.Owner))
        {
            ActionEffectMap[ToAdd.Owner].Add(ToAdd);
        }
        else {
            ActionEffectMap.Add(ToAdd.Owner, new List<Effect>());
        }
    }

    public void AddTriggeredEffect(Effect ToAdd) {
        foreach (string key in ToAdd.Keywords) {
            if (key.Contains("TRIGGER_")) {
                string conc = key.Substring(7);
                if (TriggerEffects.ContainsKey(conc))
                {
                    TriggerEffects[conc].Add(ToAdd);
                }
                else {
                    TriggerEffects.Add(conc, new List<Effect>());
                    TriggerEffects[conc].Add(ToAdd);
                }
            }
        }
    }

    public void AddEndEffect(Effect ToAdd) {
        if (EndEffect.ContainsKey(ToAdd.Owner))
        {
            EndEffect[ToAdd.Owner].Add(ToAdd);
        }
        else {
            EndEffect.Add(ToAdd.Owner, new List<Effect>());
        }
    }

    public void ProcessEffect(Effect ToProcess) {
        // determine if the effect triggers any triggering effects
        // triggers will cascade other triggers
        foreach (string KEYWORD in ToProcess.Keywords) {
            if (TriggerEffects.ContainsKey(KEYWORD)) {
                foreach (Effect Trigger in TriggerEffects[KEYWORD]) {
                    if (!TriggeredEffects.Contains(Trigger)) {
                        TriggeredEffects.Add(Trigger);
                        Trigger.Vars["TRIGGER"] = ToProcess;
                        ProcessEffect(Trigger);
                    }
                    if (Trigger.Keywords.Contains("MULTI")) {
                        ProcessEffect(Trigger);
                    }
                }
            }
        }

        // grab args and run instructions for each instruction
        VM.NextInstruction = 0;

        while (VM.NextInstruction < ToProcess.Procedure.Length) {
            string[] temp = ToProcess.Procedure[VM.NextInstruction].Split(' ');
            string command = temp[0];
            Object[] args = new Object[temp.Length - 1];
            for (int i = 1; i < temp.Length; i ++) {
                string arg = "";
                if (temp[i].Length > 6)
                {
                    arg = temp[i].Substring(6);
                }
                else {
                    arg = "";
                }
                string header = temp[i].Substring(0, 6);
                Console.WriteLine(header);
                switch (header) {
                    case "STACK$":
                        args[i - 1] = VM.MainStack.Pop();
                        break;
                    case "HOST$$":
                        args[i - 1] = this.Owner;
                        break;
                    case "SYSTEM":
                        args[i - 1] = this;
                        break;
                    case "THIS$$":
                        args[i - 1] = ToProcess;
                        break;
                    case "CASTER":
                        args[i - 1] = ToProcess.Owner.Caster;
                        break;
                    case "INT$$$":
                        args[i - 1] = int.Parse(arg);
                        break;
                    case "STRING":
                        args[i - 1] = arg;
                        break;
                    case "VAR$$$":
                        args[i - 1] = ToProcess.Vars[arg];
                        break;
                    default:
                        throw new Exception("Uncrecognized argument type: " + header);
                }
            }
            Console.WriteLine("Instruction:" + VM.NextInstruction + " " + command);
            VM.Run(command, args);
        }
    }

    public void Update() { // simulates 1 turn
        TriggeredEffects.Clear();
        foreach (Effect e in ToRemove) {
            DeleteEffect(e);
        }
        ToRemove.Clear();
        foreach (Effect effect in TickingEffects) {
            ProcessEffect(effect);
        }
    }

    private void DeleteEffect(Effect ToRemove) {
        // case 1: The effect exists in the action map -> it is a ticking action
        if (ActionEffectMap.ContainsKey(ToRemove.Owner)){
            // remove the action from the AFM list, if that is now empty remove the entry
            ActionEffectMap[ToRemove.Owner].Remove(ToRemove);
            if (ActionEffectMap[ToRemove.Owner].Count == 0)
            {
                ActionEffectMap.Remove(ToRemove.Owner);
            }
            // remove from the ticking effects list
            TickingEffects.Remove(ToRemove);
        }
        // case 2: otherwise it exists in the end effects list. do same
        else if (EndEffect.ContainsKey(ToRemove.Owner)) {
            EndEffect[ToRemove.Owner].Remove(ToRemove);
            if (EndEffect[ToRemove.Owner].Count == 0) {
                EndEffect.Remove(ToRemove.Owner);
            }
        }
    }

    private void DeleteEffect(string KeyWordString) {
        // KeyWordString of the form "A^B||-C||D" ^ and -not ||or -|xor idk...
    }

    public void RemoveEffect(Effect ToRemove) {
        this.ToRemove.Add(ToRemove);
    }
}
}