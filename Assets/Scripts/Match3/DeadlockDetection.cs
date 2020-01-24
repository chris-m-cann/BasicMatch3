using UnityEngine;
using UnityEngine.Events;
using System.Collections.Generic;

namespace Match3
{

    [CreateAssetMenu(menuName = "Systems/DeadlockDetection")]
    public class DeadlockDetection : ScriptableObject
    {
        [SerializeField] private RuntimeGridData grid;
        [SerializeField] private UnityEvent DeadlockFound;
        [SerializeField] private MatchAccumulator matchFinder;

        public System.Collections.IEnumerator DetectDeadlock(MonoBehaviour context, UnityAction onDeadlock = null)
        {

            yield return context.StartCoroutine(FindPotentialMatches(matches =>
            {
                if (matches.Count != 0) return;
                // if we get all the way here then no potental matches have been found therefore deadlocked
                Debug.LogError("Deadlock found!!");

                if (onDeadlock != null)
                {
                    onDeadlock();
                }
                else
                {
                    Notify();
                }
            }));

        }

        public System.Collections.IEnumerator FindPotentialMatches(UnityAction<List<Match>> onMatches, bool firstOnly = true)
        {
            var matches = new List<Match>();

            List<Match> potentialMatches = new List<Match>();

            for (int x = 0; x < grid.Width; x++)
            {
                for (int y = 0; y < grid.Height; y++)
                {
                    potentialMatches.Clear();
                    // stop us looking down off the grid
                    if (y > 0)
                    {
                        potentialMatches.AddRange(MatchesCaused(x, y, Vector2Int.down));
                    }

                    // stop us looking left off the grid
                    if (x > 0)
                    {
                        potentialMatches.AddRange(MatchesCaused(x, y, Vector2Int.left));
                    }

                    if (potentialMatches.Count > 0)
                    {
                        // match found therefore no deadlock
                        yield return null;
                        matches.AddRange(potentialMatches);

                        if (firstOnly)
                        {
                            onMatches(matches);
                            yield break; // return from function
                        }
                    }
                    yield return null;
                }
            }
            onMatches(matches);
        }

        private void Notify()
        {
            DeadlockFound.Invoke();
        }

        private List<Match> MatchesCaused(int x, int y, Vector2Int dir)
        {
            var x2 = x + dir.x;
            var y2 = y + dir.y;
            var tile1 = grid.GetTileAt(x, y);
            var tile2 = grid.GetTileAt(x2, y2);

            // if one of them is a blocker we cant swap
            if (!grid.cells[x, y].IsPassable || !grid.cells[x2, y2].IsPassable)
            {
                return new List<Match>();
            }

            // if anything creates matches on swap then we definietely have a match (tags are probably the wrong way to do this)
            if (tile1.CompareTag("MatchesOnSwap"))
            {
                var elements = new MatchElement[1];
                elements[0] = new MatchElement
                {
                    i = x,
                    j = y,
                    tile = tile1.gameObject
                };
                var match = new Match(elements, false, 0);
                match.causeElem = 0;
                var matches = new List<Match>();
                matches.Add(match);
                return matches;
            }

            if (tile2.CompareTag("MatchesOnSwap"))
            {
                var elements = new MatchElement[1];
                elements[0] = new MatchElement
                {
                    i = x2,
                    j = y2,
                    tile = tile2.gameObject
                };
                var match = new Match(elements, false, 0);
                match.causeElem = 0;
                var matches = new List<Match>();
                matches.Add(match);
                return matches;
            }

            var origionalPos = new Vector2Int(x, y);

            TmpSwap(origionalPos, origionalPos + dir);
            try
            {
                return matchFinder.FindMatches(grid, new SwapEventData
                {
                    swappedPos1 = origionalPos,
                    swappedPos2 = origionalPos + dir
                });
            }
            finally
            {
                TmpSwap(origionalPos, origionalPos + dir);
            }
        }

        private void TmpSwap(Vector2Int a, Vector2Int b)
        {
            var tmp = grid.GetTileAt(a.x, a.y);
            grid.SetTileAt(grid.GetTileAt(b.x, b.y), a.x, a.y);
            grid.SetTileAt(tmp, b.x, b.y);
        }
    }
}