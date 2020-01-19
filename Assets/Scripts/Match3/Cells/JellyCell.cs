using System;
using UnityEngine;

namespace Match3
{
    public class JellyCell : Cell
    {
        [SerializeField] private int hp = 1;
        [SerializeField] private RuntimeGridData grid;
        [SerializeField] private SimpleHolderCell simpleCellPrefab;


        public override bool IsPassable => true;

        public void OnMatch(Matches matches)
        {
            foreach (var match in matches.matches)
            {
                if (Array.Exists(match.elements, it => it.tile == Child.gameObject))
                {
                    Damage(1);
                    return;
                }
            }
        }

        private void Damage(int damage)
        {
            hp -= damage;
            if (hp <= 0)
            {
                KillMe();
            }
        }

        private void KillMe()
        {
            int x = Mathf.RoundToInt(transform.position.x);
            int y = Mathf.RoundToInt(transform.position.y);

            var simpleCell = Instantiate(simpleCellPrefab.gameObject, transform.position, Quaternion.identity).GetComponent<Cell>();
            simpleCell.Child = Child;
            grid.cells[x, y] = simpleCell;
            Destroy(gameObject);
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.blue;
            Gizmos.DrawWireCube(transform.position, transform.localScale);
        }
    }

}