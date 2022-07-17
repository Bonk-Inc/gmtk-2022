using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAudio : MonoBehaviour
{

    [SerializeField]
    private PlaySound damageClip;

    private void PlayDamagedSound() // TODO Implement when: Player health or death is added
    {
        damageClip?.PlayClip();
    }
}
