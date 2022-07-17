using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DieSide : MonoBehaviour
{

    [SerializeField]
    private int value;

    [SerializeField]
    private Transform display;

    public Vector3 Up => transform.up;
    public Transform Display => display.transform;

    public int Value => value;

}
