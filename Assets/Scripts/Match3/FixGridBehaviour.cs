using System.Collections.Generic;
using UnityEngine;


namespace Match3
{
    public class FixGridBehaviour : GridFixer
    {
        [SerializeField] private RuntimeGridData grid;
        [SerializeField] private TileBuilder generator;
        [SerializeField] private float dropTileLerpTime = .041f;
        public override System.Collections.IEnumerator FixGrid()
        {
            yield return StartCoroutine(DropTiles());
            yield return StartCoroutine(AddNewTiles());
        }

        // Fix grid happening after tile destruction is finished
        private System.Collections.IEnumerator DropTiles()
        {
            var coroutines = new List<Coroutine>();
            for (int column = 0; column < grid.Width; column++)
            {
                coroutines.Add(StartCoroutine(DropColumn(column)));

                var delay = UnityEngine.Random.Range(.05f, .1f);
                yield return new WaitForSeconds(delay);
            }
            foreach (var c in coroutines)
            {
                yield return c;
            }
            yield return null;
        }

        private System.Collections.IEnumerator DropColumn(int column)
        {
            int gap = 0;
            for (int row = 0; row < grid.Height; row++)
            {
                // if a gap or a blocker
                if (grid.cells[column, row].IsEmpty || !grid.cells[column, row].IsPassable)
                {
                    ++gap;
                }
                // if there are gaps or lockers below
                else if (gap > 0)
                {
                    int y = row - gap;

                    // keep going up the grid until reach a gap or back where we started
                    // if back where we started then it wasall blockers below
                    while (!grid.cells[column, y].IsEmpty && y < row)
                    {
                        ++y;
                    }

                    // need to move down
                    // TODO(chris) - maybe control this via an animation?
                    if (y < row)
                    {
                        var tile1 = grid.GetTileAt(column, row);
                        var aPos = tile1.transform.position;
                        var bPos = new Vector3(column, y);
                        float lerpTime = dropTileLerpTime * (row - y);
                        float timeSpent = 0;
                        while (timeSpent < lerpTime)
                        {
                            timeSpent += Time.deltaTime;
                            tile1.transform.position = Vector3.Lerp(aPos, bPos, timeSpent / lerpTime);
                            yield return null;
                        }

                        grid.SetTileAt(grid.GetTileAt(column, row), column, y);
                        grid.SetTileAt(null, column, row);
                    }
                }
            }
            yield return null;
        }

        // part of fixing th grid, use same random tile generator as the random tile
        private System.Collections.IEnumerator AddNewTiles()
        {
            for (int column = 0; column < grid.Width; column++)
            {
                int top = grid.Height - 1;

                // get the top row that isnt a blocker
                while (!grid.cells[column, top].IsPassable) --top;

                // while the top is a gap keep filling and dropping down
                while (grid.cells[column, top].IsEmpty)
                {
                    grid.SetTileAt(generator.Build(new Vector3(column, top, transform.position.z)), column, top);
                    yield return DropColumn(column);
                }
            }
            yield return null;
        }
    }
}