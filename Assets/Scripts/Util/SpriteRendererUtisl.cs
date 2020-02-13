using UnityEngine;
using System.Collections;

namespace Util
{
    public static class SpriteRendererUtisl
    {

        public static SpriteRenderer CopyFrom(this SpriteRenderer self, SpriteRenderer other)
        {
            self.sprite = other.sprite;
            self.color = other.color;
            self.flipX = other.flipX;
            self.flipY = other.flipY;
            self.drawMode = other.drawMode;
            self.maskInteraction = other.maskInteraction;
            self.spriteSortPoint = other.spriteSortPoint;
            self.material = other.material;
            self.sortingLayerID = other.sortingLayerID;
            self.sortingLayerName = other.sortingLayerName;
            self.sortingOrder = other.sortingOrder;
            self.renderingLayerMask = other.renderingLayerMask;

            return self;
        }
    }
}