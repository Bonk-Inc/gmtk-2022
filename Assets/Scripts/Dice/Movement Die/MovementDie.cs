using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MovementDie : ActionDie
{

    [SerializeField]
    private Image image;

    [SerializeField]
    private List<Sprite> sides;

    [SerializeField]
    private IntNumberRange changeCountRange;
    [SerializeField]
    private float changeWaitTime;

    private int lastThrow = 0;

    public override IEnumerator PlayAction()
    {
        throw new System.NotImplementedException();
    }

    public override IEnumerator Throw()
    {
        int changecount = changeCountRange.RandomInRange();
        var randomIndex = 0;
        for (int i = 0; i < changecount; i++)
        {
            randomIndex = sides.GetRandomIndex();
            image.sprite = sides[randomIndex];
            yield return new WaitForSeconds(changeWaitTime);
        }
        lastThrow = randomIndex + 1;
        
    }
}
