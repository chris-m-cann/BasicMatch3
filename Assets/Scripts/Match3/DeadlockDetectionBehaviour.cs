using UnityEngine;
using UnityEngine.Events;

namespace Match3
{
    public class DeadlockDetectionBehaviour : MonoBehaviour
    {
        [SerializeField] private DeadlockDetection detection;
        [SerializeField] private UnityEvent DeadlockFound;

        public void DetectDeadlock()
        {
            StartCoroutine(detection.DetectDeadlock(RaiseEvent));
        }

        private void RaiseEvent()
        {
            DeadlockFound.Invoke();
        }
    }
}