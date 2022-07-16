using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadSettings : MonoBehaviour
{

    [SerializeField]
    private AudioSetting[] settings;

    private void Start() {
        foreach (var setting in settings)
        {
            setting.LoadSetting();
        }
    }
}
