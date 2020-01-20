using UnityEditor;
using UnityEngine;


namespace Util
{
    [CustomPropertyDrawer(typeof(GridOfGameObjects))]
    public class GameObjectInsectorDrawer : TwoDArrayPropertyDrawer
    {

    }


    [System.Serializable]
    public struct RowofGameObjects
    {
        public GameObject[] cells;

        public RowofGameObjects(int columns)
        {
            this.cells = new GameObject[columns];
        }
    }

    [System.Serializable]
    public class GridOfGameObjects
    {
        public RowofGameObjects[] rows;

        public GridOfGameObjects(int rows, int columns)
        {
            this.rows = new RowofGameObjects[rows];
            for (int i = 0; i < rows; i++)
            {
                this.rows[i] = new RowofGameObjects(columns);
            }
        }

        public GridOfGameObjects() : this(12, 6)
        {

        }

        public bool IsTileSet(int i, int j)
        {
            return rows[i].cells[j];
        }
    }
}