using System;
using UnityEngine;
using UnityEngine.Events;

namespace Match3
{
    public class CoreLogic : MonoBehaviour
    {

        [Header("Events")]
        [SerializeField] private UnityEvent ResolvingMatches;
        [SerializeField] private UnityEvent MatchesResolved;
        [SerializeField] private MatchesUnityEvent MatchesFound;

        [Header("Data")]
        [SerializeField] private RuntimeGridData grid;

        [Header("Systems")]
        [SerializeField] private GridFixer fixer;
        [SerializeField] private MatchAccumulator matchFinder;
        [SerializeField] private MatchDestroyer destroyer;



        public void Swap(GameObject tile1, GameObject tile2)
        {
            ResolvingMatches.Invoke();

            var a = grid.FindPos(tile1);
            var b = grid.FindPos(tile2);

            if (a != null && b != null)
            {
                StartCoroutine(SwapTile(a.Value, b.Value));
            }
            else
            {
                MatchesResolved.Invoke();
            }

        }

        private System.Collections.IEnumerator SwapTile(Vector2Int a, Vector2Int b)
        {
            try
            {
                yield return StartCoroutine(TweenTiles(a, b));

                var swapData = new SwapEventData
                {
                    swappedPos2 = a,
                    swappedPos1 = b
                };

                // trigger any effects that happen on tile swap (eg bombs exploding)
                var matches = grid.GetTileAt(a.x, a.y).OnSwap(grid.GetTileAt(b.x, b.y));
                matches.AddRange(grid.GetTileAt(b.x, b.y).OnSwap(grid.GetTileAt(a.x, a.y)));

                var matchCount = 0;
                var noMatches = true;
                do
                {
                    yield return null;


                    matches.AddRange(matchFinder.FindMatches(grid, swapData));

                    matchCount = matches.Count;

                    if (matchCount > 0)
                    {
                        noMatches = false;
                        // notify of matches found
                        MatchesFound.Invoke(new Matches
                        {
                            matches = matches
                        });

                        yield return null;

                        destroyer.DestroyMatches(matches, swapData);

                        yield return StartCoroutine(fixer.FixGrid());

                        matches.Clear();
                    }
                } while (matchCount > 0);

                // if there were never any matches then want to swap the tiles back
                if (noMatches)
                {
                    yield return new WaitForSeconds(0.3f);
                    yield return StartCoroutine(TweenTiles(a, b));
                }

                yield return null;
            } 
            finally
            {
                MatchesResolved.Invoke();
            }

        }


        // move to an object that can be swapped out for an animation
        private System.Collections.IEnumerator TweenTiles(Vector2Int a, Vector2Int b)
        {
            // tween the 2 tiles
            var tile1 = grid.GetTileAt(a.x, a.y);
            var tile2 = grid.GetTileAt(b.x, b.y);
            var aPos = tile1.transform.position;
            var bPos = tile2.transform.position;
            float lerpTime = .1f;
            float timeSpent = 0;
            while (timeSpent < lerpTime)
            {
                timeSpent += Time.deltaTime;
                tile1.transform.position = Vector3.Lerp(aPos, bPos, timeSpent / lerpTime);
                tile2.transform.position = Vector3.Lerp(bPos, aPos, timeSpent / lerpTime);
                yield return null;
            }

            grid.SetTileAt(tile2, a.x, a.y);
            grid.SetTileAt(tile1, b.x, b.y);

            // last update
            yield return null;

        }

    }
}