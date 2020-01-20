using UnityEngine;

namespace Util
{
    public abstract class Variable<T> : ScriptableObject, Resetable
    {

        [SerializeField]
        private T initialValue;


        public T Value;

        [SerializeField]
        private bool resetOnSceneLoad = true;

        private void OnEnable()
        {
            if (resetOnSceneLoad)
                FindObjectOfType<Resetter>()?.Register(this);
        }

        public void Reset()
        {
            Value = initialValue;
        }

        public void CaptureRuntimeValues()
        {
            initialValue = Value;
        }
    }
}