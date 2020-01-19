using UnityEngine;
using UnityEngine.Events;
using Util;

namespace Match3
{
    public class MatchesGameEventListener : GameEventListenerBehaviour<Matches, MatchesGameEvent, MatchesUnityEvent>
    {
    }

    [CreateAssetMenu(menuName = "GameEvents/Matches")]
    public class MatchesGameEvent : GameEvent<Matches>
    {

    }

    [System.Serializable]
    public class MatchesUnityEvent : UnityEvent<Matches>
    {

    }
}