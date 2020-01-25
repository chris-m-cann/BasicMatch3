using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Match3
{
    public class LevelUI : MonoBehaviour
    {
        private Text text;
        // Start is called before the first frame update
        void Start()
        {
            text = GetComponent<Text>();
        }

        public void SetLevel(int lvl)
        {
            text.text = "Level " + lvl;
        }
    }
}