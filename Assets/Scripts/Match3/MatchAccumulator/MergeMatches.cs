using System.Collections.Generic;
using System.Linq;
using UnityEngine;


namespace Match3
{
    [CreateAssetMenu(menuName = "Systems/MatchAccumulator/Merging")]
    public class MergeMatches : MatchAccumulator
    {
        public override List<Match> FindMatches(RuntimeGridData grid, SwapEventData swapData, List<Match> previousMatches)
        {
            List<Match> merged = new List<Match>();

            bool mergeFound = false;
            for (int i = 0; i < previousMatches.Count; i++)
            {
                Match m = previousMatches[i];
                for (int j = i + 1; j < previousMatches.Count; j++)
                {
                    if (m.OverlapsWith(previousMatches[j]))
                    {
                        // create the merged Match

                        var l = new List<MatchElement>(m.elements.Length + previousMatches[j].elements.Length);
                        l.AddRange(m.elements);
                        l.AddRange(previousMatches[j].elements);
                        var mergedElements = l.Distinct(new MatchComparator()).ToArray();

                        // add it to the merged list
                        merged.Add(new Match(mergedElements));
                        // make sure we dont add this match to the merged list twice
                        previousMatches.RemoveAt(j);
                        // make sure we dont add the 3 at the end of this
                        mergeFound = true;
                    }
                }

                if (!mergeFound)
                {
                    merged.Add(m);
                }
            }


            return merged;
        }
    }

}