using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiceThrowHandler : MonoBehaviour
{

    [SerializeField]
    private Rigidbody rb;

    [SerializeField]
    private FloatNumberRange angularForce, throwForce;

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

    public int LastThrow { get; private set; } = 0;

    public event Action<int> OnLand;

    [ContextMenu("Throw")]
    public Coroutine Throw(Vector3 dir, Vector3 start)
    {
        rb.isKinematic = false;
        rb.detectCollisions = true;

        rb.position = start;
        rb.velocity = dir.normalized * throwForce.RandomInRange();
        rb.angularVelocity = UnityEngine.Random.insideUnitSphere * angularForce.RandomInRange();
        return StartCoroutine(DetectLanded());
    }

    private IEnumerator DetectLanded()
    {
        int stillCounter = 0;
        float time = 0;
        while (stillCounter < stillCheckDelay)
        {



            while (rb.angularVelocity.magnitude > maxLandedPower || rb.velocity.magnitude > maxLandedPower)
            {
                if (time > 5)
                {
                    rb.velocity = Vector3.zero;
                    rb.position = GameObject.Find("Default Throw Position").transform.position;
                    time = 0;
                }
                time += Time.deltaTime;
                stillCounter = 0;
                yield return null;
            }
            stillCounter++;
            yield return null;
        }
        LastThrow = sideChecker.GetCurrentSide().Value;
        OnLand?.Invoke(LastThrow);
    }

    public DieVisual GetRotatedVisual()
    {
        var visual = Instantiate(dieVisual);
        visual.physicalDie = this;
        visual.transform.localRotation = sideChecker.GetCurrentSide().Display.localRotation;
        return visual;
    }

}
