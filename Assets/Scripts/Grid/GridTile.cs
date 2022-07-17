using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridTile : MonoBehaviour
{
    [SerializeField]
    private GridTileVisual visual;
    [SerializeField] // Should be read only
    private Vector2Int position;

    [SerializeField]
    private bool blocked = false;

    public Vector2Int Position { get => position; }
    public bool Blocked { get => blocked; set => blocked = value; }
    public GridTileVisual Visual { get => visual; }

    public void SetPosition(Vector2Int pos)
    {
        position = pos;
    }
}
