using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UISceneLoader : MonoBehaviour
{

    private SceneLoader loader;

    private void Start() {
        loader = SceneLoader.Instance;
    }

    public void LoadScene(string sceneName){
        loader.LoadScene(sceneName);
    }

    public void LoadSceneDirect(string sceneName){
        loader.LoadSceneDirect(sceneName);
    }
}
