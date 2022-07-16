using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Grid : MonoBehaviour
{
    [SerializeField, Header("Presets")]
    private GridTile tilePreset;
    [SerializeField]
    private Transform holderPreset;

    [SerializeField, Header("Grid Variables")]
    private int x;

    [SerializeField]
    private int z;
    [SerializeField]
    private float tileSize = 1;

    [SerializeField, Header("Grid Tiles")]
    private GridTile[,] tiles;

    private Transform tileHolder;

    [ExecuteInEditMode, ContextMenu("Create Grid")]
    private void CreateGrid()
    {
        if (tileHolder == null) CreateHolder();
        tiles = new GridTile[x, z];

        for (int i = 0; i < x; i++)
        {
            for (int j = 0; j < z; j++)
            {
                CreateTile(i, j);
            }
        }
    }

    private void CreateTile(int xPos, int zPos)
    {
        var tile = Instantiate(tilePreset, tileHolder);
        tile.transform.position = new Vector3(xPos * tileSize, 0, zPos * tileSize);
        tiles[xPos, zPos] = tile;
    }

    [ExecuteInEditMode, ContextMenu("Reset Grid")]
    private void ResetGrid()
    {
        if(tileHolder != null) DestroyImmediate(tileHolder.gameObject);
        CreateHolder();
    }

    private void CreateHolder()
    {
        var holder = Instantiate(holderPreset, gameObject.transform);
        holder.localPosition = Vector3.zero;

        tileHolder = holder;
    }
}
