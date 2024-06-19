using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MarkerController : MonoBehaviour
{
    public float writingDistance = 0.1f;
    public Color markerColor = Color.black;
    public float markerWidth = 0.1f;

    private bool isWriting = false;
    private LineRenderer lineRenderer;
    private Vector3 lastPosition;

    private void Start()
    {
        lineRenderer = gameObject.AddComponent<LineRenderer>();
        lineRenderer.material = new Material(Shader.Find("Sprites/Default"));
        lineRenderer.startColor = markerColor;
        lineRenderer.endColor = markerColor;
        lineRenderer.startWidth = markerWidth;
        lineRenderer.endWidth = markerWidth;
        lineRenderer.positionCount = 0;
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0) && !Input.GetMouseButton(1)) // Add a check for the right mouse button
        {
            StartWriting();
        }
        else if (Input.GetMouseButtonUp(0) || Input.GetMouseButton(1)) // Add a check for the right mouse button
        {
            StopWriting();
        }

        if (isWriting)
        {
            RaycastHit hit;
            if (Physics.Raycast(transform.position, transform.forward, out hit, writingDistance))
            {
                if (hit.transform.CompareTag("Whiteboard"))
                {
                    Vector3 position = hit.point;
                    if (position != lastPosition)
                    {
                        lineRenderer.positionCount++;
                        lineRenderer.SetPosition(lineRenderer.positionCount - 1, position);
                        lastPosition = position;
                    }
                }
            }
        }
    }

    private void StartWriting()
    {
        isWriting = true;
        lastPosition = transform.position;
        lineRenderer.positionCount = 0;
    }

    private void StopWriting()
    {
        isWriting = false;
    }
}