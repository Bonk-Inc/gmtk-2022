using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class CoroutineHelper
{

   public static IEnumerator WaitForAll(params Coroutine[] routines){
        for (int i = 0; i < routines.Length; i++)
        {
            yield return routines[i];
        }
    }
    

}
