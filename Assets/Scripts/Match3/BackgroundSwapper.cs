using UnityEngine;
using System;
using Util;

namespace Match3
{
    public class BackgroundSwapper : MonoBehaviour
    {
        [SerializeField] private Color[] colours;

        private Camera cam;
        private Color currentColour;

        private void Awake()
        {
            cam = GetComponent<Camera>();
        }

        private void Start()
        {
            if (colours.Length == 0 || Array.TrueForAll(colours, it => it == colours[0])) {
                Debug.LogError("colours[] does not contain at least 2 unique elements");
                // just destroying this script, not the gameobject so effectivly disabling this functionality
                Destroy(this);
                return;
            }


            currentColour = colours.RandomElement();
            cam.backgroundColor = currentColour;
        }

        public void ChangeBackground()
        {
            var newColour = currentColour;

            while (currentColour == newColour)
            {
                newColour = colours.RandomElement();
            }

            cam.backgroundColor = newColour;
            currentColour = newColour;
        }
    }
}