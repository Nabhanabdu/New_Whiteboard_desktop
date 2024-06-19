using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public float mouseSensitivity = 100f;
    public Transform playerBody;

    private float verticalRotation = 0f;
    private bool isRotating = false;

    private void Update()
    {
        if (Input.GetMouseButtonDown(2))
        {
            isRotating = true;
            Cursor.lockState = CursorLockMode.Locked;
        }
        else if (Input.GetMouseButtonUp(2))
        {
            isRotating = false;
            Cursor.lockState = CursorLockMode.None;
        }

        if (isRotating)
        {
            float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
            float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

            verticalRotation -= mouseY;
            verticalRotation = Mathf.Clamp(verticalRotation, -90f, 90f);

            transform.localRotation = Quaternion.Euler(verticalRotation, 0f, 0f);
            playerBody.Rotate(Vector3.up * mouseX);
        }
    }
}