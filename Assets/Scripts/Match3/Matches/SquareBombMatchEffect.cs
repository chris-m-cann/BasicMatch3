using UnityEngine;
using System.Collections;
using Util;

namespace Match3
{
    public class SquareBombMatchEffect : MonoBehaviour, MatchEffect
    {
        [SerializeField] private GameObject effectPrefab;
        [SerializeField] private float effectDuration = 1f;
        [SerializeField] private float freezeDuration = .5f;
        [SerializeField] private AudioClip effectSound;

        private DestroyAfter destroyer;
        private AudioSource sounds;

        private void Awake()
        {
            destroyer = GetComponent<DestroyAfter>();
            if (destroyer != null)
            {
                destroyer.Lifetime = Mathf.Max(effectSound.length, effectDuration, freezeDuration);
                destroyer.UseScaledTime = true;
            }

            sounds = GetComponent<AudioSource>();
        }

        public void SpawnEffect(MatchElement[] elements)
        {

            Instantiate(effectPrefab, transform.position, Quaternion.identity);

            sounds?.PlayOneShot(effectSound);

            TimeUtils.ScaleTime(this, 0f, freezeDuration);
        }
    }
}