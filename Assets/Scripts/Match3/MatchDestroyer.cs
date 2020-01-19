using System.Collections.Generic;
using UnityEngine;

namespace Match3
{
    public abstract class MatchDestroyer : ScriptableObject
    {
        public abstract void DestroyMatches(List<Match> matches, SwapEventData swapData);
    }
}