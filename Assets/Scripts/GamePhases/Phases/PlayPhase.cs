using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayPhase : GamePhase
{
    [SerializeField]
    private AudioSource sound;
    [SerializeField]
    private DiceManager dice;

    public override void EnterPhase(GamePhaseStateMachine statemachine)
    {
        StartCoroutine(PlayActions(statemachine));
    }

    public override void LeavePhase(){}

    public override void UpdateState(GamePhaseStateMachine statemachine){}

    private IEnumerator PlayActions(GamePhaseStateMachine statemachine){
        sound.Play();
        yield return StartCoroutine(dice.PlayDiceActions());
        dice.Clear();
        sound.Stop();
        statemachine.SetState(GamePhaseType.STARTROUND);//TODO might change to npc turn later
    }
}
