using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Match3 
{
    public class InstantiateMatchEffect : MonoBehaviour, MatchEffect
    {
        [SerializeField] private GameObject effectPrefab;
        
        public void SpawnEffect(MatchElement[] elements)
        {
            var effect = Instantiate(effectPrefab, transform.position, Quaternion.identity);
            effect.GetComponent<MatchEffect>()?.SpawnEffect(elements);
        }
    }
}