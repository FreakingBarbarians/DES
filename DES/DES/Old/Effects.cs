using System;
using System.Collections.Generic;

namespace DES
{
    [Serializable]
    public class Effect
    {
        // effects can store variables!
        public List<string> Keywords;
        public Dictionary<string, Object> Vars;
        public string[] Procedure; // instruction arg1 arg2 arg3 arg4 arg5 arg6 arg7 \n instruction2 arg1 arg2 arg3 ...
        public DES.Action Owner;
        public string Name;

        public Effect(string source)
        {
            // parsing
        }

        public Effect()
        {
            Keywords = new List<string>();
            Vars = new Dictionary<string, object>();
            Procedure = new string[1];
        }
    }
}