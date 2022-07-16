using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiceThrowHandler : MonoBehaviour
{

    [SerializeField]
    private Rigidbody rb;

    [SerializeField]
    private FloatNumberRange angularForce;

    [SerializeField]
    private float up = 12, forward = 3;

    [SerializeField]
    private int stillCheckDelay = 10;

    [SerializeField]
    private float maxLandedPower = 0;

    [SerializeField]
    private DiceSideChecker sideChecker;

    private void Awake()
    {
        rb.isKinematic = true;
        rb.detectCollisions = false;
    }


    [ContextMenu("Throw")]
    public Coroutine Throw()
    {
        rb.isKinematic = false;
        rb.detectCollisions = true;

        rb.velocity = Vector3.up * up + Vector3.forward * forward;
        rb.angularVelocity = new Vector3(angularForce.RandomInRange(), angularForce.RandomInRange(), angularForce.RandomInRange());
        return StartCoroutine(DetectLanded());
    }

    private IEnumerator DetectLanded()
    {
        int stillCounter = 0;
        while (stillCounter < stillCheckDelay)
        {
            while (rb.angularVelocity.magnitude > maxLandedPower || rb.velocity.magnitude > maxLandedPower)
            {
                stillCounter = 0;
                yield return null;
            }

            stillCounter++;
            yield return null;
        }
    }

}
