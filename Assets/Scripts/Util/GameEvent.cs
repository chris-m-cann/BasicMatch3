using System.Collections.Generic;
using UnityEngine;

namespace Util
{
    public class GameEvent<T> : ScriptableObject
    {
        private readonly List<GameEventListener<T>> listeners = new List<GameEventListener<T>>();

        public void Raise(T t)
        {
            //Debug.Log($"RaisingEvent on {listeners.Count} listeners");
            // goes through the list backwards incase a listener removes itself
            for (int i = listeners.Count - 1; i >= 0; i--)
            {
                listeners[i].OnEventRaised(t);
            }
        }

        public void RegisterListener(GameEventListener<T> listener)
        {
            if (!listeners.Contains(listener))
                listeners.Add(listener);
        }

        public void RemoveListener(GameEventListener<T> listener)
        {
            if (!listeners.Contains(listener))
                listeners.Remove(listener);
        }
    }

}