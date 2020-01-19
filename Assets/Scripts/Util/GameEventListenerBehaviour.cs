using UnityEngine;
using UnityEngine.Events;


namespace Util
{
    public abstract class GameEventListenerBehaviour<T, EventT, UnityEnentT> : MonoBehaviour, GameEventListener<T>
    where EventT : GameEvent<T> where UnityEnentT : UnityEvent<T>
    {
        [SerializeField] private EventT Event;

        [SerializeField] private UnityEnentT Response;


        public void OnEventRaised(T t)
        {
            //Debug.Log("invoking GameEvent");
            Response.Invoke(t);
        }

        private void OnEnable()
        {
            Event.RegisterListener(this);
        }

        private void OnDisable()
        {
            Event.RemoveListener(this);
        }
    }
}