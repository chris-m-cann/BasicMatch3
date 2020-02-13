using System;
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

        [SerializeField] private int[] levelScoreMultipliers;
        [SerializeField] private GameObject scorePopupPrefab;

        private int cumulativeMatches = 0;
        private int currentMultiplier = 1;

        private void Start()
        {
            if (levelScoreMultipliers.Length > 0)
            {
                currentMultiplier = levelScoreMultipliers[0];
            }
        }

        public void ResetAccumulator()
        {
            cumulativeMatches = 0;
        }

        public void Score(Matches matches)
        {
            cumulativeMatches = Score(matches.matches, cumulativeMatches);
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
                var multiplier = (i + 1 + totalMatches) * currentMultiplier;
                var scoreIncrease = (multiplier * matches[i].score);


                // this should really be independant an abstracted
                InstantiateScorePopup(scoreIncrease, matches[i]);

                score.Value += scoreIncrease;
            }
            return newTotalMatches;
        }

        private void InstantiateScorePopup(int scoreIncrease, Match match)
        {
            var matchCause = match.Cause;
            var popup = Instantiate(scorePopupPrefab, new Vector3(matchCause.i, matchCause.j), Quaternion.identity, transform);
            popup.GetComponent<ScorePopup>().Popup(scoreIncrease);

        }

        public void OnNextLevel(int level)
        {
            var levelIndex = level - 1;
            if (levelScoreMultipliers.Length <= levelIndex)
            {
                currentMultiplier++;
            }
            else
            {
                currentMultiplier = levelScoreMultipliers[levelIndex];
            }
        }
    }
}