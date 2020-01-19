using System;
using UnityEditor;
using UnityEngine;


namespace Util
{
    [System.Serializable]
    public struct RowofBools
    {
        public bool[] cells;

        public RowofBools(int columns, bool defaultSetting)
        {
            this.cells = new bool[columns];

            for (int i = 0; i < columns; i++)
            {
                this.cells[i] = defaultSetting;
            }
        }
    }

    [System.Serializable]
    public class GridOfBools
    {
        public RowofBools[] rows;

        public GridOfBools(int rows, int columns, bool onByDefault = true)
        {
            this.rows = new RowofBools[rows];
            for (int i = 0; i < rows; i++)
            {
                this.rows[i] = new RowofBools(columns, onByDefault);
            }
        }

        public GridOfBools() : this(5, 5)
        {

        }

        internal bool IsTileSet(int i, int j)
        {
            return rows[i].cells[j];
        }
    }


    /**
     * 
     * only works for classes containing
     * 1. a property called "rows"
     * 2. this "rows" member must be a serializable struct containing a member called "cells"
     * 3. "cells" must be an array of the type anted in the 2 d array (i.e. bools)
     * 
     * see GridOfBools above for example
     */
    public class TwoDArrayPropertyDrawer : PropertyDrawer
    {
        virtual protected string GetRowsName() => "rows";

        virtual protected string GetCellsName() => "cells";

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            EditorGUI.PrefixLabel(position, label);

            Rect newPosition = position;
            newPosition.y += 18f;
            SerializedProperty rows = property.FindPropertyRelative(GetRowsName());


            for (int i = rows.arraySize - 1; i != -1; i--)
            {
                SerializedProperty cells = rows.GetArrayElementAtIndex(i).FindPropertyRelative(GetCellsName());

                newPosition.height = 20;
                newPosition.width = 70;

                for (int j = 0; j < cells.arraySize; j++)
                {
                    EditorGUI.PropertyField(newPosition, cells.GetArrayElementAtIndex(j), GUIContent.none);
                    newPosition.x += newPosition.width;
                }

                newPosition.x = position.x;
                newPosition.y += 20;
            }
        }

        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {

            return (property.FindPropertyRelative(GetRowsName()).arraySize + 1) * 20;
        }
    }
}