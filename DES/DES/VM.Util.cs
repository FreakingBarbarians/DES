using System;
using System.Reflection;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DES
{
    public partial class VM
    {

        public static byte[] ByteParse(string toParse)
        {
            byte[] ret = new byte[0];
            if (toParse.Length > 0)
            {

                string[] dat = toParse.Split('%');

                if (dat[0][0] == 's' || dat[0][0] == 'S')
                {
                    ret = System.Text.Encoding.UTF8.GetBytes(dat[1]);
                }
                else if (dat[0][0] == 'd' || dat[0][0] == 'D')
                {
                    ret = BitConverter.GetBytes(Double.Parse(dat[1]));
                }
                else if (dat[0][0] == 'f' || dat[0][0] == 'F')
                {
                    ret = BitConverter.GetBytes(float.Parse(dat[1]));
                }
                else if (dat[0][0] == 'l' || dat[0][0] == 'L')
                {
                    ret = BitConverter.GetBytes(long.Parse(dat[1]));
                }
                else if (dat[0][0] == 'b' || dat[0][0] == 'B')
                {
                    ret = BitConverter.GetBytes(bool.Parse(dat[1]));
                }
                else if (dat[0][0] == 'i' || dat[0][0] == 'I')
                {
                    ret = BitConverter.GetBytes(int.Parse(dat[1]));
                }
                else
                {
                    throw new Exception("Unrecognizable type: " + toParse);
                }
            }
            return ret;
        }

        public static Tuple<byte[][], int[][]> Compile(string raw) {
            // type%data
            // s%string
            // d%double
            // f%float
            // l%long
            // b%bool
            // i%int
            // $label
            // $references existing label
            // label:
            // creates_label_with_name_on_line:
            // InstructionName
            // calls_instruction_with s%arguments s%here s%seperated s%by s%spaces f%6.9

            // list that will hold the instructions. Converted to array[][] at the end
            List<List<byte>> procedure = new List<List<byte>>();

            // index for arguments;
            int[][] index = new int[0][];

            // splitting raw data into each line.
            string[] rawLines = raw.Split('\n');

            // number of lines = number of instructions
            index = new int[rawLines.Length][];

            // array of code symbols {labels, instructions, args}
            string[][] codeSymbols = new string[rawLines.Length][];

            // some dictionaries for tracking labels and what they point at
            Dictionary<string, int> labelLookup = new Dictionary<string, int>();
            Dictionary<Tuple<int, int>, string> reverseLabelLookup = new Dictionary<Tuple<int, int>, string>();

            // for each line we split the symbols by ' '. then for each char in the symbol
            // determine what it is. instruction, or arg (and what kind), or label
            for (int lineIndex = 0; lineIndex < codeSymbols.Length; lineIndex++)
            {
                codeSymbols[lineIndex] = rawLines[lineIndex].Split(' ');
                
                // don't expect arguments before instructions.
                bool instructionFound = false;

                // position of each argument in the instruction byte array
                List<int> argumentLocations = new List<int>();

                // byte list representation of the instruction line
                List<byte> instruction = new List<byte>();

                for (int symbolIndex = 0; symbolIndex < codeSymbols[lineIndex].Length; symbolIndex++)
                {

                    // the current symbol
                    string symbol = codeSymbols[lineIndex][symbolIndex];

                    // if the last symbol in the code is :
                    // it's a label
                    if (symbol.Length > 0 && symbol[symbol.Length - 1] == ':')
                    {
                        string labelName = symbol.Substring(0, symbol.Length - 1);
                        argumentLocations.Add(instruction.Count);
                        labelLookup.Add(labelName, lineIndex);
                    }
                    // Second char is a % -> we have an argument
                    else if (symbol.Length > 1 && symbol[1] == '%')
                    {
                        // If an instruction hasn't been found then we throw up
                        if (!instructionFound)
                        {
                            throw new Exception("Argument found before instruction " + symbol);
                        }

                        argumentLocations.Add(instruction.Count);
                        instruction.AddRange(VM.ByteParse(codeSymbols[lineIndex][symbolIndex]));

                    }
                    // first symbol is $ -> we have a reference to a label
                    else if (symbol.Length > 0 && symbol[0] == '$')
                    {
                        if (!instructionFound)
                        {
                            throw new Exception("Argument found before instruction " + codeSymbols[lineIndex][symbolIndex]);
                        }

                        reverseLabelLookup.Add(new Tuple<int, int>(lineIndex, instruction.Count), codeSymbols[lineIndex][symbolIndex].Substring(1));
                        // add a place holder byte
                        instruction.Add(0);
                    }
                    // matches none of the above. Must be an instruction
                    else if (symbol.Length > 0)
                    {
                        
                        MethodInfo mf = VM.Current.GetType().GetMethod(symbol);

                        if (mf == null) {
                            throw new Exception("Invalid Instruction: " + symbol);
                        }

                        MyInstruction ins = mf.GetCustomAttribute<MyInstruction>();
                        instruction.Add(ins.Code);
                        instructionFound = true;
                    }
                }

                // add the new instruction!
                procedure.Add(instruction);
                index[lineIndex] = argumentLocations.ToArray();
            }

            // Tuple: item1 - index of instruction line, item2 - index of instruction byte
            // reverseLabelLookup - Tuple (position of placeholder byte) -> string name of label
            // labelLookup - string name of label -> int index of the line of the next instruction
            foreach (Tuple<int, int> a in reverseLabelLookup.Keys)
            {
                // size of the label pos in bytes
                int labelSize;

                // temp list to build new instruction line
                List<byte> tlist = new List<byte>();

                // add all bytes before placeholder
                for (int x = 0; x < a.Item2; x++)
                {
                    tlist.Add(procedure[a.Item1][x]);
                }

                // get the bytes of the index of the line the label points to
                byte[] b = BitConverter.GetBytes(labelLookup[reverseLabelLookup[a]]);
                labelSize = b.Length;

                // add the bytes of the label to the list
                tlist.AddRange(b);

                // add the rest of the bytes after placeholder to the list
                for (int x = a.Item2 + 1; x < procedure[a.Item1].Count; x++)
                {
                    tlist.Add(procedure[a.Item1][x]);
                }

                // built list is done, set as new instruction
                procedure[a.Item1] = tlist;

                // bool controlling when to extend each index
                bool extend = false;

                // updating index
                for (int x = 0; x < index[a.Item1].Length; x++)
                {

                    // all arguments after the placeholder will be shifted
                    // labelSize -1 . since the placeholder took up 1 byte
                    // by itself
                    if (extend == true)
                    {
                        index[a.Item1][x] += labelSize - 1;
                    }

                    // if we have reached the placeholder
                    if (index[a.Item1][x] == a.Item2)
                    {
                        extend = true;
                    }

                }
            }

            byte[][] retProcedure = new byte[procedure.Count][];
            for (int i = 0; i < procedure.Count; i++)
            {
                retProcedure[i] = procedure[i].ToArray();
            }

            return new Tuple<byte[][], int[][]>(retProcedure, index);
        }

    }
}
