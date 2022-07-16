using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class AudioSetting : MonoBehaviour
{

    [SerializeField]
    private string settingName;
    [SerializeField]
    private string mixerSetting = "volume";

    [SerializeField]
    private Slider slider;

    [SerializeField]
    private AudioMixer mixer;

    [SerializeField]
    private float minDB = -80, maxDB = 0;

    private string playerPrefKey;

    private void Awake() {
        slider.maxValue = LogToLinear(maxDB);
        slider.minValue = LogToLinear(minDB);
        
        slider.value = LogToLinear(LoadSetting());
        
        slider.onValueChanged.AddListener((val) => SetVolume());
    }

    private void SetVolume(){
        mixer.SetFloat(mixerSetting, LinearToLog(slider.value));
        PlayerPrefs.SetFloat(playerPrefKey, LinearToLog(slider.value));
    }

    private float LogToLinear(float log){
        return Mathf.Pow(10, (log / 20));
    }

    private float LinearToLog(float linear){
        return Mathf.Log10(linear)*20;
    }

    public float LoadSetting(){
        playerPrefKey = $"vol_{settingName}";
        float startValue;
        if(PlayerPrefs.HasKey(playerPrefKey)){
            startValue = PlayerPrefs.GetFloat(playerPrefKey);
            mixer.SetFloat(mixerSetting, startValue);
        } else {
            mixer.GetFloat(mixerSetting, out startValue);
        }
        return startValue;
    }
}
