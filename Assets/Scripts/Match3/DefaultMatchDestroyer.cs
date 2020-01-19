using System.Collections.Generic;
using System.Linq;
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
                foreach(var elem in matches[i].elements)
                {
                    if (!distinctElements.Contains(elem))
                    {
                        matches.AddRange(elem.tile.GetComponent<Tile>().OnDestroyed());
                        distinctElements.Add(elem);
                    }
                    
                }
            }

            distinctElements.Clear();

            for (int i = 0; i < matches.Count; i++)
            {
                Match newMatch = SpawnEffects(matches[i], swapData);
                foreach (var elem in newMatch.elements)
                {
                    distinctElements.Add(elem);
                }
            }
            foreach (var elem in distinctElements)
            {
                //  Debug.Log($"Destroying tile @({elem.i}, {elem.j})");
                Destroy(elem.tile);
                grid.SetTileAt(null, elem.i, elem.j);
            }

        }

        // move to a seperate object that triggers in a match destruction
        private Match SpawnEffects(Match match, SwapEventData swapData)
        {

            if (match.canCreateShape == false)
            {
                return match;
            }

            var shape = MatchShapes.FindShape(match);

            if (shape != null)
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
                    grid.SetTileAt(powerup, cause.i, cause.j);
                    powerup.OnCreate(swapData, cause.tile.GetComponent<Tile>());
                    Destroy(cause.tile);
                    match.elements = match.elements.RemoveAt(causePos);
                }
            }

            return match;
        }

    }
}