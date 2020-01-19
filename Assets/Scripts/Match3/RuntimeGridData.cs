using UnityEngine;

namespace Match3
{
    [CreateAssetMenu(menuName = "RuntimeSets/GridData")]
    public class RuntimeGridData : ScriptableObject
    {
        [System.NonSerialized]
        public Cell[,] cells;


        public Tile GetTileAt(int x, int y)
        {
            return cells[x, y].Child;
        }

        public void SetTileAt(Tile newTile, int x, int y)
        {
            cells[x, y].Child = newTile;
            if (newTile != null)
                newTile.transform.parent = cells[x, y].transform;
        }


        public int Width
        {
            get => cells.GetLength(0);
        }


        public int Height
        {
            get => cells.GetLength(1);
        }

        public Vector2Int? FindPos(GameObject tile)
        {
            for (int i = 0; i < Width; i++)
            {
                for (int j = 0; j < Height; j++)
                {
                    if (GetTileAt(i, j)?.gameObject == tile)
                    {
                        return new Vector2Int(i, j);
                    }
                }
            }
            return null;
        }

        public bool AreAdjacent(GameObject tile1, GameObject tile2)
        {
            var distance = (tile1.transform.position - tile2.transform.position).sqrMagnitude;

            // another place assuming both size and distance between tiles
            return Mathf.Approximately(distance, 1);
        }

        public MatchElement MatchElementAt(int x, int y) => new MatchElement
        {
            i = x,
            j = y,
            tile = cells[x, y].Child.gameObject
        };
    }
}