using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFreeMovement : MonoBehaviour
{
    [SerializeField]
    private Camera movementCamera;

    [SerializeField, Header("Movement Options")]
    private float movementMargin = 0.05f;

    [SerializeField]
    private float speed = 15;

    private void Awake()
    {
        if(movementCamera == null) movementCamera = Camera.main;
    }

    private void Update()
    {
        CheckInput();
    }

    private void CheckInput()
    {
        var vertical = Input.GetAxis("Vertical");
        var horizontal = Input.GetAxis("Horizontal");

        if (Mathf.Abs(vertical) <= movementMargin && Mathf.Abs(horizontal) <= movementMargin) return;
        
        Move(horizontal, vertical);
    }

    private void Move(float horizontal, float vertical)
    {
        var horizontalSpeed = horizontal * speed * Time.deltaTime;
        var verticalSpeed = vertical * speed * Time.deltaTime;
        movementCamera.transform.position += new Vector3(horizontalSpeed, 0, verticalSpeed);
    }
}
