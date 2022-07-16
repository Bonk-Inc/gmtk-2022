using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RethrowPhase : GamePhase
{

    [SerializeField]
    private Button rethrowButton;

    [SerializeField]
    private DiceSelector selector;

    [SerializeField]
    private DiceManager manager;

    private GamePhaseStateMachine statemachine;

    public override void EnterPhase(GamePhaseStateMachine statemachine)
    {
        this.statemachine = statemachine;
        rethrowButton.gameObject.SetActive(true);
        rethrowButton.onClick.AddListener(Rethrow);
        selector.StartSelecting();
    }

    public override void LeavePhase()
    {
        rethrowButton.gameObject.SetActive(false);
        rethrowButton.onClick.RemoveListener(Rethrow);
    }

    public override void UpdateState(GamePhaseStateMachine statemachine){}

    private void Rethrow(){
        StartCoroutine(ThrowSelectedDice());
    }

    private IEnumerator ThrowSelectedDice(){
        var dice = selector.GetSelectedDice();
        selector.StopSelecting();
        yield return StartCoroutine(manager.Throw(dice));
        statemachine.SetState(GamePhaseType.REARRANGE);
    }
}
