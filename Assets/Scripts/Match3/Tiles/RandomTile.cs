using UnityEngine;

namespace Match3
{
    public class RandomTile : Tile
    {
        [SerializeField] private RandomTileBuilder builder;

        public override void OnCreate(SwapEventData swapData, Tile cause)
        {
            GenerateNewTile();
        }

        /* TODO(chris) this is bad for several reasons
         * 
         * 1. instantiating a tile you dont need just to destroy it
         * 2. this wont scale with new components beiong addded to tiles in general
         * 3. this doesnt account for some tiles having components that others dont
         * 
         * what you really want is to "become" that object and for this one to cease to exist?
         * alternatevely this moved to being at the cell level and we add onCreate funtionality to Cells
         * alternatively we could move Tiles to being purly data driven (more scriptable objects) and just pick one to by "my data" but this may skrew up our editor experience for visually laying out levels
         * 
         * investigate!!
         */
        private void GenerateNewTile()
        {
            var chosenTile = builder.Build(transform.position);
            var chosenSpriteRenderer = chosenTile.GetComponent<SpriteRenderer>();
            var myRenderer = GetComponent<SpriteRenderer>();

            myRenderer.sprite = chosenSpriteRenderer.sprite;
            myRenderer.color = chosenSpriteRenderer.color;

            tag = chosenTile.tag;
            Destroy(chosenTile.gameObject);
            //  Debug.Log($"Random tile genreation done @({Mathf.RoundToInt(transform.position.x)}, {Mathf.RoundToInt(transform.position.y)})");
        }
    }
}