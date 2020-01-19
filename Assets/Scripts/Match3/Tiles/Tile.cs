using System.Collections.Generic;
using UnityEngine;


namespace Match3
{
    public abstract class Tile : MonoBehaviour
    { 
        public virtual void OnCreate(SwapEventData swapData, Tile cause)
        {

        }

        public virtual List<Match> OnSwap(Tile other)
        {
            return new List<Match>();
        }

        public virtual List<Match> OnDestroyed()
        {
            return new List<Match>();
        }
    }
}