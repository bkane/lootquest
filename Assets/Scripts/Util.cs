using System;
using System.Collections;
using UnityEngine;

public class Util
{
    public static IEnumerator DelayCall(float delayInSeconds, Action onComplete)
    {
        yield return new WaitForSeconds(delayInSeconds);
        onComplete();
    }
}

