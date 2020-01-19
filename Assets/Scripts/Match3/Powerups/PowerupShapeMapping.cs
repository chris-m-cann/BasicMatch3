using System.Linq;
using UnityEngine;

namespace Match3
{
    [CreateAssetMenu(menuName = "TileBuilder/Powerups/ShapeMappings")]
    public class PowerupShapeMapping : PowerupSpawner
    {
        [System.Serializable]
        public struct PowerupShapePair
        {
            public ShapeType shape;
            public Tile powerupPrefab;
        }


        [SerializeField] private PowerupShapePair[] powerups;

        public override Tile SpawnPowerup(ShapeType shape, Vector3 position)
        {
            var powerup = powerups.FirstOrDefault(it => it.shape == shape).powerupPrefab;

            if (powerup != null)
            {
                return Instantiate(powerup, position, Quaternion.identity).GetComponent<Tile>();
            }
            else
            {
                return null;
            }
        }
    }
}