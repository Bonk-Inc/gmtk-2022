using System.Collections;
using System.Collections.Generic;
using System.Linq;
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

    [SerializeField]
    private HorizontalLayoutGroup visualContainer;

    private GamePhaseStateMachine statemachine;

    public override void EnterPhase(GamePhaseStateMachine statemachine)
    {
        this.statemachine = statemachine;
        rethrowButton.gameObject.SetActive(true);
        rethrowButton.onClick.AddListener(Rethrow);
        
        for (int i = 0; i < manager.Dice.Count; i++)
        {
            var visual = manager.Dice[i].Getvisual();
           selector.Spots[i].SetDie(visual);
            // visual.transform.SetParent(visualContainer.transform, false);
        }

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
        var selected = selector.GetSelectedDice();
        var physicalDice = selected.Select((selection) => selection.dice.physicalDie).ToArray();
        var actionDice = selected.Select((selection) => selection.dice.physicalDie.ActionDie).ToArray();
        selector.StopSelecting();
        foreach (var selection in selected)
        {
            Destroy(selection.dice.gameObject);
        }
        yield return StartCoroutine(manager.Throw(actionDice));
        for (int i = 0; i < actionDice.Length; i++)
        {
            selected[i].spot.SetDie(physicalDice[i].GetRotatedVisual());
        }
        statemachine.SetState(GamePhaseType.REARRANGE);
    }
}
