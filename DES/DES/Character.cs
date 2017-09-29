using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DES
{
    [Serializable]
    public abstract class AttributeInfo {
        public static int STR = 0;
        public static int DEX = 1;
        public static int CON = 2;
        public static int INT = 3;
        public static int WIS = 4;
        public static int CHA = 5;
    }

    [Serializable]
    public class Character : Item
    {

        // we go with classic Roleplaying stats

        public int[] Attributes;
        // public List<ClassLevel>();
        // race etc etc

        public Character(string name, float volume, float weight, int sortinglayer) : base(name,weight,volume,sortinglayer) {
            // more stuff

            Attributes = new int[6];
        }


    }
}
