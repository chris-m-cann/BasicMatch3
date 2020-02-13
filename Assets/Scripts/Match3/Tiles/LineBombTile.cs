using DigitalRuby.LightningBolt;
using System;
using System.Collections.Generic;
using UnityEngine;
using Util;

namespace Match3
{

    public class LineBombTile : Tile
    {
        [SerializeField] private RuntimeGridData grid;
        [SerializeField] private int BombScore = 50;
        [SerializeField] private GameObject idleEffect;

        private MatchEffect onDestroyEffect;
        private Vector2Int direction = Vector2Int.right;

        private void Awake()
        {
            onDestroyEffect = GetComponent<MatchEffect>();
        }

        public override void OnCreate(SwapEventData swapData, Tile cause)
        {
            if (cause == null)
            {
                // stay default
                return;
            }
            var theirSr = cause.GetComponent<SpriteRenderer>();
            GetComponent<SpriteRenderer>().CopyFrom(theirSr);

            tag = cause.tag;

            direction = swapData.swappedPos2 - swapData.swappedPos1;
            direction.x = Mathf.Abs(direction.x);
            direction.y = Mathf.Abs(direction.y);

            if (direction == Vector2Int.up)
            {
                idleEffect.transform.rotation = Quaternion.Euler(0, 0, 90);
            } else
            {
                idleEffect.transform.rotation = Quaternion.Euler(0, 0, 0);
            }
        }


        public override List<Match> OnDestroyed()
        {
            var myPos = new Vector2Int(Mathf.RoundToInt(transform.position.x), Mathf.RoundToInt(transform.position.y));

            MatchElement[] elements;

            if (direction == Vector2Int.up)
            {
                elements = VerticalBoom(myPos);
            }
            else
            {
                elements = HorizontalBoom(myPos);
            }

           
            onDestroyEffect?.SpawnEffect(elements);

            var match = new Match(elements, false, BombScore);
            var matches = new List<Match>();
            matches.Add(match);

            return matches;
        }


        private MatchElement[] HorizontalBoom(Vector2Int myPos)
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

            return elements.ToArray();
        }


        private MatchElement[] VerticalBoom(Vector2Int myPos)
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

            return elements.ToArray();
        }


    }
}