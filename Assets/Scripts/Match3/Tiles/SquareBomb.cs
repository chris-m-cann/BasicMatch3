using UnityEngine;
using System.Collections.Generic;
using System.Linq;


namespace Match3
{
    // TODO(chris) - need spawned effects to take on the tag of the match that spawned them
    public class SquareBomb : Tile
    {
        [SerializeField] private int squareBombScore = 60;
        [SerializeField] private RuntimeGridData grid;


        public override void OnCreate(SwapEventData swapData, Tile cause)
        {
            if (cause == null)
            {
                // stay default
                return;
            }

            var mySr = GetComponent<SpriteRenderer>();
            var causeSr = cause.GetComponent<SpriteRenderer>();

            mySr.sprite = causeSr.sprite;
            tag = cause.tag;
        }

        public override List<Match> OnDestroyed()
        {
            var matches = new List<Match>();


            var elements = new List<MatchElement>();
            var maybePos = grid.FindPos(gameObject);

            if (maybePos == null)
            {
                Debug.LogError("Couldnt find SquareBomb on grid at " + transform.position);
                return matches;
            }

            var pos = maybePos.Value;
            /*
             * -  -  -  -  -
             * -  x  x  x  -
             * -  x  o  x  -
             * -  x  x  x  -
             * -  -  -  -  -
             * 
             * o = bomb
             * x = to be destroyed
             * - = dont care
             * 
             */

            // top row
            if (pos.y < grid.Height - 1)
            {
                elements.Add(grid.MatchElementAt(pos.x, pos.y + 1));

                if (pos.x > 0)
                {
                    elements.Add(grid.MatchElementAt(pos.x - 1, pos.y + 1));
                }

                if (pos.x < grid.Width - 1)
                {

                    elements.Add(grid.MatchElementAt(pos.x + 1, pos.y + 1));
                }
            }

            // left center
            if (pos.x > 0)
            {
                elements.Add(grid.MatchElementAt(pos.x - 1, pos.y));
            }

            // right center
            if (pos.x < grid.Width - 1)
            {

                elements.Add(grid.MatchElementAt(pos.x + 1, pos.y));
            }

            // bottom row
            if (pos.y > 0)
            {
                elements.Add(grid.MatchElementAt(pos.x, pos.y - 1));

                if (pos.x > 0)
                {
                    elements.Add(grid.MatchElementAt(pos.x - 1, pos.y - 1));
                }

                if (pos.x < grid.Width - 1)
                {

                    elements.Add(grid.MatchElementAt(pos.x + 1, pos.y - 1));
                }
            }

            // dont blow up blockers!
            var passableElements = elements.Where(it => grid.cells[it.i, it.j].IsPassable).ToArray();

            matches.Add(new Match(passableElements, false, squareBombScore));

            return matches;
        }
    }
}