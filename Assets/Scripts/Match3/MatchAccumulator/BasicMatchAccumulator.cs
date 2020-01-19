using System.Collections.Generic;
using UnityEngine;


namespace Match3
{
    [CreateAssetMenu(menuName = "Systems/MatchAccumulator/Basic")]
    public class BasicMatchAccumulator : MatchAccumulator
    {
        public override List<Match> FindMatches(RuntimeGridData grid, SwapEventData swapData, List<Match> previousMatches)
        {
            var potentialMatch = new List<MatchElement>();
            var lastTileTag = "";

            // check for vertical matches
            for (int x = 0; x < grid.Width; x++)
            {
                for (int y = 0; y < grid.Height; y++)
                {
                    lastTileTag = ProcessTile(previousMatches, potentialMatch, grid, lastTileTag, x, y);
                }

                if (potentialMatch.Count > 2)
                {
                    previousMatches.Add(new Match(potentialMatch.ToArray()));
                }
                potentialMatch.Clear();
                lastTileTag = "";
            }


            // check for horizontal matches
            for (int y = 0; y < grid.Height; y++)
            {
                for (int x = 0; x < grid.Width; x++)
                {
                    lastTileTag = ProcessTile(previousMatches, potentialMatch, grid, lastTileTag, x, y);
                }

                if (potentialMatch.Count > 2)
                {
                    previousMatches.Add(new Match(potentialMatch.ToArray()));
                }
                potentialMatch.Clear();
                lastTileTag = "";
            }

            return previousMatches;

           

        }

        private string ProcessTile(List<Match> matches, List<MatchElement> potentialMatch, RuntimeGridData grid, string lastTileTag, int x, int y)
        {
            var tile = grid.GetTileAt(x, y);
            if (tile == null) return "";



            // if first element
            if (potentialMatch.Count == 0)
            {
                potentialMatch.Add(MatchElementAt(grid, x, y));
            }
            // if same as the previously seen tiles
            else if (tile.CompareTag(lastTileTag))
            {
                potentialMatch.Add(MatchElementAt(grid, x, y));
            }
            // if this tag does not match that of the previous tile but we have built up enough for a match then add that new match
            else if (potentialMatch.Count > 2)
            {
                matches.Add(new Match(potentialMatch.ToArray()));
                potentialMatch.Clear();

                potentialMatch.Add(MatchElementAt(grid, x, y));
            }
            // not the same tag as before so ditch list we were building and start again
            else
            {
                potentialMatch.Clear();

                potentialMatch.Add(MatchElementAt(grid, x, y));
            }

            return tile.tag;
        }


        private MatchElement MatchElementAt(RuntimeGridData grid, int i, int j)
        {
            return new MatchElement
            {
                i = i,
                j = j,
                tile = grid.GetTileAt(i, j).gameObject
            };
        }

    }
}