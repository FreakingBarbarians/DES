using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;

namespace DES
{

    public enum ExecutionMode : int { STANDARD, END }; // some helpful executionmodes

    public enum InstructionType : int { STANDARD, JUMP};

    public class MyInstruction : Attribute {
        public string Name;

        public byte Code;

        public InstructionType IType;

        public int ArgCount;

        public string[] ArgDescription;

        public MyInstruction(string name, byte code, InstructionType type, int argCount, params string[] argDesc) {
            this.Name = name;
            this.Code = code;
            IType = type;
            ArgCount = argCount;
            ArgDescription = argDesc;
        }
    }

    public partial class VM
    {
        public static VM Current;
        public Stack<byte[]> Stack;
        public Dictionary<string, byte[]> Heap;
        public Dictionary<byte, Instruction> InstructionLookup;

        public BinaryFormatter formatter;

        public Item target;
        public Effect effect;

        public byte[][] Registers; // well they kinda are right? :) Like uniforms in OPENGL? :o

        public byte[] currentInstruction;
        public int[] currentIndex;

        private int IC; // instruction counter;

        public delegate void Instruction();



        public VM() {
            if (Current != null) {
                throw new Exception("Two instances of VM created");
            }

            Current = this;
            Heap = new Dictionary<string, byte[]>();
            Stack = new Stack<byte[]>();
            InstructionLookup = new Dictionary<byte, Instruction>();

            formatter = new BinaryFormatter();

            var meths = this.GetType().GetMethods().Where(meth => Attribute.IsDefined(meth, typeof(MyInstruction)));
            foreach (System.Reflection.MethodInfo method in meths) {
                MyInstruction code = method.GetCustomAttributes(typeof(MyInstruction), true).First() as MyInstruction;
                Console.Out.WriteLine(code.Code + " " + method.Name);
                Instruction delegateInstruction = (Instruction)method.CreateDelegate(typeof(Instruction), this);
                InstructionLookup.Add(code.Code, delegateInstruction);
            }
        }

        public void Execute(Effect effect, Item target) {
            this.effect = effect;
            this.target = target;
            for (IC = 0; IC < effect.Source.Instruction.Length; IC++) {
                currentInstruction = effect.Source.Instruction[IC];
                currentIndex = effect.Source.ArgIndex[IC];
                InstructionLookup[effect.Source.Instruction[IC][0]].Invoke();
            }
        }

        // stole some notation from what I learned about MIPS in class (hey my education is comming in handy :D)
        // I preceeding the instruction means Immediate, the value that the instructions are concerned with are inside the instruction itself
        // S meaning stack,  the value that the instructions are concerned with are in the stack
        // M for memory? maybe idk.


        // pushes the object onto the stack
        // Signature: IA*
        [MyInstruction("Push To Stack" ,0, InstructionType.STANDARD, 1, "Data to push")]
        public void Push() {
            byte[] toPush = new byte[currentInstruction.Length - 1];
            Array.Copy(currentInstruction, 1, toPush, 0, currentInstruction.Length - 1);
            Stack.Push(toPush);
        }

        // Loads bytes from the heap and pushes onto the stack
        // Signature: IA*
        [MyInstruction("Load From Heap To Stack", 1, InstructionType.STANDARD, 1, "Heap Address")]
        public void Load() {
            byte[] byteAddress = new byte[currentInstruction.Length - 1];
            Array.Copy(currentInstruction, 1, byteAddress, 0, currentInstruction.Length - 1);
            string address = System.Text.Encoding.UTF8.GetString(byteAddress);
            address = effect.Id.ToString() + address;
            Stack.Push(Heap[address]);
        }

        // saves bytes onto heap
        // Signature: IA*A* (address, data)
        [MyInstruction("Store from Instruction To Heap", 2, InstructionType.STANDARD, 2, "Heap Address", "Data")]
        public void Save() {
            byte[][] splitArgs = splitArguments(currentInstruction, effect.Source.ArgIndex[IC]);
            byte[] byteAddress = splitArgs[0];
            byte[] data = splitArgs[1];

            string address = System.Text.Encoding.UTF8.GetString(byteAddress);
            address = effect.Id.ToString() + address;

            if (Heap.ContainsKey(address))
            {
                Heap[address] = data;
            }
            else {
                Heap.Add(address, data);
            }

        }

