using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartPosition : MonoBehaviour
{
    [SerializeField]
    private GridManager manager;

    [SerializeField, Header("Settings")]
    private Vector2Int position;
    [SerializeField]
    private Direction moveDirection;
    public Vector2Int Position { get => position; }
    public Direction MoveDirection { get => moveDirection; set => moveDirection = value; }

    public void SetPosition(Vector2Int newPosition, bool skipCheck = false)
    {
        if (skipCheck || manager.PositionIsValid(newPosition)) position = newPosition;
    }
}
