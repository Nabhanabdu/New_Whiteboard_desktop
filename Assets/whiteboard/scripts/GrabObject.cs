using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrabObject : MonoBehaviour
{
    public Transform grabPoint;
    public float grabRange = 2f;
    public float grabForce = 150f;

    private GameObject heldObject;
    private Rigidbody heldObjectRigidbody;

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
                        heldObjectRigidbody = heldObject.GetComponent<Rigidbody>();
                        heldObjectRigidbody.isKinematic = true;
                        heldObject.transform.SetParent(grabPoint);
                        heldObject.transform.localPosition = Vector3.zero;
                        heldObject.transform.localRotation = Quaternion.identity;
                    }
                }
            }
            else
            {
                ReleaseObject();
            }
        }
    }

    private void ReleaseObject()
    {
        heldObject.transform.SetParent(null);
        heldObjectRigidbody.isKinematic = false;
        heldObjectRigidbody.AddForce(transform.forward * grabForce);
        heldObject = null;
        heldObjectRigidbody = null;
    }
}
