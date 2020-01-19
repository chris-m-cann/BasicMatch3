using UnityEngine;

namespace Match3
{
    public struct SwapEventData
    {
        // the positions that have been swapped
        public Vector2Int swappedPos1;
        public Vector2Int swappedPos2;

        // sentinal value used to determine that no swap has take place
        public static SwapEventData None = new SwapEventData
        {
            swappedPos1 = Vector2Int.zero,
            swappedPos2 = Vector2Int.zero
        };
    }
}