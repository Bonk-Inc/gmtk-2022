using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridManager : MonoBehaviour
{
    [SerializeField, Header("Dependencies")]
    private GridGenerator generator;
    [SerializeField]
    private LevelCreator creator;

    [SerializeField, Header("Grid Settings")]
    private bool generateGrid = false;
    [SerializeField]
    private bool createFromLevel = false;

    [SerializeField]
    private string path = "Levels";
    [SerializeField]
    private string level = "level-1";

    private GridRow[] grid;

    private void Awake()
    {
        GetGrid();
    }

    private void GetGrid()
    {
        if (createFromLevel)
        {
            grid = creator.LoadLevel(path, level);
            return;
        }

        if (generateGrid)
        {
            grid = generator.CreateGrid();
            return;
        }
        grid = generator.Rows;
    }

    public GridTile GetTileByPosition(Vector2Int position)
    {
        return PositionIsValid(position) ? grid[position.x].Tiles[position.y] : null;
    }

    public bool PositionIsValid(Vector2Int position)
    {
        return position.x >= 0 && position.x < grid.Length && position.y >= 0 && position.y < grid[position.x].Tiles.Length;
    }

}
