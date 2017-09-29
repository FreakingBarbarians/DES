using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DES
{
    [Serializable]
    public struct ExecutionInfo
    {
        public int numberKilled;
        public int damageDone;
        // etc etc
    }

    [Serializable]
    public class Effect
    {
        public EffectTemplate Source;
        public int Id;
        public DES.Action Owner;
        public ExecutionInfo info;
        public ExecutionMode ExecutionMode;

        public Effect(EffectTemplate Source, Action Owner) {
            this.Owner = Owner;
            this.Source = Source;
            Id = -1;
        }
    }
}
