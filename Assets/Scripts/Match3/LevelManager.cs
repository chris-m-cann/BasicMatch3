using UnityEngine;
using UnityEngine.Events;
using System.Collections;
using Util;

namespace Match3
{
    public class LevelManager : MonoBehaviour
    {
        [SerializeField] private IntVariable score;
        [SerializeField] private int[] levelThresholds;
        [SerializeField] private GameState allowedStateToTransaitionDuring;
        [SerializeField] private Shuffler shuffler;
        [SerializeField] private GridValidtor validator;

        [SerializeField] private IntUnityEvent onStartingLevelTransition;
        [SerializeField] private IntUnityEvent onNextLevelBegun;


        private int currentLevel = 0;
        private int nextLevelThreashold = 1000;

        private void Start()
        {
            if (levelThresholds.Length > 0)
            {
                nextLevelThreashold = levelThresholds[0];
            }
        }

        public void SeeIfNextLevelTime(GameState state)
        {
            if (state == allowedStateToTransaitionDuring && score.Value > nextLevelThreashold)
            {
                StartCoroutine(NextLevel());    
            }
        }

        private IEnumerator NextLevel()
        {
            ++currentLevel;

            // once we go past our configured level threshold amounts just double the score needed.
            // this should lead to an exponential increase that makes getting too far past the "designed" level cap unlikely
            if (levelThresholds.Length <= currentLevel)
            {
                nextLevelThreashold *= 2;
            }
            else
            {
                nextLevelThreashold = levelThresholds[currentLevel];
            }

            // fire "Transitinng levels"
            onStartingLevelTransition.Invoke(currentLevel + 1);

            yield return null;

            // reshuffle the board
            shuffler.ShuffleBoard();
            yield return StartCoroutine(validator.ValidateGrid());

            // fire grid built
            onNextLevelBegun.Invoke(currentLevel + 1);
        }
    }
}