using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class CSVReader
{
    public string[][] ReadFile(string path = "Levels", string fileName = "level")
    {
        var data = LoadResource(path, fileName);
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

    private TextAsset LoadResource(string path, string fileName)
    {
        return Resources.Load<TextAsset>(path + "/" + fileName);
    }
}
