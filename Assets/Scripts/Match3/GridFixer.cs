using System.Collections;
using UnityEngine;


namespace Match3
{
    public abstract class GridFixer : MonoBehaviour
    {
        public abstract IEnumerator FixGrid();
    }
}