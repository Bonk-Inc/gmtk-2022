using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class RoundTracker : MonoBehaviour
{
    private int round = 0;

    public int Round { get => round; }

    public event Action<int> OnRoundStart;


    public void NextRound()
    {
        round++;
        OnRoundStart?.Invoke(round);
    }

    public void ResetRounds()
    {
        round = 0;
    }
}
