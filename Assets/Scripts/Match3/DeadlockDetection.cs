using UnityEngine;
using UnityEngine.Events;

namespace Match3
{

    [CreateAssetMenu(menuName = "Systems/DeadlockDetection")]
    public class DeadlockDetection : ScriptableObject
    {
        [SerializeField] private RuntimeGridData grid;
        [SerializeField] private UnityEvent DeadlockFound;
        [SerializeField] private MatchAccumulator matchFinder;

        public System.Collections.IEnumerator DetectDeadlock(UnityAction onDeadlock = null)
        {
            for (int x = 0; x < grid.Width; x++)
            {
                for (int y = 0; y < grid.Height; y++)
                {
                    var foundMatch = false;
                    // stop us looking down off the grid
                    if (y > 0)
                    {
                        foundMatch = CausesMatch(x, y, Vector2Int.down);
                    }

                    // stop us looking left off the grid
                    if (!foundMatch && x > 0)
                    {
                        foundMatch = CausesMatch(x, y, Vector2Int.left);
                    }

                    if (foundMatch)
                    {
                        // match found therefore no deadlock
                        yield return null;
                        yield break;
                    }
                    yield return null;
                }
            }

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
        }

        private void Notify()
        {
            DeadlockFound.Invoke();
        }

        private bool CausesMatch(int x, int y, Vector2Int dir)
        {
            var x2 = x + dir.x;
            var y2 = y + dir.y;
            var tile1 = grid.GetTileAt(x, y);
            var tile2 = grid.GetTileAt(x2, y2);

            // if one of them is a blocker we cant swap
            if (!grid.cells[x, y].IsPassable || !grid.cells[x2, y2].IsPassable)
            {
                return false;
            }
           
            // if anything creates matches on swap then we definietely have a match (tags are probably the wrong way to do this)
            if (tile1.CompareTag("MatchesOnSwap") || tile2.CompareTag("MatchesOnSwap"))
            {
                return true;
            }

            var origionalPos = new Vector2Int(x, y);

            TmpSwap(origionalPos, origionalPos + dir);
            try
            {
                return matchFinder.FindMatches(grid, new SwapEventData
                {
                    swappedPos1 = origionalPos,
                    swappedPos2 = origionalPos + dir
                }).Count > 0;
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