using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DES
{
    [Serializable]
    public partial class ActionSystem
    {

        public Dictionary<string, Effect> Listeners;
        public void init()
        {
            Listeners = new Dictionary<string, Effect>();
        }
        


    }
}
