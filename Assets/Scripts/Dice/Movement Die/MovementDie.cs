using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MovementDie : ActionDie
{

    [SerializeField]
    private PlayerMovement movement;

    public override IEnumerator PlayAction()
    {
        yield return movement.Move(Thrower.LastThrow);
    }

}
