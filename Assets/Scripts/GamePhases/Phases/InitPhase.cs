using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InitPhase : GamePhase
{
    [SerializeField, Header("Dependencies")]
    private PlayerMovement playerMovement;
    [SerializeField, Header("Settings")]
    private Vector2Int startPosition;

    public override void EnterPhase(GamePhaseStateMachine statemachine) {
        print(startPosition);
        playerMovement.SetInstantLocation(startPosition);
        //TODO fly camera over level
        statemachine.SetState(GamePhaseType.THROW);
    }
    public override void LeavePhase(){}

    public override void UpdateState(GamePhaseStateMachine statemachine)
    {
    }
}
