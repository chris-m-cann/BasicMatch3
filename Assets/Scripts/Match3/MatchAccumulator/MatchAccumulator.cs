using System.Collections.Generic;
using UnityEngine;


namespace Match3
{
    public abstract class MatchAccumulator : ScriptableObject
    {
        public abstract List<Match> FindMatches(RuntimeGridData grid, SwapEventData swapData, List<Match> previousMatches);

        public List<Match> FindMatches(RuntimeGridData grid, SwapEventData swapData)
        {
            return FindMatches(grid, swapData, new List<Match>());
        }
    }
}