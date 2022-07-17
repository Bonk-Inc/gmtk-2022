using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiceAudioStarter : MonoBehaviour
{
    private const string LEVEL_TAG = "Level";

    public event Action PlaySound;

    private void OnCollisionEnter(Collision other) {
        if(other.collider.CompareTag(LEVEL_TAG)){
            PlaySound?.Invoke();
        }
    }
}
