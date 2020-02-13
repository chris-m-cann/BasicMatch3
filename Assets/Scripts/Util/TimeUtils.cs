using UnityEngine;
using System.Collections;

public static class TimeUtils
{
    public static void ScaleTime(this MonoBehaviour self, float scale, float duration)
    {
        self.StartCoroutine(ResetTimeScale(duration));
    }

    private static IEnumerator ResetTimeScale(float duration)
    {
        Time.timeScale = 0;
        yield return new WaitForSecondsRealtime(duration);
        Time.timeScale = 1;
    }
   
}
