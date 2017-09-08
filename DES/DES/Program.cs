using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Web;
using System.Diagnostics;

namespace DES
{
    class Program
    {
        static void Main(string[] args)
        {
            VM VM = new VM();

            Test1();
            Test2();
            Test3();
            Test4();
        }

        public static void Test0() {
            Character dummy = new Character("dumItem", 0, 0, 0);

            ActionTemplate PrintToConsole = new ActionTemplate();
            EffectTemplate printEffect = new EffectTemplate();
            printEffect.Name = "PrintEffect";
            printEffect.Instruction = new byte[1][];
            List<byte> instruction = new List<byte>();
            instruction.Add(3);
            instruction.Add(VM.TypeToByte(typeof(string))); // 
            foreach (byte b in Encoding.UTF8.GetBytes("ThommySmells"))
            {
                instruction.Add(b);
            }
            printEffect.Instruction[0] = instruction.ToArray();

            printEffect.ArgIndex = new int[1][];
            printEffect.ArgIndex[0] = new int[] { 2 };

            PrintToConsole.Effects = new EffectTemplate[] { printEffect };
            PrintToConsole.DispatchAction(new Item[] { dummy }, dummy);
            Stopwatch time = new Stopwatch();
            time.Start();
            dummy.ActionSys.ProcessEffects();
            time.Stop();
            Console.Out.WriteLine(time.ElapsedMilliseconds);
            Console.ReadKey();
        }

        public static void Test1() {
            BinaryFormatter formatter = new BinaryFormatter();
            // goal. Create a spell that does damage.

            Character c = new Character("test", 1, 1, 1);
            c.MHP = 32;
            c.HP = 32;

            ActionTemplate fireball = new ActionTemplate();
            EffectTemplate firedamage = new EffectTemplate();
            firedamage.Name = "FireDamage";
            List<byte> IS1 = new List<byte>();
            IS1.Add(5);
            IS1.Add(VM.TypeToByte(typeof(int)));
            byte[] num = BitConverter.GetBytes(32);

            for (int b = 0; b < num.Length; b++) {
                IS1.Add(num[b]);
            }

            firedamage.Instruction[0] = IS1.ToArray();
            firedamage.ArgIndex[0] = new int[] { 2 };

            fireball.Effects = new EffectTemplate[] { firedamage };
            
            fireball.DispatchAction(new Item[] { c }, c);

            c.ActionSys.ProcessEffects();

            Console.Out.WriteLine(c.HP);
            Console.ReadKey();
        }

        public static void Test2() {
            BinaryFormatter formatter = new BinaryFormatter();
            // goal. Create a spell that does damage. Using the stack

            Character c = new Character("test", 1, 1, 1);
            c.MHP = 32;
            c.HP = 32;

            ActionTemplate fireball = new ActionTemplate();
            EffectTemplate firedamage = new EffectTemplate();
            firedamage.Name = "FireDamage";

            List<byte> IS1 = new List<byte>();
            List<byte> IS2 = new List<byte>();

            IS1.Add(0);
            IS1.Add(VM.TypeToByte(typeof(int)));

            byte[] num =  BitConverter.GetBytes(32);
            for (int b = 0; b < num.Length; b++)
            {
                IS1.Add(num[b]);
            }

            IS2.Add(6);

            firedamage.ArgIndex = new int[2][];
            firedamage.Instruction = new byte[2][];

            firedamage.ArgIndex[0] = new int[] { 2 };
            firedamage.ArgIndex[1] = new int[0];
            firedamage.Instruction[0] = IS1.ToArray();
            firedamage.Instruction[1] = IS2.ToArray();

            fireball.Effects = new EffectTemplate[] { firedamage };
            fireball.DispatchAction(new Item[] { c }, c);

            c.ActionSys.ProcessEffects();

            Console.Out.WriteLine(c.HP);
            Console.ReadKey();
        }

