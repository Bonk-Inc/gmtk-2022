using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiceSideChecker : MonoBehaviour
{
    
    [SerializeField]
    private Transform[] sides;

    public int GetCurrentValue(){
        int smallestAngleTransformIndex = -1;
        float angle = float.MaxValue;
        for (int i = 0; i < sides.Length; i++)
        {
            var currentAngle = Vector3.Angle(transform.position + Vector3.down, transform.position + sides[i].up);
            if(currentAngle < angle){
                angle = currentAngle;
                smallestAngleTransformIndex = i;
            }
        }
        return smallestAngleTransformIndex+1;//+1 for index to value
    }

}
