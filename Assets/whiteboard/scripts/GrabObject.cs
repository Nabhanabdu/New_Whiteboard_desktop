using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrabObject : MonoBehaviour
{
    public Transform grabPoint;
    public float grabRange = 2f;
    public float mouseMovementSensitivity = 1f;
    public float movementRangeX = 5f; // Define the movement range on the X axis
    public float movementRangeY = 3f; // Define the movement range on the Y axis

    private GameObject heldObject;
    private bool isHoldingMarker = false;
    private Vector3 initialMarkerRotation; // Added a new variable to store the initial marker rotation

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (heldObject == null)
            {
                RaycastHit hit;
                if (Physics.Raycast(transform.position, transform.forward, out hit, grabRange))
                {
                    if (hit.transform.gameObject.CompareTag("Marker"))
                    {
                        heldObject = hit.transform.gameObject;
                        heldObject.transform.SetParent(grabPoint);
                        heldObject.transform.localPosition = Vector3.zero;
                        initialMarkerRotation = heldObject.transform.localRotation.eulerAngles; // Store the initial marker rotation
                        heldObject.transform.localRotation = Quaternion.Euler(initialMarkerRotation.x, 90f, initialMarkerRotation.z); // Rotate the marker horizontally
                        heldObject.GetComponent<Rigidbody>().isKinematic = true;
                        heldObject.GetComponent<MarkerController>().enabled = false;
                        isHoldingMarker = true;
                    }
                }
            }
            else
            {
                ReleaseObject();
            }
        }

        if (isHoldingMarker)
        {
            float mouseX = Input.GetAxis("Mouse X") * mouseMovementSensitivity;
            float mouseY = Input.GetAxis("Mouse Y") * mouseMovementSensitivity;

            Vector3 newPosition = grabPoint.position + new Vector3(mouseX, mouseY, 0);

            // Clamp the position to stay within the movement range
            newPosition = new Vector3(
                Mathf.Clamp(newPosition.x, grabPoint.position.x - movementRangeX, grabPoint.position.x + movementRangeX),
                Mathf.Clamp(newPosition.y, grabPoint.position.y - movementRangeY, grabPoint.position.y + movementRangeY),
                newPosition.z
            );

            heldObject.transform.localPosition = newPosition - grabPoint.position;

            heldObject.transform.Rotate(Vector3.forward, -mouseY, Space.Self); // Rotate around the forward axis for horizontal rotation
            heldObject.transform.Rotate(Vector3.up, mouseX, Space.Self); // Rotate around the up axis for vertical rotation
        }
    }

    private void ReleaseObject()
    {
        heldObject.transform.SetParent(null);
        heldObject.GetComponent<Rigidbody>().isKinematic = false;
        heldObject.GetComponent<MarkerController>().enabled = true;
        heldObject.transform.rotation = Quaternion.Euler(initialMarkerRotation); // Reset the marker rotation to its initial rotation
        heldObject = null;
        isHoldingMarker = false;
    }
}
