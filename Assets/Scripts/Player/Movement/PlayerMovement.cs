using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField, Header("Dependencies")]
    private GridManager grid;

    [SerializeField, Header("Game Information - Read Only!")]
    private GridTile currentPosition;

    [SerializeField]
    private Direction movementDirection = Direction.North;

    public event Action OnMovementFinish;

    public void SetInstantLocation(Vector2Int position)
    {
        ChangeTile(position);
    }

    public void Move(int steps)
    {
        StartCoroutine(MoveSteps(steps));
    }

    private IEnumerator MoveSteps(int steps)
    {
        
        yield return new WaitForSeconds(1f);
    }

    private void ChangeTile(Vector2Int position)
    {
        var tile = grid.GetTileByPosition(position);
        ChangeTile(tile);
    }

    private void ChangeTile(GridTile tile)
    {
        if (currentPosition != null) currentPosition.Blocked = false;

        transform.position = new Vector3(tile.transform.position.x, tile.transform.position.y + tile.transform.localScale.y, tile.transform.position.z);
        tile.Blocked = true;
        currentPosition = tile;
    }
}
