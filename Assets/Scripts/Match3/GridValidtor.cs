using UnityEngine;
using System.Collections;

namespace Match3
{
    // not the best name as it fixes the grid as well as validates it
    public class GridValidtor : MonoBehaviour
    {
        [SerializeField] private DeadlockDetection deadlock;
        [SerializeField] private EnsureNoMatchesOnStart fixInitialMatches;
        [SerializeField] private Shuffler shuffler;

        public IEnumerator ValidateGrid()
        {

            yield return StartCoroutine(fixInitialMatches.FixInitialGrid());


            bool hasDeadlock = false;

            do {
                yield return StartCoroutine(deadlock.DetectDeadlock(this, () => hasDeadlock = true));

                if (hasDeadlock)
                {
                    Debug.Log("Had a dealock so shuffling");
                    shuffler.ShuffleBoard();
                    yield return StartCoroutine(fixInitialMatches.FixInitialGrid());
                }
            } while (hasDeadlock);

            // by the time we get here we should have a board with no deadlock and no initial matches
            
        }

    }
}