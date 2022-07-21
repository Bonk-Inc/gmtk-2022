using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridTileVisual : MonoBehaviour
{
    [SerializeField]
    private MeshRenderer meshRenderer;

    [SerializeField]
    private Texture texture;
    [SerializeField]
    private Material material;


    public MeshRenderer MeshRenderer => meshRenderer;

    public void ToggleRender(bool show = false)
    {
        meshRenderer.enabled = show;
    }

    [ContextMenu("test")]
    public void Test()
    {
        meshRenderer.material.mainTexture = texture;
    }
    [ContextMenu("test2")]
    public void Test2()
    {
        meshRenderer.material.mainTexture = texture;
    }
}
