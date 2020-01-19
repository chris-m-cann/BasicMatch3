using UnityEngine;

namespace Match3
{
    public abstract class PowerupSpawner : ScriptableObject
    {
        public abstract Tile SpawnPowerup(ShapeType shape, Vector3 position);
    }
}