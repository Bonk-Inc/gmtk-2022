using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotationDie : ActionDie
{
    // Start is called before the first frame update
    [SerializeField]
    private PlayerMovement movement;

    public override IEnumerator PlayAction()
    {
        yield return movement.Rotate((Direction)Thrower.LastThrow);
    }
}
