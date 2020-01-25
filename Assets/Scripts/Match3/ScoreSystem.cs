using System.Collections.Generic;
using UnityEngine;
using Util;

namespace Match3
{
    public class ScoreSystem : MonoBehaviour
    {
        // TODO(chris) - maybe move this to an event rather than just a variable so that things like spawning effects based on scores can be independedent
        // alternately make an ObservableVariable that fires and event when it is updated
        [SerializeField] private IntVariable score;

        [SerializeField] private int[] levelThresholds;
        [SerializeField] private IntUnityEvent onLevelThresholdReached;

        private int cumulativeMatches = 0;
        private int currentLevel = 0;
        private int nextLevelThreashold = 1000;

        private void Start()
        {
            if (levelThresholds.Length > 0)
            {
                nextLevelThreashold = levelThresholds[0];
            }
        }

        public void ResetAccumulator()
        {
            cumulativeMatches = 0;
        }

        public void Score(Matches matches)
        {
            cumulativeMatches = Score(matches.matches, cumulativeMatches);

            if (score.Value > nextLevelThreashold)
            {
                NextLevel();
            }
        }

        private int Score(List<Match> matches, int totalMatches)
        {
            // score increase is 1 * base score per each new match is in the match object. eg if match.score = 5
            // match 1 = 5, match 2 = 10, match 3 = 15 and so on
            // so a matches.Count of 3 gets use 15 + 10 + 5 = baseScore * ( 3 + 2 + 1) = baseScore * 3!
            // taking into count totalMatches we need to shift this up by a certain amount so say totalMatches was 2
            // then t should be baseScore * (5 + 4 + 3) = baseScore* (5! - 2!)
            var newTotalMatches = matches.Count + totalMatches;
            for (int i = 0; i < matches.Count; ++i)
            {
                var multiplier = i + totalMatches + 1;
                var scoreIncrease = (multiplier * matches[i].score);

                score.Value += scoreIncrease;
            }
            return newTotalMatches;
        }

        private void NextLevel()
        {
            ++currentLevel;

            // once we go past our configured level threshold amounts just double the score needed.
            // this should lead to an exponential increase that makes getting too far past the "designed" level cap unlikely
            if (levelThresholds.Length <= currentLevel)
            {
                nextLevelThreashold *= 2;
            } else
            {
                nextLevelThreashold = levelThresholds[currentLevel];
            }

            // +1 as currentLevel is 0 indexed
            onLevelThresholdReached.Invoke(currentLevel + 1);
        }
    }
}