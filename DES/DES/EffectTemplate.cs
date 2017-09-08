using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DES
{
    public class EffectTemplate
    {
        public string Name;
        public byte[][] Instruction; // [instruction][position]
        public int[][] ArgIndex;
        public string[] Keywords;

        // ON Removal instructions as well? AGHHH :(
        // can just put inside Main instructions but have a special
        // Jump Location?
        // Or have the editors put it in directly

        public EffectTemplate() {
            Instruction = new byte[1][];
            Instruction[0] = new byte[1];
            ArgIndex = new int[1][];
            ArgIndex[0] = new int[1];
            Keywords = new string[1];
            Name = "UNDEFINED";
        }
    }
}
