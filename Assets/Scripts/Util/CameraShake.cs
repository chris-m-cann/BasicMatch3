using System.Collections;
using UnityEngine;

namespace Util
{
    public class CameraShake : MonoBehaviour
    {
        public void StartShake(float duration, float magnitide)
        {
            StartCoroutine(Shake(duration, magnitide));
        }

        private IEnumerator Shake(float duration, float magnitide)
        {
            var orginal = transform.position;

            var elapsed = 0f;

            while (elapsed < duration)
            {
                var newPos = Random.insideUnitCircle * magnitide;

                transform.position = orginal + new Vector3(newPos.x, newPos.y, 0);

                elapsed += Time.deltaTime;

                yield return null;
            }

            transform.position = orginal;
        }
    }
}