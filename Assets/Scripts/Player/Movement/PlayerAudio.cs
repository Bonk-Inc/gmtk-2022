using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAudio : MonoBehaviour
{
    [SerializeField, Header("Dependencies")]
    private PlayerMovement movement;

    [SerializeField]
    private PlaySound movementClip, damageClip;


    private void Awake()
    {
        movement.OnMovementStart += PlayMovementSound;
    }
    private void PlayMovementSound()
    {
        movementClip?.PlayClip();
    }

    private void PlayDamagedSound()
    {
        damageClip?.PlayClip();
    }
}
