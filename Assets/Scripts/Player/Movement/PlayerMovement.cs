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

    private Coroutine movementRoutine;

    public event Action OnMovementStart, OnMovementFinish, OnBonked;

    public void SetInstantLocation(Vector2Int position)
    {
        ChangeTile(position);
    }

    public Coroutine Move(int steps)
    {
        movementRoutine = StartCoroutine(MoveSteps(steps));
        return movementRoutine;
    }

    public void Rotate(Direction direction)
    {
        movementDirection = direction;
        // TODO Show player rotating visually
    }

    private IEnumerator MoveSteps(int steps)
    {
        OnMovementStart?.Invoke();

        while (steps > 0)
        {
            var nextPosition = currentPosition.Position + GetMovement();
            ChangeTile(nextPosition);
            steps--;
            yield return new WaitForSeconds(movementTime);
        }
        FinishMovement();
        OnMovementFinish?.Invoke();
    }

    public void FinishMovement()
    {
        if (movementRoutine != null) StopCoroutine(movementRoutine);
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

    private void ChangeTile(Vector2Int position)
    {
        var tile = grid.GetTileByPosition(position);
        if(tile != null) ChangeTile(tile);
    }

    private void ChangeTile(GridTile tile)
    {
        if (tile.Blocked)
        {
            OnBonked?.Invoke();
            FinishMovement();
            return;
        }
        if (currentPosition != null) currentPosition.Blocked = false;

        transform.position = new Vector3(tile.transform.position.x, tile.transform.position.y + tile.transform.localScale.y, tile.transform.position.z);
        tile.Blocked = true;
        currentPosition = tile;
    }
}
