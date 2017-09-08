using System.Collections;
using System;

namespace DES
{
    public class ItemManager
    {
        public static ItemManager Current;

        MyObjectPool<Item> pool;

        public ItemManager()
        {
            if (Current != null)
            {
                throw new Exception("More than one instance of ItemManager is being made");
            }
            else
            {
                Current = this;
            }
        }

        // more crap later!
    }
}