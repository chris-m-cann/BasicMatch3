using UnityEngine;
using Util;

namespace Match3
{
    public class ResizeToGrid : MonoBehaviour
    {
        [SerializeField] private RuntimeGridData grid;

        // this is another component assuming single unit tile widths. Is this really a problem?
        private float offset = -0.5f;
        public void Resize()
        {
            int width = grid.Width;
            int height = grid.Height;

            transform.localScale = new Vector3(width, height, 1);


            var x = ((float)width / 2) + offset;
            var y = ((float)height / 2) + offset;
            transform.position = transform.position.copy(x, y);
        }
    }
}
