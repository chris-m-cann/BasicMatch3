using UnityEngine;


namespace Match3
{
    public abstract class TileBuilder : ScriptableObject
    {
        public abstract Tile Build(Vector3 position);
    }
}
