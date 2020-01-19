using System.Collections.Generic;
using UnityEngine;

namespace Match3
{

    public class LineBombTile : Tile
    {
        [SerializeField] private RuntimeGridData grid;
        [SerializeField] private int BombScore = 50;
        [SerializeField] private GameObject horizontalOverlay;
        [SerializeField] private GameObject verticalOverlay;

        private Vector2Int direction = Vector2Int.right;

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

            direction = swapData.swappedPos2 - swapData.swappedPos1;
            direction.x = Mathf.Abs(direction.x);
            direction.y = Mathf.Abs(direction.y);

            if (direction == Vector2Int.up)
            {
                horizontalOverlay.SetActive(false);
                verticalOverlay.SetActive(true);
            } else
            { 
                horizontalOverlay.SetActive(true);
                verticalOverlay.SetActive(false);
            }
        }


        public override List<Match> OnDestroyed()
        {
            var myPos = new Vector2Int(Mathf.RoundToInt(transform.position.x), Mathf.RoundToInt(transform.position.y));
            var matches = new List<Match>();

            if (direction == Vector2Int.up)
            {
                matches.Add(VerticalBoom(myPos));
            }
            else
            {
                matches.Add(HorizontalBoom(myPos));
            }
            return matches;
        }


        private Match HorizontalBoom(Vector2Int myPos)
        {
            var elements = new List<MatchElement>();

            for (int x = 0; x < grid.Width; ++x)
            {
                if (grid.cells[x, myPos.y].IsPassable && !grid.cells[x, myPos.y].IsEmpty)
                {
                    elements.Add(new MatchElement
                    {
                        i = x,
                        j = myPos.y,
                        tile = grid.GetTileAt(x, myPos.y).gameObject
                    });
                }
            }

            return new Match(elements.ToArray(), false, BombScore);
        }


        private Match VerticalBoom(Vector2Int myPos)
        {
            var elements = new List<MatchElement>();

            for (int y = 0; y < grid.Height; ++y)
            {
                if (grid.cells[myPos.x, y].IsPassable && !grid.cells[myPos.x, y].IsEmpty)
                {
                    elements.Add(new MatchElement
                    {
                        i = myPos.x,
                        j = y,
                        tile = grid.GetTileAt(myPos.x, y).gameObject
                    });
                }
            }

            return new Match(elements.ToArray(), false, BombScore);
        }

    }
}