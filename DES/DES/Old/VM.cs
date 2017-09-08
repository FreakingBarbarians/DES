using System;
using System.Collections.Generic;

namespace DES
{
    public class VM // merge with efect system
    {

        public static Dictionary<string, Delegate> Commands = new Dictionary<string, Delegate>();
        public static Stack<Object> MainStack = new Stack<Object>(); // redesign?
        public static int NextInstruction;
        public static void init()
        {
            Commands.Clear();
            MainStack.Clear();
            Commands.Add("RestoreHealth", new Action<Item, int>(RestoreHealth));
            Commands.Add("AddHealth", new Action<Item, int>(AddHealth));
            Commands.Add("SetHealth", new Action<Item, int>(SetHealth));
            Commands.Add("SetMaxHealth", new Action<Item, int>(SetMaxHealth));
            Commands.Add("RemoveEffect", new Action<EffectSystem, Effect>(RemoveEffect));
            Commands.Add("JEQ", new Action<object, object, int>(JEQ));
            Commands.Add("JLT", new Action<int, int, int>(JLT));
            Commands.Add("JUMP", new Action<int>(JUMP));
            Commands.Add("DecrementVar", new Action<Effect, string>(DecrementVar));
            Commands.Add("IncrementVar", new Action<Effect, string>(IncrementVar));
            NextInstruction = 0;
        }

        public static void Run(string commandName, params Object[] args)
        {
            if (Commands.ContainsKey(commandName))
            {
                object retVal = Commands[commandName].DynamicInvoke(args); // slow :(
                                                                           // can get around this by using case statements. but LOTS of typing :(
                if (retVal != null)
                {
                    MainStack.Push((string)retVal);
                }
            }
        }

        public static void RestoreHealth(Item target, int amount)
        {
            target.HP = Math.Min(target.HP + amount, target.MHP);
            NextInstruction++;
        }

        public static void AddHealth(Item target, int amount)
        {
            target.MHP += amount;
            target.HP = Math.Min(target.MHP, target.HP);
            NextInstruction++;
        }

        public static void SetHealth(Item target, int amount)
        {
            target.HP = Math.Min(amount, target.MHP);
            NextInstruction++;
        }

        public static void SetMaxHealth(Item target, int amount)
        {
            target.MHP = amount;
            NextInstruction++;
        }

        public static void RemoveEffect(EffectSystem system, Effect effect)
        {
            system.RemoveEffect(effect);
            NextInstruction++;
        }

        public static void JEQ(object a, object b, int location)
        {
            if (a.Equals(b))
            {
                NextInstruction = location;
            }
            else
            {
                NextInstruction++;
            }
        }

        public static void JLT(int a, int b, int location)
        {
            if (a < b)
            {
                NextInstruction = location;
            }
            else
            {
                NextInstruction++;
            }
        }

        public static void JLT(float a, float b, int location)
        {
            if (a < b)
            {
                NextInstruction = location;
            }
            else
            {
                NextInstruction++;
            }
        }

        public static void JUMP(int location)
        {
            NextInstruction = location;
        }

        public static void DecrementVar(Effect effect, string varname)
        {
            effect.Vars[varname] = (int)effect.Vars[varname] - 1;
            NextInstruction++;
        }

        public static void IncrementVar(Effect effect, string varname)
        {
            effect.Vars[varname] = (int)effect.Vars[varname] + 1;
            NextInstruction++;
        }

        // Think about using dynamic programming to generate a fast *profile* that is created
        // First time an action is run, and then re-used whenever an action is used again.
    }
}