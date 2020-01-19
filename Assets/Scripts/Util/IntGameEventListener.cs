using UnityEngine;
using UnityEngine.Events;

namespace Util
{
    public class IntGameEventListener : GameEventListenerBehaviour<int, IntGameEvent, IntUnityEvent>
    {
    }

    [CreateAssetMenu(menuName = "GameEvents/Int")]
    public class IntGameEvent : GameEvent<int>
    {

    }

    [System.Serializable]
    public class IntUnityEvent : UnityEvent<int>
    {

    }
}