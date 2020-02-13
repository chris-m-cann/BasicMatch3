using System;
using System.Collections.Generic;
using UnityEngine;

namespace Match3 
{
    public class CompositeMatchEffect : MonoBehaviour, MatchEffect
    {
        private MatchEffect[] childEffects;

        private void Awake()
        {
            childEffects = GetComponentsInChildren<MatchEffect>();

        }

        public void SpawnEffect(MatchElement[] elements)
        {
            if (childEffects == null) return;

            foreach(var effect in childEffects)
            {
                // GetComponentsInChildren returns matching components from this gameObject too!!
                // we want macthing components from the parent but we dont want to process this one as
                // that will recurse infinitely
                if (effect == this) continue;

                effect.SpawnEffect(elements);
            }
        }
    }
}