using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiceManager : MonoBehaviour
{

    [SerializeField]
    private Camera cam;

    [SerializeField]
    private List<ActionDie> dice;

    [SerializeField]
    private DiceSpot[] spots;

    [SerializeField]
    private DiceAudioPlayer player;


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

        if(dice.Count == 0)
         yield break;

        var diceThrows = new Coroutine[dice.Count];

        for (int i = 0; i < dice.Count; i++)
        {
            diceThrows[i] = StartCoroutine(dice[i].Throw(cam.transform.forward, cam.transform.position -cam.transform.forward*3 + (Vector3)Random.insideUnitCircle*15));
        }
        player.StartRound();
        yield return StartCoroutine(CoroutineHelper.WaitForAll(diceThrows));
    }

    public IEnumerator Throw(ActionDie[] dice){

        if(dice.Length == 0)
         yield break;

        var diceThrows = new Coroutine[dice.Length];

        for (int i = 0; i < dice.Length; i++)
        {
            diceThrows[i] = StartCoroutine(dice[i].Throw(cam.transform.forward, cam.transform.position -cam.transform.forward*3 + (Vector3)Random.insideUnitCircle*15));
        }
        player.StartRound();
        yield return StartCoroutine(CoroutineHelper.WaitForAll(diceThrows));
    }

    public void Clear(){
        for (int i = 0; i < spots.Length; i++)
        {
            if(spots[i].transform.childCount > 0){
                Destroy(spots[i].transform.GetChild(0).gameObject);
            }
        }
    }

}
