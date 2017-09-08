using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DES
{
    class Utils
    {
        public static ActionVariable CloneActionVariable(ActionVariable a) {
            ActionVariable clone;

            clone.Data = new byte[a.Data.Length];
            clone.Keywords = new string[a.Keywords.Length];

            Array.Copy(a.Data, clone.Data, a.Data.Length);
            Array.Copy(a.Keywords, clone.Keywords, a.Keywords.Length);

            clone.dataType = a.dataType; // won't really change

            return clone;
        }
    }
}
