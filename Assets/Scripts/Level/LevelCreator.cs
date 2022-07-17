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
    [SerializeField, Header("Settings - Most to be moved to different scripts.")]
    private StartPosition startPosition;

    [SerializeField, Header("Wall Prefabs")]
    private GameObject wallPrefab;
    [SerializeField]
    private GameObject wallCornerPrefab, wallNoSupportPrefab, wallNoSupportCornerPrefab;

    [SerializeField, Header("Prop Prefabs")]
    private GameObject standingLampPrefab;
    [SerializeField]
    private GameObject shortLampPrefab, armChairPrefab, bigSofaPrefab, bookshelfPrefab, bookPilePrefab, carpetPrefab, tvPrefab, tablePrefab, longTablePrefab, lampTablePrefab;


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
                SetTileData(tileData, tile, row);
            }
        }
    }

    private void SetTileData(string data, GridTile tile, GridRow row)
    {
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
        
        // TODO Could be an enum instead of just strings
        switch (setting[0])
        {
            case "PLAYER":
                SetPlayer(tile, setting[1]);
                break;
            case "WALL":
                SetWall(tile, setting[1]);
                break;
            case "PROP":
                SetProp(tile, setting[1]);
                break;
            case "GOAL":
                SetGoal(tile, setting[1]);
                break;
            default:
                break;
        }
        
    }

    private void SetPlayer(GridTile tile, string setting)
    {
        startPosition.SetPosition(tile.Position, true);
        var settings = setting.Split("-");

        // TODO setting[0] Decides on player model.

        // TODO Do we want NWSE or rather have it numbered?
        settings[1] = settings[1].Trim();
        startPosition.MoveDirection = settings[1] switch
        {
            "W" => Direction.West,
            "S" => Direction.South,
            "E" => Direction.East,
            _ => Direction.North,
        };
    }

    private void SetWall(GridTile tile, string setting)
    {
        var settings = setting.Split("-");
        tile.Blocked = true;

        settings[0] = settings[0].Trim();
        var prefab = settings[0] switch
        {
            "1" => wallPrefab,
            "2" => wallCornerPrefab,
            "3" => wallNoSupportPrefab,
            _ => wallNoSupportCornerPrefab
        };
        var wall = Instantiate(prefab, tile.transform);
        wall.transform.localPosition = Vector3.zero;


        settings[1] = settings[1].Trim();
        var rotation = settings[1] switch
        {
            "N" => -90f,
            "S" => 90f,
            "E" => 180f,
            _ => 0f,
        };

        Vector3 rotationVector = new Vector3(0, -rotation, 0);
        wall.transform.rotation = Quaternion.Euler(rotationVector);
    }

    private void SetProp(GridTile tile, string setting)
    {
        var settings = setting.Split("-");

        settings[0] = settings[0].Trim();
        var prefab = settings[0] switch
        {
            "LAMP1" => standingLampPrefab,
            "LAMP2" => shortLampPrefab,
            "CHAIR1" => armChairPrefab,
            "SOFA1" => bigSofaPrefab,
            "SHELF1" => bookshelfPrefab,
            "BOOKS1" => bookPilePrefab,
            "CARPET1" => carpetPrefab,
            "TV1" => tvPrefab,
            "TABLE1" => tablePrefab,
            "TABLE2" => longTablePrefab,
            _ => lampTablePrefab
        };
        var prop = Instantiate(prefab, tile.transform);
        prop.transform.localPosition = Vector3.zero;

        settings[1] = settings[1].Trim();
        var rotation = settings[1] switch
        {
            "N" => -90f,
            "S" => 90f,
            "E" => 180f,
            _ => 0f,
        };

        Vector3 rotationVector = new Vector3(0, -rotation, 0);
        prop.transform.rotation = Quaternion.Euler(rotationVector);

        settings[2] = settings[2].Trim();
        tile.Blocked = settings[2].Equals("F");

    }

    private void SetGoal(GridTile tile, string setting)
    {
        var goal = Instantiate(goalPrefab, tile.transform);
        goal.transform.position = new Vector3(goal.transform.position.x, 1, goal.transform.position.z);
        // TODO setting[0] Decides on goal model.
        
    }
}
