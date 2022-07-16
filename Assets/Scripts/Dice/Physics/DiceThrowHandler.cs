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

        [SerializeField]
    private DieVisual dieVisual;

    [SerializeField]
    private ActionDie action;

    public ActionDie ActionDie => action;


    [ContextMenu("Throw")]
    public Coroutine Throw()
    {
        rb.isKinematic = false;
        rb.detectCollisions = true;

        rb.velocity = Vector3.up * up + Vector3.forward * forward;
        rb.angularVelocity = Random.insideUnitSphere * angularForce.RandomInRange();
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

    public DieVisual GetRotatedVisual(){
        var visual = Instantiate(dieVisual);
        visual.physicalDie = this;
        visual.transform.localRotation = sideChecker.GetCurrentSide().Display.localRotation;
        return visual;
    }

}
