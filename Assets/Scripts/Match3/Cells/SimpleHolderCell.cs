using UnityEngine;

namespace Match3
{
    public class SimpleHolderCell : Cell
    {
        public override bool IsPassable => true;

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.green;
            Gizmos.DrawWireCube(transform.position, transform.localScale);
        }
    }
}