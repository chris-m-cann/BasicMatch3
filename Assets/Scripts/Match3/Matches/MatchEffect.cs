using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Match3 
{
    public interface MatchEffect
    {
        void SpawnEffect(MatchElement[] elements);
    }
}