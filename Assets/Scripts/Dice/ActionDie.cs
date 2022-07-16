using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ActionDie : MonoBehaviour
{

    [SerializeField]
    private DiceThrowHandler thrower;

    public virtual IEnumerator Throw(){
        yield return thrower.Throw();
    }

    public virtual DieVisual Getvisual(){
        return thrower.GetRotatedVisual();
    }

    public abstract IEnumerator PlayAction();

}
