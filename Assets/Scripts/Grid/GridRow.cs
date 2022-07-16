using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class GridRow
{
    [SerializeField]
    private GridTile[] tiles;

    public GridTile[] Tiles { get => tiles; set => tiles = value; }
}
