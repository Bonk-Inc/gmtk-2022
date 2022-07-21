using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelCreator : MonoBehaviour
{
    // TODO Split script up in multiple scripts
    private CSVReader reader;

    [SerializeField, Header("Dependencies")]
    private GridGenerator generator;
    [SerializeField]
    private LevelInformation information;
    [SerializeField, Header("Creators")]
    private LevelPropCreator propCreator;
    [SerializeField]
    private LevelPlayerCreator playerCreator;
    [SerializeField]
    private LevelWallCreator wallCreator;
    
    [SerializeField, Header("Goal Prefabs")]
    private GameObject goalPrefab;

    private LevelCreator()
    {
        reader = new CSVReader();
    }

    public GridRow[] LoadLevel(string path, string level)
    {
        // Step 1: Get Data
        var data = reader.ReadFile(path, level);
        if (data.Length == 0) return generator.CreateGrid();

        // Step 2: Create Level Information
        var levelInfo = data[0][0].Split(";");
        if (levelInfo.Length != 4)
        {
            Debug.LogError("Level Information not set!");
            return generator.CreateGrid();
        }
        else
        {
            SetLevelInformation(levelInfo);
        }

        // Step 3: Instantiate Grid with correct size
        var grid = generator.CreateGrid(data.Length -1, data[1].Length);

        // Step 4: Read and use Data
        SetLevelData(data, grid);

        return grid;
    }

    private void SetLevelInformation(string[] levelInformation)
    {
        var levelTitle = levelInformation[0];
        var levelDescription = levelInformation[1];

        information.SetInformation(levelTitle, levelDescription);
        int.TryParse(levelInformation[2], out int gameModeInt);

        var settings = levelInformation[3].Split('_');
        settings[^1].Trim();
        
        var gamemode = ((LevelGamemodeTypes)gameModeInt) switch
        {
             _ => new ReachTheGoalGamemode(settings[0], settings[1], settings[2]),
        };

        information.GameMode = gamemode;
    }

    private void SetLevelData(string[][] data, GridRow[] grid)
    {
        for (int i = 1; i < data.Length; i++)
        {
            var row = grid[i-1];
            for (int j = 0; j < row.Tiles.Length; j++)
            {
                var tileData = data[i][j];
                var tile = row.Tiles[j];
                SetTileData(tileData, tile);
            }
        }
    }

    private void SetTileData(string data, GridTile tile)
    {
        data = data.Trim();
        if (data.Equals("NONE"))
        {
            SetGhostTile(tile);
            return;
        }
        var tileSettings = data.Split(':');
        SetTileVisual(tile, tileSettings[0]);
        SetTileObject(tile, tileSettings[1]);
    }

    private void SetGhostTile(GridTile tile)
    {
        tile.Blocked = true;
        tile.Visual.ToggleRender();
    }

    private void SetTileVisual(GridTile tile, string visualSetting)
    {
        // TODO Create script for finding the correct visual and a script to set visual on current tile.
    }

    private void SetTileObject(GridTile tile, string objectSetting)
    {
        if (objectSetting.Equals("Empty")) return;

        var setting = objectSetting.Split('_');
        
        // TODO Enum + Dictionary<Enum, Action>
        switch (setting[0])
        {
            case "PLAYER":
                playerCreator.SetPlayer(tile, setting[1]);
                break;
            case "WALL":
                wallCreator.SetWall(tile, setting[1]);
                break;
            case "PROP":
                propCreator.SetProp(tile, setting[1]);
                break;
            case "GOAL":
                SetGoal(tile, setting[1]);
                break;
            default:
                break;
        }
    }

    private void SetGoal(GridTile tile, string setting)
    {
        var goal = Instantiate(goalPrefab, tile.transform);
        goal.transform.position = new Vector3(goal.transform.position.x, 1, goal.transform.position.z);
        // TODO setting[0] Decides on goal model.
    }
}
