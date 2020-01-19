using UnityEngine;

namespace Match3
{
    public class BlankCell : Cell
    {
        public override bool IsPassable { get { return false; } }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireCube(transform.position, transform.localScale);
        }
    }
}