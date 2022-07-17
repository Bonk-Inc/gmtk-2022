using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class CSVReader : MonoBehaviour
{
    [SerializeField]
    private string path = "Levels"; // TODO Should this be here, or as a parameter?

    public string[][] ReadFile(string fileName)
    {
        var data = LoadResource(fileName);
        return ReadData(data.text);
    }

    private string[][] ReadData(string data)
    {
        var rows = data.Split('\n');
        string[][] result = new string[rows.Length-1][];

        for (int i = 0; i < rows.Length; i++)
        {
            var column = rows[i].Split(',');
            if(column[0].Length != 0) result[i] = column;
        }
        return result;
    }

    private TextAsset LoadResource(string fileName)
    {
        return Resources.Load<TextAsset>(path + "/" + fileName);
    }
}
