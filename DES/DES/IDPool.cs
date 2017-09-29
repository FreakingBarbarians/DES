using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class IDPool : MyObjectPool<int>
    {
        public IDPool(int size) {
            pool = new int[size];
        indexlookup = new Dictionary<int, int>();
            for (int i = 0; i < size; i ++) {
                pool[i] = i;
                indexlookup.Add(i, i);
            }
            front = 0;
            back = size - 1;
            freecount = size;
        }
}
