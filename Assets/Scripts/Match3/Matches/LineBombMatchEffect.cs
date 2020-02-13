using DigitalRuby.LightningBolt;
using System;
using System.Collections.Generic;
using UnityEngine;
using Util;

namespace Match3 
{
    public class LineBombMatchEffect : MonoBehaviour, MatchEffect
    {
        [SerializeField] private RuntimeGridData grid;
        [SerializeField] private GameObject lineEffectPrefab;
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
            int myY = Mathf.RoundToInt(transform.position.y);
            SpawnEffect(Array.TrueForAll(elements, it => it.j == myY));
        }

        private void SpawnEffect(bool isHorizontal)
        {
            Vector3 startPos;
            Vector3 endPos;

            if (isHorizontal)
            {
                startPos = new Vector3(0, transform.position.y);
                endPos = new Vector3(grid.Width, transform.position.y);
            } else
            {
                startPos = new Vector3(transform.position.x, 0);
                endPos = new Vector3(transform.position.x, grid.Height);
            }

            var lightning = Instantiate(lineEffectPrefab, Vector3.zero, Quaternion.identity);
            var script = lightning.GetComponent<LightningBoltScript>();
            if (script != null)
            {
                script.StartPosition = startPos;
                script.EndPosition = endPos;
            }


            this.ExecuteAfterUnscaled(effectDuration, () => Destroy(lightning));
            TimeUtils.ScaleTime(this, 0f, freezeDuration);
            sounds?.PlayOneShot(effectSound);
        }
    }
}