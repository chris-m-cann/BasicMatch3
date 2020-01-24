using UnityEngine;
using System.Collections.Generic;



namespace Util
{
    public static class ListExtensions
    {
        public static T RandomElement<T>(this List<T> list)
        {
            return list[UnityEngine.Random.Range(0, list.Count)];
        }

    }
}
