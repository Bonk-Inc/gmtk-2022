using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.Events;

[System.Serializable]
public class TileCreated: UnityEvent<GridTile>{}

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
    
    [SerializeField]
    public TileCreated OnTileCreated;


    private Transform tileHolder;

    public GridRow[] Rows { get => rows; }

    [ExecuteInEditMode, ContextMenu("Create Grid")]
    public GridRow[] CreateGrid()
    {
        return CreateGrid(x, z);
    }

    public GridRow[] CreateGrid(int xSize, int zSize)
    {
        if (transform.childCount == 0) CreateHolder();
        if (tileHolder == null || tileHolder.childCount > 0) ResetGrid();

        rows = new GridRow[xSize];

        for (int i = 0; i < xSize; i++)
        {
            rows[i] = new GridRow
            {
                Tiles = new GridTile[zSize]
            };

            for (int j = 0; j < zSize; j++)
            {
                CreateTile(i, j);
            }
        }
#if UNITY_EDITOR
        if (!Application.isPlaying) EditorUtility.SetDirty(gameObject);
#endif
        print("Grid Created.");
        return rows;
    }

    private void CreateTile(int xPos, int zPos)
    {
        var tile = Instantiate(tilePreset, tileHolder);

        tile.transform.position = new Vector3(xPos * tileSize, 0, zPos * tileSize);
        tile.name = "Tile - " + (xPos+1) + " " + (zPos+1);
        tile.SetPosition(new Vector2Int(xPos, zPos));
        
        rows[xPos].Tiles[zPos] = tile;

        OnTileCreated?.Invoke(tile);
    }

    [ExecuteInEditMode, ContextMenu("Reset Grid")]
    private void ResetGrid()
    {
        if (transform.childCount != 0)
        {
            DestroyImmediate(transform.GetChild(0).gameObject);
            tileHolder = null;
        }
        CreateHolder();
    }

    private void CreateHolder()
    {
        var holder = Instantiate(holderPreset, gameObject.transform);
        holder.localPosition = Vector3.zero;

        tileHolder = holder;
    }
}
