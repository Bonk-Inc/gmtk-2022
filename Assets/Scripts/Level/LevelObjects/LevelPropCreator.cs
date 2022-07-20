using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class LevelPropCreator : MonoBehaviour
{
    [SerializeField, Header("Prop Prefabs")]
    private GameObject standingLampPrefab;
    [SerializeField]
    private GameObject shortLampPrefab, armChairPrefab, bigSofaPrefab, bookshelfPrefab, bookPilePrefab, carpetPrefab, tvPrefab, tablePrefab, longTablePrefab, lampTablePrefab;

    private const string settingDivider = "-";
    private const string blockedKey = "F";

    public void SetProp(GridTile tile, string setting)
    {
        var settings = setting.Split(settingDivider);

        settings[0] = settings[0].Trim();
        var prop = InstantiateProp(tile, settings[0]);

        settings[1] = settings[1].Trim();
        RotateProp(prop, settings[1]);

        settings[2] = settings[2].Trim();
        tile.Blocked = settings[2].Equals(blockedKey);
    }

    private GameObject InstantiateProp(GridTile tile, string setting)
    {
        // TODO Enum & dictionary?
        var prefab = setting switch
        {
            "LAMP1" => standingLampPrefab,
            "LAMP2" => shortLampPrefab,
            "CHAIR1" => armChairPrefab,
            "SOFA1" => bigSofaPrefab,
            "SHELF1" => bookshelfPrefab,
            "BOOKS1" => bookPilePrefab,
            "CARPET1" => carpetPrefab,
            "TV1" => tvPrefab,
            "TABLE1" => tablePrefab,
            "TABLE2" => longTablePrefab,
            _ => lampTablePrefab
        };
        var prop = Instantiate(prefab, tile.transform);
        prop.transform.localPosition = Vector3.zero;
        return prop;
    }

    private void RotateProp(GameObject prop, string setting)
    {
        var rotation = setting switch
        {
            "N" => -90f,
            "S" => 90f,
            "E" => 180f,
            _ => 0f,
        };

        Vector3 rotationVector = new Vector3(0, -rotation, 0);
        prop.transform.rotation = Quaternion.Euler(rotationVector);
    }
}
