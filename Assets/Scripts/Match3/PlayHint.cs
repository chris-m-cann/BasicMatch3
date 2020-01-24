using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Util;

namespace Match3
{
    public class PlayHint : MonoBehaviour
    {
        // Todo(chris) move this to Variable<float> so can be set from a settings menu
        public float hintAfterSecs = 7f;
        // Todo(chris) not really doing deadlock detection, split out the part we are using to find potential matches and the call it from the deadlock detection
        public DeadlockDetection deadlock;

        private ParticleSystem effect;
        private Vector3 offset;
        private Timer timeout = new Timer();
        // variable to track if we have been stopped between deciding to look for a match to hint and the match being found
        private bool stoppedDuringDetection = false;

        private void Awake()
        {
            effect = GetComponent<ParticleSystem>();
            offset = transform.position;
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.H))
            {
                ToggleHints();
            }

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
            // not sure if it is during detection at this point but cant hurt to set it
            stoppedDuringDetection = true;
        }

        private void ToggleHints()
        {
            if (effect.isPlaying)
            {
                StopEffect();
            }
            else
            {
                StartEffect();
            }
        }

        private void StopEffect()
        {
            effect.Stop();
        }

        private void StartEffect()
        {
            timeout.Stop();
            if (!effect.isPlaying)
            {
                // reset the flag here so we know that if it has been set by the time the Action triggers the we know not to display the hint
                stoppedDuringDetection = false;

                StartCoroutine(deadlock.FindPotentialMatches(matches =>
                {
                    if (matches.Count > 0 && !stoppedDuringDetection)
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