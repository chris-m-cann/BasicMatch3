using UnityEngine;
using UnityEngine.Events;


namespace Util
{
    public class VoidGameEventListener : GameEventListenerBehaviour<Void, VoidGameEvent, VoidUnityEvent>
    {

    }

    [CreateAssetMenu(menuName = "GameEvents/Void")]
    public class VoidGameEvent : GameEvent<Void>
    {
        public void Raise() => Raise(Void.Instance);
    }

    [System.Serializable]
    public class VoidUnityEvent : UnityEvent<Void> { }

}