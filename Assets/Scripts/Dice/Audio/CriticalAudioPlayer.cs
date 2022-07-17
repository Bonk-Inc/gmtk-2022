using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CriticalAudioPlayer : MonoBehaviour
{
    [SerializeField]
    private DiceThrowHandler throwHandler;

    [SerializeField]
    private int criticalNumber = 0;

    [SerializeField]
    private PlaySound soundPlayer;

    private void Awake()
    {
        throwHandler.OnLand += (side) =>
        {
            if (side == criticalNumber)
            {
                soundPlayer.PlayClip();
            }
        };
    }
}
