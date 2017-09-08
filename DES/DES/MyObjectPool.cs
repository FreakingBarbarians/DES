using System.Collections;
using System.Collections.Generic;
using System;
using MyUtils;
/*  Generic pool
    User is responsible for initialization/setting up of
    Objects requested from the pool
*/
public class MyObjectPool<T> {

    public T[] pool;                        // the pool
    public Dictionary<T, int> indexlookup;  // lookup for index of objects
    protected int size;                       // num objects in pool
    protected int front;                      // front of free objects
    protected int back;                       // back of free objects
    protected int freecount;                  // # of free objects in pool

    public MyObjectPool(int size, params System.Object[] args){
        pool = new T[size];
        indexlookup = new Dictionary<T, int>();
        freecount = size;
        System.Reflection.ConstructorInfo constructorInfo = typeof(T).GetConstructor(Utils.GetTypes(args));

        if (constructorInfo == null) {
            throw new Exception("Could not find constructor with argument types: " + Utils.ListToString(Utils.GetTypes(args)));
        }

        for (int i = 0; i < size; i++) {
            pool[i] = (T)constructorInfo.Invoke(args);
            indexlookup.Add(pool[i], i);
        }
        front = 0;
        back = size - 1;
    }

    protected MyObjectPool() {

    }


    public T Get() {
        if (freecount < 1) {
            return default(T);
        }

        T returnable = pool[front];

        front = (front + 1) % pool.Length;

        freecount--;
        return returnable;
    }

    public void Free(T Object) {
        if (freecount == pool.Length) {
            throw new System.Exception("Pool Full");
        }
        // move back one space forward
        back = (back + 1) % pool.Length;
        // create a pointer to the in-use object at back
        T p = pool[back];
        // replace in-use object at back with in-comming free object
        pool[back] = Object;
        // replace the old in-use position in-comming object had with p
        pool[indexlookup[Object]] = p;
        
        // update index lookup to swap the positions of the two
        int t = indexlookup[Object];
        indexlookup[Object] = back;
        indexlookup[p] = t;
    }

}
