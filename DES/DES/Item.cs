using System;
using System.Collections.Generic;
using DES;

namespace DES
{
    [Serializable]
    public class Item
    {
        public float Volume;
        public float Weight;
        public int SortingLayer;
        public List<string> KEYWORDS;
        public int MHP;
        public int HP;
        public string Name;
        public ActionEffectSystem ActionSys;

        public Item(string name, float weight, float volume, int SortingLayer)
        {
            this.Name = name;
            this.Weight = weight;
            this.Volume = volume;
            this.SortingLayer = SortingLayer;
            ActionSys = new ActionEffectSystem(this);
        }

        public override string ToString()
        {
            return String.Format("{0} W:{1} V:{2} S:{3}", Name, Weight, Volume, SortingLayer);
        }

        // redo this.

    }
}