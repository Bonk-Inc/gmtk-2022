using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaySound : MonoBehaviour
{
    [SerializeField]
    private AudioSource source;
    [SerializeField]
    private List<AudioClip> clips;

    public void PlayClip() {
        var clip = clips.GetRandom();
        if(clip == null) return;
        source.clip = clip;
        source.Play();
    } 

}
