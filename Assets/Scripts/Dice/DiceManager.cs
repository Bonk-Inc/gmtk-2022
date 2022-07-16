using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiceManager : MonoBehaviour
{

    [SerializeField]
    private List<ActionDie> dice;

    public List<ActionDie> Dice => dice;

    public void PlayDiceActionSequence(){
        StartCoroutine(PlayDiceActions());
    }

    public IEnumerator PlayDiceActions(){
        for (int i = 0; i < dice.Count; i++)
        {
            yield return StartCoroutine(dice[i].PlayAction());
        }
    }

    public void ThrowAllDice(){
        StartCoroutine(ThrowAll());
    }

    public IEnumerator ThrowAll(){

        var diceThrows = new Coroutine[dice.Count];

        for (int i = 0; i < dice.Count; i++)
        {
            diceThrows[i] = StartCoroutine(dice[i].Throw());
        }
        yield return StartCoroutine(CoroutineHelper.WaitForAll(diceThrows));
    }

    public IEnumerator Throw(ActionDie[] dice){

        var diceThrows = new Coroutine[dice.Length];

        for (int i = 0; i < dice.Length; i++)
        {
            diceThrows[i] = StartCoroutine(dice[i].Throw());
        }
        yield return StartCoroutine(CoroutineHelper.WaitForAll(diceThrows));
    }

}
