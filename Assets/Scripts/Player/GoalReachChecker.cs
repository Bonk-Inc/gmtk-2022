using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoalReachChecker : MonoBehaviour
{

    private const string GOAL_TAG = "Goal";

    [SerializeField]
    private PlayerMovement movement;

    private void Start() {
        movement.OnChangedPosition += (tile) => {
            if(tile.transform.childCount > 0 && tile.transform.GetChild(0).tag == GOAL_TAG){
                Debug.Log("GOALIEE");
            }
        };
    }
}