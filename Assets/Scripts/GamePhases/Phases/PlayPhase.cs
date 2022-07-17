using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayPhase : GamePhase
{
    [SerializeField]
    private AudioSource sound;
    [SerializeField]
    private DiceManager dice;

    private bool left = false;

    public override void EnterPhase(GamePhaseStateMachine statemachine)
    {
        left = false;
        StartCoroutine(PlayActions(statemachine));
    }

    public override void LeavePhase(){
        left = true;
    }

    public override void UpdateState(GamePhaseStateMachine statemachine){}

    private IEnumerator PlayActions(GamePhaseStateMachine statemachine){
        sound.Play();
        yield return StartCoroutine(dice.PlayDiceActions());
        dice.Clear();
        sound.Stop();

        if(!left)
            statemachine.SetState(GamePhaseType.STARTROUND);//TODO might change to npc turn later
    }
}
