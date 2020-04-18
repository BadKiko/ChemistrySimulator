using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateFlask : MonoBehaviour
{
    [SerializeField] private float RotateSpeed;
    private bool isDragging;
    private Rigidbody flaskRigidbody;


    private void Start()
    {
        flaskRigidbody = GetComponent<Rigidbody>();
    }
    private void Update()
    {
        if (Input.GetMouseButton(1))
        {

            float rotX = Input.GetAxis("Mouse X") * RotateSpeed * Mathf.Deg2Rad;
            float rotY = Input.GetAxis("Mouse Y") * RotateSpeed * Mathf.Deg2Rad;

            transform.RotateAround(Vector3.up, -rotX);
            transform.RotateAround(Vector3.right, rotY);
        }
    }
}
