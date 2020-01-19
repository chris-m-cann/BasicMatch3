using System.Collections.Generic;
using UnityEngine;
using System;


namespace Match3
{
    [CreateAssetMenu(menuName = "Systems/MatchAccumulator/Composite")]
    public class CompositeMatchAccumulator : MatchAccumulator
    {
        [SerializeField] private MatchAccumulator[] accumulators;

        public override List<Match> FindMatches(RuntimeGridData grid, SwapEventData swapData, List<Match> previousMatches)
        {
            foreach (var acc in accumulators)
            {
                previousMatches = acc.FindMatches(grid, swapData, previousMatches);
            }
            return previousMatches;
        }
    }
}