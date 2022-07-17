using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField, Header("Dependencies")]
    private GridManager grid;

    [SerializeField, Header("Movement Settings")]
    private float movementTime = 0.5f;

    [SerializeField, Header("Game Information - Read Only!")]
    private GridTile currentPosition;

    [SerializeField]
    private Direction movementDirection = Direction.North;

    public event Action OnMovementStart, OnMovementFinish, OnBonked;

    private void Awake() {
        Rotate(Direction.North);
    }

    public void SetInstantLocation(Vector2Int position)
    {
        ChangeTile(position);
    }

    public Coroutine Move(int steps)
    {
        return StartCoroutine(MoveSteps(steps));
    }

    public void Rotate(Direction direction)
    {
        movementDirection = direction;
        // TODO Show player rotating visually
    }

    private IEnumerator MoveSteps(int steps)
    {
        OnMovementStart?.Invoke();

        Vector2Int nextPosition = currentPosition.Position + GetMovement();
        while (steps > 0 && ChangeTile(nextPosition))
        {
            nextPosition = currentPosition.Position + GetMovement();
            steps--;
            yield return new WaitForSeconds(movementTime);
        }
        OnMovementFinish?.Invoke();
    }


    private Vector2Int GetMovement()
    {
        return movementDirection switch
        {
            Direction.North => Vector2Int.up,
            Direction.East => Vector2Int.right,
            Direction.South => Vector2Int.down,
            Direction.West => Vector2Int.left,
            _ => Vector2Int.zero,
        };
    }

    private bool ChangeTile(Vector2Int position)
    {
        var tile = grid.GetTileByPosition(position);
        if(tile != null) return ChangeTile(tile);
        return false;
    }

    private bool ChangeTile(GridTile tile)
    {
        if (tile.Blocked)
        {
            OnBonked?.Invoke();
            return false;
        }
        if (currentPosition != null) currentPosition.Blocked = false;

        transform.position = new Vector3(tile.transform.position.x, tile.transform.position.y + tile.transform.localScale.y, tile.transform.position.z);
        tile.Blocked = true;
        currentPosition = tile;
        return true;
    }
}
