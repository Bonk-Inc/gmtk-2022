using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ActionDie : MonoBehaviour
{

    public abstract IEnumerator Throw();

    public abstract IEnumerator PlayAction();

}
