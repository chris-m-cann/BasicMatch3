using System.Linq;
using System.Collections;
using UnityEngine;

namespace Match3
{
    public class EnsureNoMatchesOnStart : MonoBehaviour
    {
        [SerializeField] private RuntimeGridData grid;
        [SerializeField] private TileBuilder generator;
        [SerializeField] private LayerMask matchableLayer;

        private string[,] GetCells()
        {
            var tiles = new string[grid.Width, grid.Height];

            for (int x = 0; x < grid.Width; x++)
            {
                for (int y = 0; y < grid.Height; y++)
                {
                    var child = grid.cells[x, y].Child;
                    tiles[x, y] = $"({grid.cells[x, y].transform.position.x}, {grid.cells[x, y].transform.position.y}) {child.tag}";
                }
            }

            return tiles;
        }

        public IEnumerator FixInitialGrid()
        {
            // need this here so coliders have updated from the shuffle (should be done suring fixed update as physics
            yield return new WaitForFixedUpdate();

            var cells = GetCells();
            for (int x = 0; x < grid.Width; x++)
            {
                for (int y = 0; y < grid.Height; y++)
                {
                    var tile = grid.GetTileAt(x, y);

                    while (CreatesMatch(tile.gameObject))
                    {
                        Debug.Log($"match found in initial grid @({x},{y})");
                        Destroy(tile.gameObject);
                        grid.SetTileAt(generator.Build(new Vector3(x, y, transform.position.x)), x, y);

                        tile = grid.GetTileAt(x, y);
                        // need this here so coliders have updated from the new addition (should be done suring fixed update as physics
                        yield return new WaitForFixedUpdate();
                    }

                }
            }
            yield return null;
        }

        // Todo(chris) - this seems gross and uneccesary, we have the grid now and match finding so why not use them?
        // while (find matches finds some matches) 
        //  swap out the tiles in the matches for a different random tile
        //
        // could also get more sophisticated with it and say a radom tile that doesnt match any of the surrounding tiles or something
        // or only pick one of the elemtns of the match and swap that out
        private bool CreatesMatch(GameObject g)
        {
            // ray cast 2 spaces in each cardinal direction, if in a row with your tag >= 3 then true

            /*
             * x
             * x
             * o
             */
            {
                var hits = Physics2D.RaycastAll(g.transform.position, Vector2.up, 1.9f, matchableLayer);

                if (hits.Length == 3 && hits.All(it => it.collider.gameObject.CompareTag(g.tag)))
                {
                    return true;
                }
            }

            /*
             * o
             * x
             * x
             */
            {
                var hits = Physics2D.RaycastAll(g.transform.position, Vector2.down, 1.9f, matchableLayer);

                if (hits.Length == 3 && hits.All(it => it.collider.gameObject.CompareTag(g.tag)))
                {
                    return true;
                }
            }

            /*
             * x
             * o
             * x
             */
            {
                var hitsUp = Physics2D.RaycastAll(g.transform.position, Vector2.up, 0.9f, matchableLayer);
                var hitsDown = Physics2D.RaycastAll(g.transform.position, Vector2.down, 0.9f, matchableLayer);

                if (hitsUp.Length == 2 && hitsUp.All(it => it.collider.gameObject.CompareTag(g.tag))
                    && hitsDown.Length == 2 && hitsDown.All(it => it.collider.gameObject.CompareTag(g.tag)))
                {
                    return true;
                }
            }


            // x x o
            {
                var hits = Physics2D.RaycastAll(g.transform.position, Vector2.left, 1.9f, matchableLayer);


                if (hits.Length == 3 && hits.All(it => it.collider.gameObject.CompareTag(g.tag)))
                {
                    return true;
                }
            }

            // o x x
            {
                var hits = Physics2D.RaycastAll(g.transform.position, Vector2.right, 1.9f, matchableLayer);


                if (hits.Length == 3 && hits.All(it => it.collider.gameObject.CompareTag(g.tag)))
                {
                    return true;
                }
            }

            // x o x
            {
                var hitsLeft = Physics2D.RaycastAll(g.transform.position, Vector2.left, 0.9f, matchableLayer);
                var hitsRight = Physics2D.RaycastAll(g.transform.position, Vector2.right, 0.9f, matchableLayer);


                if (hitsLeft.Length == 2 && hitsLeft.All(it => it.collider.gameObject.CompareTag(g.tag))
                    && hitsRight.Length == 2 && hitsRight.All(it => it.collider.gameObject.CompareTag(g.tag)))
                {
                    return true;
                }
            }



            return false;
        }

    }


}