        public static void Test3() {
            BinaryFormatter formatter = new BinaryFormatter();
            // goal. Create a spell that does damage. Using the stack

            Character c = new Character("test", 1, 1, 1);
            c.MHP = 32;
            c.HP = 32;

            ActionTemplate fireball = new ActionTemplate();
            EffectTemplate firedamage = new EffectTemplate();
            firedamage.Name = "FireDamage";

            List<byte> IS1 = new List<byte>();
            List<byte> IS2 = new List<byte>();

            int[] index = new int[2];

            IS1.Add(7);

            index[0] = 1;

            byte[] x = BitConverter.GetBytes(64);
            for (int b = 0; b < x.Length; b++)
            {
                IS1.Add(x[b]);
            }

            byte[] y = BitConverter.GetBytes(1);
            for (int b = 0; b < y.Length; b++)
            {
                IS1.Add(y[b]);
            }
            index[1] = index[0] + x.Length;

            IS2.Add(6);

            firedamage.ArgIndex = new int[2][];
            firedamage.Instruction = new byte[2][];

            firedamage.ArgIndex[0] = index;
            firedamage.ArgIndex[1] = new int[0];

            firedamage.Instruction[0] = IS1.ToArray();
            firedamage.Instruction[1] = IS2.ToArray();

            fireball.Effects = new EffectTemplate[] { firedamage };
            fireball.DispatchAction(new Item[] { c }, c);

            c.ActionSys.ProcessEffects();

            Console.Out.WriteLine(c.HP);
            Console.ReadKey();
        }

        public static void Test4() {
            Character c = new Character("me", 100,100,1);
            c.Attributes = new int[] { 16, 12, 14, 11, 21, 20 };

            c.MHP = 32;
            c.HP = 32;

            ActionTemplate EldritchBlast = new ActionTemplate();

            EffectTemplate EldritchBlastDamage = new EffectTemplate();

            EldritchBlast.Effects = new EffectTemplate[] { EldritchBlastDamage };

            EldritchBlastDamage.ArgIndex = new int[4][];
            EldritchBlastDamage.Instruction = new byte[4][];

            List<byte> RollDamage = new List<byte>();
            List<byte> ApplyDamage = new List<byte>();
            List<byte> GetModifier = new List<byte>();
            List<byte> ApplyDamage2 = new List<byte>();

            RollDamage.Add(7);
            EldritchBlastDamage.ArgIndex[0] = new int[2];

            // 4d6 in buffer;
            byte[] dice = BitConverter.GetBytes(4);
            byte[] sides = BitConverter.GetBytes(6);
            foreach (byte b in dice) {
                RollDamage.Add(b);
            }

            foreach (byte b in sides) {
                RollDamage.Add(b);
            }

            // position of 4 and 6 in buffer
            EldritchBlastDamage.ArgIndex[0][0] = 1;
            EldritchBlastDamage.ArgIndex[0][1] = EldritchBlastDamage.ArgIndex[0][0] + dice.Length;
            // 4d6 to instruction set
            EldritchBlastDamage.Instruction[0] = RollDamage.ToArray();

            // apply damage

            EldritchBlastDamage.Instruction[1] = new byte[] { 6 };

            // get cha mod

            EldritchBlastDamage.ArgIndex[2] = new int[2];

            GetModifier.Add(10);

            byte[] attribute = BitConverter.GetBytes(5);
            byte[] scale = BitConverter.GetBytes(0.5f);

            foreach (byte b in attribute) {
                GetModifier.Add(b);
            }

            foreach (byte b in scale) {
                GetModifier.Add(b);
            }

            EldritchBlastDamage.ArgIndex[2][0] = 1;
            EldritchBlastDamage.ArgIndex[2][1] = EldritchBlastDamage.ArgIndex[2][0] + attribute.Length;

            EldritchBlastDamage.Instruction[2] = GetModifier.ToArray();

            EldritchBlastDamage.Instruction[3] = new byte[] { 6 };

            EldritchBlast.Effects = new EffectTemplate[] { EldritchBlastDamage };

            EldritchBlast.DispatchAction(new Item[] { c }, c);

                c.ActionSys.ProcessEffects();
                Console.Out.WriteLine(32 - c.HP);
                Console.ReadKey();
        }
    }
}