        // prints string literal in the instruction
        // Signature: IA
        [MyInstruction("Print", 3, InstructionType.STANDARD, 1, "string")]
        public void IPrintConsole() {
            byte[][] splitargs = splitArguments(currentInstruction, effect.Source.ArgIndex[IC]);
            string printable = Encoding.UTF8.GetString(splitargs[0]);
            Console.Out.WriteLine(printable);
        }

        // Prints string literal on top of stack
        // Signature: I
        [MyInstruction("Print Stack", 4, InstructionType.STANDARD, 0)]
        public void SPrintConsole() {
            string printable = Encoding.UTF8.GetString(Stack.Pop());
            Console.Out.WriteLine(printable);
        }

        // removes a # of hp from the owners hitbar
        // Signature: #IA
        [MyInstruction("Damage Health I", 5, InstructionType.STANDARD, 1, "Damage")]
        public void IDamageHealth() {
            byte[][] splitargs = splitArguments(currentInstruction, currentIndex);
            int damage = BitConverter.ToInt32(splitargs[0], 0);
            target.HP -= damage;

            if (target.HP < 1) {
                // send death event.
            }
        }

        // removes a # of hp from the owner's hitbar. The amount is the first item on the stack.
        // Signature: #I
        [MyInstruction("Damage Health S", 6, InstructionType.STANDARD,0)]
        public void SDamageHealth() {
            byte[] num = Stack.Pop();
            int damage = BitConverter.ToInt32(num, 0);
            target.HP -= damage;

            if (target.HP < 1) {
                // send death event.
            }
        }

        // rolls and pushes to stack, the result of xdy dice. x dice with y sides
        // Signature: IAA, A is int
        [MyInstruction("Roll dice", 7, InstructionType.STANDARD, 2, "Dice", "ySides")]
        public void IRollDice() {
            byte[][] splitargs = splitArguments(currentInstruction, currentIndex);
            int x = BitConverter.ToInt32(splitargs[0], 0);
            int y = BitConverter.ToInt32(splitargs[1], 0);
            Stack.Push(BitConverter.GetBytes(MyUtils.Utils.RollDice(x, y)));
        }

        // adds 2 immediate values
        // IAA, A is int
        [MyInstruction("Add Immediate", 8, InstructionType.STANDARD, 2, "a", "b")]
        public void IAddI() {
            byte[][] splitargs = splitArguments(currentInstruction, currentIndex);
            int a = BitConverter.ToInt32(splitargs[0], 0);
            int b = BitConverter.ToInt32(splitargs[1], 0);
            Stack.Push(BitConverter.GetBytes(a + b));
        }

        // adds 2 stack values
        // I
        [MyInstruction("Add Stack", 9, InstructionType.STANDARD, 0)]
        public void SAddS() {
            int b = BitConverter.ToInt32(Stack.Pop(), 0);
            int a = BitConverter.ToInt32(Stack.Pop(), 0);
            Stack.Push(BitConverter.GetBytes(a + b));
        }

        // Pushes to the stack the scaling value of an attribute
        // IA*A*, scaling attribute, scale factor
        [MyInstruction("Scale With Attribute", 10, InstructionType.STANDARD, 2, "Scaling Attribute", "Scale Factor")]
        public void IScale() {
            byte[][] splitargs = splitArguments(currentInstruction, currentIndex);
            int a = BitConverter.ToInt32(splitargs[0], 0);
            float b = BitConverter.ToSingle(splitargs[1], 0);
            byte[] val = BitConverter.GetBytes((int)(effect.Owner.Caster.Attributes[a] * b));
            Stack.Push(val);
        }

        // Jumps so that the specified instruction is the next one
        // I.e. pass 15, and it will jump to 14, so next ins is 15
        // IA*
        [MyInstruction("Jump To I", 11, InstructionType.JUMP, 1, "Line")]
        public void IJump() {
            byte[][] splitargs = splitArguments(currentInstruction, currentIndex);
            int pos = BitConverter.ToInt32(splitargs[0],0);
            pos--;

            IC = pos;
        }

        // IJump but the pos comes from the stack
        // I
        [MyInstruction("Jump To S", 12, InstructionType.JUMP, 0)]
        public void SJump() {
            int pos = BitConverter.ToInt32(Stack.Pop(), 0);
            pos--;
            IC = pos;
        }

