using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiceAudioPlayer : MonoBehaviour
{
    [SerializeField]
    private PlaySound player;

    [SerializeField]
    private DiceAudioStarter[] starters;

    private bool firstOfRound = false;

    private void Awake() {
        for (int i = 0; i < starters.Length; i++)
        {
            starters[i].PlaySound += PlayAudio;
        }
    }

    public void StartRound(){
        firstOfRound = true;
    }

    private void PlayAudio(){
        if(firstOfRound){
            player.PlayClip();
            firstOfRound = false;
        }
    }

}
