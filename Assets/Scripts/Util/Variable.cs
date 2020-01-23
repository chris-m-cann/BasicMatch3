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
            {
                var resetters = Resources.FindObjectsOfTypeAll<RestablesRuntimeSet>();

                if (resetters != null)
                {
                    foreach(var resetter in resetters)
                    {
                        resetter.Add(this);
                    }
                }
            }
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