using System.Linq;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;

namespace Match3
{
    public class GridCollector : MonoBehaviour
    {
        [SerializeField] private RuntimeGridData gridData;
        [SerializeField] private GridValidtor validator;
        [SerializeField] private UnityEvent OnGridCreated;

        private void Start()
        {
            StartCoroutine(InitialiseGame());
        }

        private IEnumerator InitialiseGame()
        {
            gridData.cells = BuildGridData();

            TriggerOnCreateAbilities();

            yield return StartCoroutine(validator.ValidateGrid());

            OnGridCreated.Invoke();
        }

        private Cell[,] BuildGridData()
        {
            var cellsInScene = FindObjectsOfType<Cell>();

            // this makes the assumption that they are all at least 1 unit apart
            var positions = cellsInScene.Select(it => new Vector2Int(Mathf.RoundToInt(it.transform.position.x), Mathf.RoundToInt(it.transform.position.y))).ToArray();

            int minX = positions.Min(it => it.x);
            int maxX = positions.Max(it => it.x);
            int minY = positions.Min(it => it.y);
            int maxY = positions.Max(it => it.y);

            // [0 1 2]  2 - 0 = 2 for 3 tiles so need width + 1 
            int width = maxX - minX + 1;
            int height = maxY - minY + 1;


            Cell[,] cells = new Cell[width, height];

            for (int i = 0; i < cellsInScene.Length; ++i)
            {
                // dont want to add disabled tiles to list so we can have mutiple grids in the scame scene and just enable the one we are testing
                if (cellsInScene[i].gameObject.activeInHierarchy == false) continue;

                var xIdx = positions[i].x - minX;
                var yIdx = positions[i].y - minY;


                cells[xIdx, yIdx] = cellsInScene[i];
            }

            return cells;
        }

        private void TriggerOnCreateAbilities()
        {
            foreach (var cell in gridData.cells)
            {
                cell?.Child?.OnCreate(SwapEventData.None, null);
            }
        }
    }
}