using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartRoundPhase : GamePhase
{
    [SerializeField, Header("Dependencies")]
    private RoundTracker roundTracker;
    [SerializeField]
    private PlaySound sound;
    [SerializeField]
    private RoundUI roundUI;
    [SerializeField, Header("Settings")]
    private float startingTime = 4f;

    public override void EnterPhase(GamePhaseStateMachine statemachine) {
        StartCoroutine(RoundActions(statemachine));
    }
    public override void LeavePhase(){}

    public override void UpdateState(GamePhaseStateMachine statemachine)
    {
    }

    private IEnumerator RoundActions(GamePhaseStateMachine statemachine)
    {
        sound.PlayClip();
        roundTracker.NextRound();
        roundUI.ShowUI();
        yield return new WaitForSeconds(startingTime);
        roundUI.HideUI();
        statemachine.SetState(GamePhaseType.THROW);

    }
}
