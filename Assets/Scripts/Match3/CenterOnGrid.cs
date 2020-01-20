using UnityEngine;
using Util;

namespace Match3
{
    public class CenterOnGrid : MonoBehaviour
    {
        [SerializeField] private RuntimeGridData grid;
        [SerializeField] private Vector2 offset = new Vector2(.5f, .5f);
        public void Center()
        {
            int width = grid.Width;
            int height = grid.Height;


            var cam = GetComponent<Camera>();

            var x = ((float)width / 2) - 0.5f + offset.x;  // - 0.5f for half a tiles width
            var y = ((float)height / 2) - 0.5f + offset.y; // - 0.5f for half a tiles height
            transform.position = transform.position.copy(x, y);

            // + 1 as width is center to center the size between the leftmost and right most element
            cam.orthographicSize = width + 1;
        }
    }
}
