using UnityEngine;
using UnityEngine.UI;
using Util;

namespace Match3
{
    public class Match3Score : MonoBehaviour
    {
        [SerializeField] private IntVariable score;
        [SerializeField] private float scoreUpdateSpeed = 5f;

        private Text text;
        private float displayScore = 0;

        private void Start()
        {
            text = GetComponent<Text>();
            text.text = displayScore.ToString();
        }

        private void Update()
        {
            if (displayScore != score.Value)
            {
                displayScore += scoreUpdateSpeed * Time.deltaTime;
                displayScore = Mathf.Min(displayScore, score.Value);
                text.text = ((int)displayScore).ToString();
            }
        }
    }
}