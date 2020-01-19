using System.Collections.Generic;
using UnityEngine;

namespace Match3
{
    [CreateAssetMenu(menuName = "Systems/MatchAccumulator/AddCauseMetadata")]
    public class AddCauseMetadata : MatchAccumulator
    {
        public override List<Match> FindMatches(RuntimeGridData grid, SwapEventData swapData, List<Match> previousMatches)
        {
            for (int i = 0; i < previousMatches.Count; i++)
            {
                var match = previousMatches[i];
                match.causeElem = System.Array.FindIndex(match.elements, item =>
                {
                    var pos = new Vector2Int(item.i, item.j);
                    return pos == swapData.swappedPos1 || pos == swapData.swappedPos2;
                });

                previousMatches[i] = match;
            }
            return previousMatches;
        }
    }
}