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
        // TODO What happens when the coroutine from movement.Move gets cancelled?
        yield return movement.Move(Thrower.LastThrow);
    }

}
