using UnityEngine;

namespace Util
{
    public class Timer
    {
        private float _elapsedTime = 0f;

        public bool Elapsed { get { return _elapsedTime <= Time.time; } private set { } }

        public void Start(float timeout)
        {
            _elapsedTime = Time.time + timeout;
        }
    }
}