using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridManager : MonoBehaviour
{
    [SerializeField, Header("Dependencies")]
    private GridGenerator generator;

    [SerializeField, Header("Grid Settings")]
    private bool generateGrid = false;

    private GridRow[] grid;

    private void Awake()
    {
        grid = generateGrid ? generator.CreateGrid() : generator.Rows;
    }

    public GridTile GetTileByPosition(Vector2Int position)
    {
        return PositionIsValid(position) ? grid[position.x].Tiles[position.y] : null;
    }

    private bool PositionIsValid(Vector2Int position)
    {
        return (position.x >= 0 && position.x < grid.Length && position.y >= 0 && position.y < grid[position.x].Tiles.Length);
    }

}
