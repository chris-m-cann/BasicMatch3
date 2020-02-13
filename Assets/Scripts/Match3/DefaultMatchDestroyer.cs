using System.Collections.Generic;
using System.Linq;
using System;
using UnityEngine;
using Util;

namespace Match3
{
    /*
     * needs restructuriung and teasing apart so that 
     * - an event is fired when things are destroyed so we can spawn effects and such
     * - shape based tile generation is one seperately
     * - call on destroy on tiles beiong destroyed
     */
    [CreateAssetMenu(menuName = "Systems/MatchDestroyer/Default")]
    public class DefaultMatchDestroyer : MatchDestroyer
    {
        [SerializeField] private RuntimeGridData grid;
        [SerializeField] private PowerupSpawner powerups;

        public override void DestroyMatches(List<Match> matches, SwapEventData swapData)
        {
            var distinctElements = new HashSet<MatchElement>();
            // for each element run any on destruction stuff and add to end of matches list
            for (int i = 0; i < matches.Count; i++)
            {
                foreach (var elem in matches[i].elements)
                {
                    if (!distinctElements.Contains(elem))
                    {
                        matches.AddRange(elem.tile.GetComponent<Tile>().OnDestroyed());
                        distinctElements.Add(elem);
                    }

                }
            }

            var spawned = new List<MatchElement>();

            // for each match see if they for the shapes for a powerup
            // add any tiles caught in startup effects to the elements to be destroyed 
            // add any powerups spawned to a list to be added to the grid once the other tiles have been destroyed
            for (int i = 0; i < matches.Count; i++)
            {
                Effect effect = SpawnEffects(matches[i], swapData);
                if (effect.match.HasValue)
                {
                    foreach (var elem in effect.match.Value.elements)
                    {
                        distinctElements.Add(elem);
                    }
                }

                if (effect.spawned.HasValue)
                {
                    spawned.Add(effect.spawned.Value);
                }
            }

            // actually destroy the tiles
            foreach (var elem in distinctElements)
            {
                Destroy(elem.tile);
                grid.SetTileAt(null, elem.i, elem.j);
            }

            // add spawned powerups to the grid
            foreach (var newTile in spawned)
            {
                grid.SetTileAt(newTile.tile.GetComponent<Tile>(), newTile.i, newTile.j);
            }
        }

        // move to a seperate object that triggers in a match destruction
        private Effect SpawnEffects(Match match, SwapEventData swapData)
        {

            if (match.canCreateShape == false)
            {
                return Effect.None;
            }

            var shape = MatchShapes.FindShape(match);

            if (shape == null)
            {
                return Effect.None;
            }
            else
            {
                int causePos;
                // right now if we couldnt work out the element that caused the match (the one you swapped)
                // then we just pick the last one in the list
                // TODO(chris) - a match is always cause by _some_ element moving, that should be the cause
                if (match.causeElem > -1)
                {
                    causePos = match.causeElem;
                }
                else
                {
                    causePos = match.elements.Length - 1;
                }
                var cause = match.elements[causePos];

                var powerup = powerups.SpawnPowerup(shape.Value, new Vector3(cause.i, cause.j, cause.tile.transform.position.z));

                if (powerup != null)
                {
                    powerup.OnCreate(swapData, cause.tile.GetComponent<Tile>()); 
                    return new Effect
                    {
                        match = null,
                        spawned = new MatchElement { i = cause.i, j = cause.j, tile = powerup.gameObject }
                    };
                } else
                {
                    return Effect.None;
                }
            }



        }

        private struct Effect
        {
            // I know match looks dumb right now but just seemed like an effect that imediately destroys some tiles 
            // seems like a pretty normal thing to want
            public Match? match;
            public MatchElement? spawned;

            public static Effect None = new Effect
            {
                match = null,
                spawned = null
            };
        }

    }
}