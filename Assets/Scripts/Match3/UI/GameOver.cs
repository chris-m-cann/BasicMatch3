using UnityEngine;
using UnityEngine.UI;
using Util;

namespace Match3
{
    public class GameOver : MonoBehaviour
    {
        [SerializeField] private GameObject gameOverScreen;
        [SerializeField] private Text finalScore;
        [SerializeField] private IntVariable score;

        public void OnGameOver()
        {
            finalScore.text = "Score: " + score.Value;
            gameOverScreen.SetActive(true);
        }
    }

}