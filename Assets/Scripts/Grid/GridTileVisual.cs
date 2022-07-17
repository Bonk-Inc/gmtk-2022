using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridTileVisual : MonoBehaviour
{
    [SerializeField]
    private MeshRenderer meshRenderer;

    public MeshRenderer MeshRenderer => meshRenderer;

    public void ToggleRender(bool show = false)
    {
        meshRenderer.enabled = show;
    }
}