        // Jumps to X if A  == B. where X and A are in the instruction set, B is on stack
        // makes no sense to put A and B in instruction set since you would know the answer always
        // IA*A*, A, X
        [MyInstruction("Jump if Equal I", 13, InstructionType.JUMP, 2, "To Equate", "Line")]
        public void IJumpEQ() {
            byte[][] splitargs = splitArguments(currentInstruction, currentIndex);
            int pos = BitConverter.ToInt32(splitargs[1], 0);

            // proper & optimized linq comparison? hopefully :)
            if (splitargs[0].SequenceEqual(Stack.Pop())) {
                IC = --pos; // double check to make sure this actually works
            }

        }

        // jumps to X if A == B where X is in the instruction set, A and B are on stack.
        // IA*, X
        [MyInstruction("Jump if Equal S", 14, InstructionType.JUMP, 1, "Line")]
        public void SJumpEQ() {
            byte[][] splitargs = splitArguments(currentInstruction, currentIndex);
            int pos = BitConverter.ToInt32(splitargs[0], 0);

            if (Stack.Pop().SequenceEqual(Stack.Pop())) {
                IC = --pos;
            }
        }

        // pushes the value at register index to stack
        // IA*, regposition
        [MyInstruction("Get Register", 15, InstructionType.STANDARD, 1, "Register Address")]
        public void GetRegister() {
            byte[][] splitargs = splitArguments(currentInstruction, currentIndex);
            int index = BitConverter.ToInt32(splitargs[0], 0);

            Stack.Push(Registers[index]);

        }

        // stores value at register index
        // The value is come from instruction
        // IA*A*, regpos, value
        [MyInstruction("Store Register I", 16, InstructionType.STANDARD, 2, "Register Address", "Value")]
        public void IStoreRegister() {
            byte[][] splitargs = splitArguments(currentInstruction, currentIndex);
            int index = BitConverter.ToInt32(splitargs[0], 0);
            Registers[index] = splitargs[1];
        }

        // stores the value at register index.
        // the value is come from stack
        // IA*, regpos
        [MyInstruction("Store Register S", 17, InstructionType.STANDARD, 1, "Register Address")]
        public void SStoreRegister() {
            byte[][] splitargs = splitArguments(currentInstruction, currentIndex);
            int index = BitConverter.ToInt32(splitargs[0], 0);
            Registers[index] = Stack.Pop();
        }

        // retrievs and pushes the execution mode of the current 
        // effect onto the stack
        // I
        [MyInstruction("Get Execution Mode", 18, InstructionType.STANDARD, 0)]
        public void GetExecutionMode() {
            Stack.Push(BitConverter.GetBytes((int) effect.ExecutionMode));
        }

        // compares an integer provided in the instructions.
        // jumps to the position provided in pos 1 if...
        // TODO: Come up with a better documentation scheme
        // IA*A*, int to be compared, position of next instruction
        [MyInstruction("Jump if I Less Than S", 19, InstructionType.STANDARD, 2, "I", "Line")]
        public void IJumpLT() {
            byte[][] splitargs = splitArguments(currentInstruction, currentIndex);
            int pos = BitConverter.ToInt32(splitargs[1], 0);
            int num = BitConverter.ToInt32(splitargs[0], 0);
            if (num < BitConverter.ToInt32(Stack.Pop(), 0)) {
                IC = --pos;
            }
        }

        [MyInstruction("Jump if S Less Than S", 20, InstructionType.STANDARD, 0)]
        public void SJumpLT() {
            byte[][] splitargs = splitArguments(currentInstruction, currentIndex);
            int pos = BitConverter.ToInt32(splitargs[0], 0);
            if (BitConverter.ToInt32(Stack.Pop(), 0) < BitConverter.ToInt32(Stack.Pop(), 0)) {
                IC = --pos;
            }
        }
        
        // other shit.
        
        // a simpler set register for c# code to call.
        // Intended to push action.localvar variables to the VM
        public void SetRegister(int regnumber, byte[] data) {
            if (regnumber < Registers.Length) {
            Registers[regnumber] = data;
            }
        }

        public byte[][] splitArguments(byte[] args, int[] index) {

            byte[][] splitArgs = new byte[index.Length][];

            for (int i = 0; i < index.Length; i++) {
                byte[] p;
                if (i == index.Length - 1)
                {
                    p = new byte[args.Length - index[i]];
                }
                else {
                    p = new byte[index[i + 1] - index[i]];
                }
                Array.Copy(args, index[i], p, 0, p.Length);
                splitArgs[i] = p;
            }
            return splitArgs;
        }

        public static Type BytetoType(byte code)
        {

            return null;

        }

        public static byte TypeToByte(Type type) {
            return 0;
        }
    }
}
