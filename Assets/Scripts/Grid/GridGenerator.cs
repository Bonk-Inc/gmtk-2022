using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GridGenerator : MonoBehaviour
{
    [SerializeField, Header("Presets")]
    private GridTile tilePreset;
    [SerializeField]
    private Transform holderPreset;

    [SerializeField, Header("Generator Settings")]
    private int x;
    [SerializeField]
    private int z;
    [SerializeField]
    private float tileSize = 1;

    [SerializeField, Header("Grid Tiles")]
    private GridRow[] rows;

    private Transform tileHolder;

    public GridRow[] Rows { get => rows; }


    [ExecuteInEditMode, ContextMenu("Create Grid")]
    public GridRow[] CreateGrid()
    {
        if (tileHolder == null) CreateHolder();
        if (tileHolder.childCount > 0) ResetGrid();

        rows = new GridRow[x];

        for (int i = 0; i < x; i++)
        {
            var row = new GridRow();
            rows[i] = row;
            print(rows[i]);
            rows[i].Tiles = new GridTile[z];

            for (int j = 0; j < z; j++)
            { 
                CreateTile(i, j);
            }
        }
        print("Grid Created." + rows );
        return rows;
    }

    private void CreateTile(int xPos, int zPos)
    {
        var tile = Instantiate(tilePreset, tileHolder);
        tile.transform.position = new Vector3(xPos * tileSize, 0, zPos * tileSize);
        tile.name = "Tile - " + (xPos+1) + " " + (zPos+1);
        rows[xPos].Tiles[zPos] = tile;
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
