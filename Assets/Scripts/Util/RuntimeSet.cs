using System.Collections.Generic;
using UnityEngine;


namespace Util
{
    public abstract class RuntimeSet<T> : ScriptableObject
    {
        [System.NonSerialized]
        private readonly List<T> items = new List<T>();

        public int Size { get => items.Count; }



        public void Add(T t)
        {
            if (!items.Contains(t)) items.Add(t);
        }


        public void Remove(T t)
        {
            if (items.Contains(t)) items.Remove(t);
        }

        public T this[int idx]
        {
            get => items[idx];
        }
    }
}