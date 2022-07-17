using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelFinishedUI : MonoBehaviour
{

    [SerializeField]
    private string nextLevel = string.Empty;

    [SerializeField]
    private GameObject finishedGame, finishedLevel;

    public void Open()
    {
        gameObject.SetActive(true);
        if (nextLevel == string.Empty)
        {
            finishedGame.SetActive(true);
            finishedLevel.SetActive(false);
        }
        else
        {
            finishedGame.SetActive(false);
            finishedLevel.SetActive(true);
        }
    }


    public void NextLevel()
    {
        SceneLoader.Instance.LoadScene(nextLevel);
    }

}
