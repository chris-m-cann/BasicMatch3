using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Util;

namespace Match3
{
    public class PlayHint : MonoBehaviour
    {
        // Todo(chris) move this to Variable<float> so can be set from a settings menu
        [SerializeField] private float hintAfterSecs = 7f;
        // Todo(chris) not really doing deadlock detection, split out the part we are using to find potential matches and the call it from the deadlock detection
        [SerializeField] private DeadlockDetection deadlock;
        [SerializeField] private GameStateVariable gameState;
        [SerializeField] private GameState allowedState;

        private ParticleSystem effect;
        private Vector3 offset;
        private Timer timeout = new Timer();

        private void Awake()
        {
            effect = GetComponent<ParticleSystem>();
            offset = transform.position;
        }

        private void Update()
        {
            if (timeout.Elapsed)
            {
                StartEffect();
            }
        }

        public void StartHintClock()
        {
            timeout.Start(hintAfterSecs);
        }

        public void StopHintClock()
        {
            timeout.Stop();
            // incase we are already playing
            StopEffect();
        }


        public void OnGameStateChanged(GameState state)
        {
            if (state == allowedState)
            {
                StartHintClock();
            } else
            {
                StopHintClock();
            }
        }

        private void StopEffect()
        {
            effect.Stop();
        }

        private void StartEffect()
        {
            timeout.Stop();
            if (!effect.isPlaying && gameState.Value == allowedState)
            {
                StartCoroutine(deadlock.FindPotentialMatches(matches =>
                {
                    if (matches.Count > 0 && gameState.Value == allowedState)
                    {
                        var randomMatch = matches.RandomElement();
                        // need to add code here to make sure still need to spawn effect
                        transform.position = randomMatch.elements[randomMatch.causeElem].tile.transform.position + offset;
                        effect.Play();
                    }
                }, false));

            }
        }


    }
}