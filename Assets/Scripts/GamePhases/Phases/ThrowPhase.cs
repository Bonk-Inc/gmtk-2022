using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowPhase : GamePhase
{
    [SerializeField]
    private DiceManager dice;

    public override void EnterPhase(GamePhaseStateMachine statemachine)
    {
        StartCoroutine(ThrowDice(statemachine));
    }

    public override void LeavePhase(){}

    public override void UpdateState(GamePhaseStateMachine statemachine){}

    private IEnumerator ThrowDice(GamePhaseStateMachine statemachine){
        yield return StartCoroutine(dice.ThrowAll());
        statemachine.SetState(GamePhaseType.REARRANGE);
    }

}
