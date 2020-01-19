using UnityEngine;
using Util;

namespace Match3
{
    [CreateAssetMenu(menuName = "TileBuilder/Random")]
    public class RandomTileBuilder : TileBuilder
    {
        [SerializeField] private GameObject[] prefabs;
        public override Tile Build(Vector3 position)
        {
            return Instantiate(prefabs.RandomElement(), position, Quaternion.identity).GetComponent<Tile>();
        }
    }
}
