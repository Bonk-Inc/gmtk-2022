using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelPlayerCreator : MonoBehaviour
{
    [SerializeField]
    private Transform playerObject;
    [SerializeField, Header("Settings")]
    private StartPosition startPosition;
    [SerializeField, Header("Player Prefabs")]
    private GameObject kidJustinPrefab;
    [SerializeField]
    private GameObject guitaristDjoestPrefab;

    public void SetPlayer(GridTile tile, string setting)
    {
        startPosition.SetPosition(tile.Position, true);
        var settings = setting.Split("-");

        settings[0] = settings[0].Trim();
        CreatePlayerModel(settings[0]);

        settings[1] = settings[1].Trim();
        RotatePlayer(settings[1]);
    }

    private void CreatePlayerModel(string setting)
    {
        // TODO Enum & dictionary?
        var prefab = setting switch
        {
            "1" => kidJustinPrefab,
            _ => guitaristDjoestPrefab,
        };
        var player = Instantiate(prefab, playerObject);
        player.transform.localPosition = Vector3.zero;
    }

    private void RotatePlayer(string setting)
    {
        startPosition.MoveDirection = setting switch
        {
            "W" => Direction.West,
            "S" => Direction.South,
            "E" => Direction.East,
            _ => Direction.North,
        };
    }
}
