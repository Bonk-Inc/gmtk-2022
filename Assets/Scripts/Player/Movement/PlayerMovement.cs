using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField, Header("Dependencies")]
    private GridManager grid;

    [SerializeField, Header("Movement Settings")]
    private float movementTime = 0.5f, rotationTime = 0.5f;

    [SerializeField, Header("Game Information - Read Only!")]
    private GridTile currentPosition;

    [SerializeField]
    private Direction movementDirection = Direction.North;

    [SerializeField]
    private float stepHeight = 1;

    public event Action<GridTile> OnChangedPosition;

    public event Action OnMovementStart, OnMovementFinish, OnBonked;

    public void SetInstantLocation(Vector2Int position)
    {
        ChangeTileInstant(position);
    }

    public Coroutine Move(int steps)
    {
        return StartCoroutine(MoveSteps(steps));
    }

    public void RotateInstant(Direction direction)
    {
        movementDirection = direction;
        transform.forward = GetDirection();
    }

    public Coroutine Rotate(Direction direction)
    {
        var prevDirection = GetMovement();
        movementDirection = direction;
        var directionMove = GetMovement();
        return StartCoroutine(RotateAnimation((Vector2)directionMove, (Vector2)prevDirection));
    }

    private IEnumerator RotateAnimation(Vector2 end, Vector2 start)
    {
        var angle =  Vector2.SignedAngle(end, start);
        var initialAngle = transform.rotation.eulerAngles.y;
        var t = 0f;
        while(t < 1){
            var rotation = transform.rotation.eulerAngles;
            rotation.y = initialAngle + Mathf.Lerp(0, angle, t);
            transform.rotation = Quaternion.Euler(rotation);
            t += Time.deltaTime / rotationTime;
            yield return null;
        }
        RotateInstant(movementDirection);
    }

    private IEnumerator MoveSteps(int steps)
    {
        OnMovementStart?.Invoke();

        Vector2Int nextPosition = currentPosition.Position + GetMovement();
        Coroutine animation;
        while (steps > 0 && ChangeTile(nextPosition, out animation))
        {
            nextPosition = currentPosition.Position + GetMovement();
            steps--;
            yield return animation;
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

    private Vector3 GetDirection()
    {
        return movementDirection switch
        {
            Direction.North => Vector3.forward,
            Direction.East => Vector3.right,
            Direction.South => Vector3.back,
            Direction.West => Vector3.left,
            _ => Vector3.forward,
        };
    }

    private bool ChangeTileInstant(Vector2Int position)
    {
        var tile = grid.GetTileByPosition(position);
        if (tile != null) return ChangeTileInstant(tile);

        return false;
    }

    private bool ChangeTileInstant(GridTile tile)
    {
        if (tile.Blocked)
        {
            OnBonked?.Invoke();
            return false;
        }
        if (currentPosition != null) currentPosition.Blocked = false;

        transform.position = new Vector3(tile.transform.position.x, tile.transform.position.y + tile.transform.localScale.y, tile.transform.position.z);
        OnChangedPosition?.Invoke(tile);
        tile.Blocked = true;
        currentPosition = tile;
        return true;
    }

    private bool ChangeTile(Vector2Int position, out Coroutine moveAnimation)
    {
        var tile = grid.GetTileByPosition(position);
        if (tile != null) return ChangeTile(tile, out moveAnimation);
        moveAnimation = null;
        return false;
    }

    private bool ChangeTile(GridTile tile, out Coroutine moveAnimation)
    {
        moveAnimation = null;
        if (tile.Blocked)
        {
            OnBonked?.Invoke();
            return false;
        }
        if (currentPosition != null) currentPosition.Blocked = false;

        moveAnimation = StartCoroutine(MoveAnimation(
            transform.position, 
            new Vector3(tile.transform.position.x, tile.transform.position.y + tile.transform.localScale.y, tile.transform.position.z), 
            () => OnChangedPosition?.Invoke(tile)
        ));
        tile.Blocked = true;
        currentPosition = tile;
        return true;
    }






    private IEnumerator MoveAnimation(Vector3 startPoint, Vector3 endPoint, Action completed)
    {
        float t = 0;
        float cdStart = startPoint.x;
        float csEnd = endPoint.x;
        if (Math.Abs(startPoint.x - endPoint.x) > Math.Abs(startPoint.z - endPoint.z))
        {
            cdStart = startPoint.x;
            csEnd = endPoint.x;

        }
        else
        {
            cdStart = startPoint.z;
            csEnd = endPoint.z;

        }

        Vector2 p0 = new Vector2(Mathf.Lerp(cdStart, csEnd, 0), startPoint.y);
        Vector2 p1 = new Vector2(Mathf.Lerp(cdStart, csEnd, 0.33f), startPoint.y + stepHeight);
        Vector2 p2 = new Vector2(Mathf.Lerp(cdStart, csEnd, 0.66f), endPoint.y + stepHeight);
        Vector2 p3 = new Vector2(Mathf.Lerp(cdStart, csEnd, 1), endPoint.y);
        while (t < 1)
        {
            var point = GetBrezierPoint(t, p0, p1, p2, p3);
            if (Math.Abs(startPoint.x - endPoint.x) > Math.Abs(startPoint.z - endPoint.z))
            {
                transform.position = new Vector3(point.x, point.y, transform.position.z);
            }
            else
            {
                transform.position = new Vector3(transform.position.x, point.y, point.x);
            }
            t += Time.deltaTime / movementTime;
            yield return null;
        }
        completed?.Invoke();
        transform.position = endPoint;
    }

    private Vector2 GetBrezierPoint(float t, Vector2 p0, Vector2 p1, Vector2 p2, Vector2 p3)
    {
        float cx = 3 * (p1.x - p0.x);
        float cy = 3 * (p1.y - p0.y);
        float bx = 3 * (p2.x - p1.x) - cx;
        float by = 3 * (p2.y - p1.y) - cy;
        float ax = p3.x - p0.x - cx - bx;
        float ay = p3.y - p0.y - cy - by;
        float Cube = t * t * t;
        float Square = t * t;

        float resX = (ax * Cube) + (bx * Square) + (cx * t) + p0.x;
        float resY = (ay * Cube) + (by * Square) + (cy * t) + p0.y;

        return new Vector2(resX, resY);
    }
}
