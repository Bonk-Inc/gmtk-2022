using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelCreator : MonoBehaviour
{
    // TODO Split script up in multiple scripts


    [SerializeField, Header("Dependencies")]
    private CSVReader reader;

    [SerializeField]
    private GridGenerator generator;
    [SerializeField, Header("Settings - Most to be moved to different scripts.")]
    private StartPosition startPosition;

    [SerializeField]
    private GameObject wallPrefab;

    [SerializeField]
    private GameObject goalPrefab;

    public GridRow[] LoadLevel(string level)
    {
        // Step 1: Get Data
        var data = reader.ReadFile(level);
        if (data.Length == 0) return generator.CreateGrid();

        // Step 2: Instantiate Grid with correct size
        var grid = generator.CreateGrid(data.Length, data[0].Length);

        // Step 3: Read and use Data
        SetLevelData(data, grid);

        return grid;
    }

    private void SetLevelData(string[][] data, GridRow[] grid)
    {
        for (int i = 0; i < grid.Length; i++)
        {
            var row = grid[i];
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
        var wall = Instantiate(wallPrefab, tile.transform);
        wall.transform.position = new Vector3(wall.transform.position.x, 1, wall.transform.position.z);
        // TODO setting[0] Decides on wall model.
        settings[1] = settings[1].Trim();
        var rotation = settings[1] switch
        {
            "N" => -90f,
            "S" => 90f,
            "E" => 180f,
            _ => 0f,
        };

        Vector3 rotationVector = new Vector3(-90, -rotation, 0);
        wall.transform.rotation = Quaternion.Euler(rotationVector);
    }

    private void SetGoal(GridTile tile, string setting)
    {
        var goal = Instantiate(goalPrefab, tile.transform);
        goal.transform.position = new Vector3(goal.transform.position.x, 1, goal.transform.position.z);
        // TODO setting[0] Decides on goal model.
        
    }
}
