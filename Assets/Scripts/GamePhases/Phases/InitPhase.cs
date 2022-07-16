using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InitPhase : GamePhase
{
    public override void EnterPhase(GamePhaseStateMachine statemachine){
        //TODO fly camera over level
        statemachine.SetState(GamePhaseType.THROW);
    }
    public override void LeavePhase(){}

    public override void UpdateState(GamePhaseStateMachine statemachine)
    {
    }
}
