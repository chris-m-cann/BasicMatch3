using UnityEngine;
using System.Collections;

namespace Util
{
    [CreateAssetMenu(menuName = "GameEvents/Void")]
    public class VoidGameEvent : GameEvent<Void>
    {
        public void Raise() => Raise(Void.Instance);
    }
}