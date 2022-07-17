using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelInformation : MonoBehaviour
{
    [SerializeField]
    private LevelGamemode gameMode;

    [SerializeField]
    private string title, description;

    public string Title { get => title; }
    public string Description { get => description; }
    public LevelGamemode GameMode { get => gameMode; set => gameMode = value; }

    public void SetInformation(string newTitle, string newDescription)
    {
        title = newTitle;
        description = newDescription;
    }
}
