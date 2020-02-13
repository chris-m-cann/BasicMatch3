using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Match3 
{
    public class RegularMatchEffect : MonoBehaviour, MatchEffect
    {
        [SerializeField] private GameObject explosionPrefab;
        [SerializeField] private SpriteRenderer spriteRenderer;

        private void Awake()
        {
            // if we have not been set in the inspector then try and find it in gameobject
            if (spriteRenderer == null) 
                spriteRenderer = GetComponent<SpriteRenderer>();
        }

        public void SpawnEffect(MatchElement[] elements)
        {
            var explosion = Instantiate(explosionPrefab, transform.position, Quaternion.identity);
            
            var particles = explosion.GetComponent<ParticleSystem>();
            if (particles == null) return;


            var endColour = GetExplosionColour();

            // grab the last key in the colour gradent so we can animate from whit to our actual colour
            var colourModule = particles.colorOverLifetime;
            var colour = colourModule.color;
            var colourGradient = colour.gradient;
            var colourKeys = colourGradient.colorKeys;
            var key = colourKeys[colourGradient.colorKeys.Length - 1];

            // set them all the way back.
            // due to wierd property way they have set this up I need to go aaaall the way back through the objects
            key.color = endColour;
            colourKeys.SetValue(key, colourGradient.colorKeys.Length - 1);
            colourGradient.colorKeys = colourKeys;
            colour.gradient = colourGradient;
            colourModule.color = colour;

            var main = particles.main;
            main.useUnscaledTime = true;

            // set the explosion to be our shape
            if (spriteRenderer != null)
            {
                var sprites = particles.textureSheetAnimation;
                sprites.SetSprite(0, spriteRenderer.sprite);
            }
        }

        private Color GetExplosionColour()
        {
            if (spriteRenderer == null) return Color.white;

            // grab the colour of our tile
            return spriteRenderer.material.GetColor("_TintRGBA_Color_1");

        }
    }
}