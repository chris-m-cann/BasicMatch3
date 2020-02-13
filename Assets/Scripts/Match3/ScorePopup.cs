using UnityEngine;
using System.Collections;
using TMPro;

namespace Match3
{
    public class ScorePopup : MonoBehaviour
    {
        private TMPro.TextMeshPro text;

        private void Awake()
        {
            text = GetComponentInChildren<TextMeshPro>();
        }
        public void Popup(int score)
        {
            text.text = score.ToString();
        }
    }
}