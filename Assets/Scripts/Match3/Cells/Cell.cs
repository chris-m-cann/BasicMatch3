using UnityEngine;


namespace Match3
{

    public abstract class Cell : MonoBehaviour
    {
        public abstract bool IsPassable { get; }
        public Tile Child;
        public virtual bool IsEmpty { get { return Child == null; } }

        // Tile is a child in scene for visual purposes. find and set member for rest of the app to access
        private void Awake()
        {
            if (transform.childCount > 0)
            {
                var childTiles = transform.GetComponentsInChildren<Tile>();

                if (childTiles.Length > 0)
                {
                    Child = childTiles[0];
                }
            }
        }
    }

}