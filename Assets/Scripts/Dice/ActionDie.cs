using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ActionDie : MonoBehaviour
{

    [SerializeField]
    private DiceThrowHandler thrower;

    protected DiceThrowHandler Thrower => thrower;

    public virtual IEnumerator Throw(Vector3 dir, Vector3 start){
        yield return thrower.Throw(dir, start);
    }

    public virtual DieVisual Getvisual(){
        return thrower.GetRotatedVisual();
    }

    public abstract IEnumerator PlayAction();

}
