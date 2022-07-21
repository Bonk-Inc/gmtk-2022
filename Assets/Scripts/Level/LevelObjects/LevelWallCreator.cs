using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelWallCreator : MonoBehaviour
{
    [SerializeField, Header("Wall Prefabs")]
    private GameObject wallPrefab;
    [SerializeField]
    private GameObject wallCornerPrefab, wallNoSupportPrefab, wallNoSupportCornerPrefab;

    public void SetWall(GridTile tile, string setting)
    {
        var settings = setting.Split("-");
        tile.Blocked = true;

        settings[0] = settings[0].Trim();
        var wall = CreateWall(tile, settings[0]);


        settings[1] = settings[1].Trim();
        RotateWall(wall, settings[1]);
    }

    private GameObject CreateWall(GridTile tile, string setting)
    {
        var prefab = setting switch
        {
            "1" => wallPrefab,
            "2" => wallCornerPrefab,
            "3" => wallNoSupportPrefab,
            _ => wallNoSupportCornerPrefab
        };
        var wall = Instantiate(prefab, tile.transform);
        wall.transform.localPosition = Vector3.zero;
        return wall;
    }

    private void RotateWall(GameObject wall, string setting)
    {
        var rotation = setting switch
        {
            "N" => -90f,
            "S" => 90f,
            "E" => 180f,
            _ => 0f,
        };

        Vector3 rotationVector = new Vector3(0, -rotation, 0);
        wall.transform.rotation = Quaternion.Euler(rotationVector);
    }
}
