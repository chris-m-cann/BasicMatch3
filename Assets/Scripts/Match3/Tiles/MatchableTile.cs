using System.Collections.Generic;
using UnityEngine;

namespace Match3
{
    public class MatchableTile : Tile
    {
        private MatchEffect onDestroyEffect;

        private void Awake()
        {
            onDestroyEffect = GetComponent<MatchEffect>();
        }

        public override List<Match> OnDestroyed()
        {
            onDestroyEffect?.SpawnEffect(new MatchElement[0]);

            return new List<Match>();
        }
    }
}