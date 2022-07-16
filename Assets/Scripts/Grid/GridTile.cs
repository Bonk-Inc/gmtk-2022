using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridTile : MonoBehaviour
{
    [SerializeField] // Should be read only
    private Vector2 position;

    [SerializeField]
    private bool blocked = false;

    public Vector2 Position { get => position; }
    public bool Blocked { get => blocked; set => blocked = value; }

    public GridTile(Vector2 pos)
    {
        SetPosition(pos);
    }

    public void SetPosition(Vector2 pos)
    {
        position = pos;
    }
}
