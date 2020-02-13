using UnityEngine;
using System.Collections;

namespace Util
{
    public class PitchRandomiser : MonoBehaviour
    {
        [SerializeField] private AudioClip clip;
        [SerializeField] private AudioSource source;
        [SerializeField] private float minPitch;
        [SerializeField] private float maxPitch;


        private float defaultPitch;

        private void Start()
        {
            defaultPitch = source.pitch;
        }

        public void PlayClip()
        {
            source.pitch = Random.Range(minPitch, maxPitch);
            source.PlayOneShot(clip);
        }
    }

}