using DigitalRuby.LightningBolt;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Util;

namespace Match3 
{
    public class GenocideBombEffect : MonoBehaviour, MatchEffect
    {
        [SerializeField] private GameObject effectPrefab;
        [SerializeField] private float effectDuration;
        [SerializeField] private float freezeDuration;
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
            foreach(var e in elements)
            {
                SpawnEffect(new Vector3(e.i, e.j));
            }

            sounds?.PlayOneShot(effectSound);

            TimeUtils.ScaleTime(this, 0f, freezeDuration);
        }

        private void SpawnEffect(Vector3 pos)
        {
            var effect = Instantiate(effectPrefab, Vector3.zero, Quaternion.identity);
            var script = effect.GetComponent<LightningBoltScript>();

            script.StartPosition = transform.position;
            script.EndPosition = pos;

            this.ExecuteAfterUnscaled(effectDuration, () => Destroy(effect));
        }

    }
}