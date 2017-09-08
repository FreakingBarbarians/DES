using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DES
{
    public struct ExecutionInfo
    {
        public int numberKilled;
        public int damageDone;
        // etc etc
    }

    public class Effect
    {
        public EffectTemplate Source;
        public int Id;
        public DES.Action Owner;
        public ExecutionInfo info;

        public Effect(EffectTemplate Source, Action Owner) {
            this.Owner = Owner;
            this.Source = Source;
            Id = -1;
        }
    }
}
