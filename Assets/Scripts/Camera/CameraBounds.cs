using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraBounds : MonoBehaviour
{
    private bool first = true;
    private Bounds levelbounds = new Bounds();

    [SerializeField]
    private float xMaxMargin = 0, xMinMargin, zMaxMargin = 3, zMinMargin;

    public void OnTileAdded(GridTile tile){
        if (first)
        {
            first = false;
            levelbounds = tile.Visual.MeshRenderer.bounds;
        } else {
            levelbounds.Encapsulate(tile.Visual.MeshRenderer.bounds);
        }
    }

    private void LateUpdate() {

        var position = transform.position;
        position.x = Mathf.Clamp(position.x, levelbounds.min.x - xMinMargin, levelbounds.max.x + xMaxMargin);
        position.z = Mathf.Clamp(position.z, levelbounds.min.z - zMinMargin, levelbounds.max.z + zMaxMargin);
        transform.position = position;
    }

}
