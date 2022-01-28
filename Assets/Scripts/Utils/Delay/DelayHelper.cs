using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DelayHelper
{
    public static IEnumerator DelayByFrame(Action callback = null) {

        callback?.Invoke();
        yield return null;
    }
}
