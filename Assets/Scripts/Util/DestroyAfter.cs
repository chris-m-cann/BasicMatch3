using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Util
{
    public class DestroyAfter : MonoBehaviour
    {
        [SerializeField] private float lifetime;
        [SerializeField] private bool useScaledTime = true;

        public float Lifetime { get => lifetime; set => lifetime = value; }
        public bool UseScaledTime { get => useScaledTime; set => useScaledTime = value; }


        void Start()
        {
            if (useScaledTime)
            {
                this.ExecuteAfter(lifetime, () => Destroy(gameObject));
            } else
            {
                this.ExecuteAfterUnscaled(lifetime, () => Destroy(gameObject));
            }
        }

    }
}