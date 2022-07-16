using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RearrangePhase : GamePhase
{

    [SerializeField]
    private DiceRearanger rearanger;

    [SerializeField]
    private DiceManager manager;

    private GamePhaseStateMachine statemachine;

    public override void EnterPhase(GamePhaseStateMachine statemachine)
    {
        this.statemachine = statemachine;
        rearanger.StartRearrange();
        rearanger.OnRearrangeFinished += NextPhase;
        
    }

    public override void LeavePhase(){
        rearanger.OnRearrangeFinished -= NextPhase;
    }
    public override void UpdateState(GamePhaseStateMachine statemachine){}

    private void NextPhase(){
        statemachine.SetState(GamePhaseType.PLAY);
    }
}
