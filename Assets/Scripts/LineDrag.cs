using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class LineDrag : MonoBehaviour
{
    public Collider collider;

    private Vector3 mouseHitPoint;
    private Vector3 distanceOffset;

    [SerializeField]
    private bool isDragging = false;

    private void OnMouseDown()
    {
        isDragging = true;
    }

    private void OnMouseUp()
    {
        isDragging = false;
    }

    private void Awake()
    {
        collider = GameObject.Find("Line").GetComponent<Collider>();
    }

    private void Update()
    {
        if (isDragging)
        {
            Vector3 movPosition = getMousePoint();
            movPosition.y = transform.position.y;

            transform.position = ClampPointInBound(getMousePoint(), collider.bounds);
        }
    }

    Vector3 ClampPointInBound(Vector3 position, Bounds bounds)
    {
        Vector3 clampedPosition = Vector3.zero;

        float clampedX = Mathf.Clamp(position.x, bounds.min.x, bounds.max.x);
        float clampedZ = Mathf.Clamp(position.z, bounds.min.z, bounds.max.z);

        clampedPosition = new Vector3(clampedX, position.y, clampedZ);

        return clampedPosition;
    }

    Vector3 getMousePoint()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        Plane groundPlane = new Plane(Vector3.up, Vector3.zero); // Bề mặt ngang với Vector3.up là hướng pháp tuyến

        float rayDistance;
        if (groundPlane.Raycast(ray, out rayDistance))
        {
            Vector3 hitPoint = ray.GetPoint(rayDistance);
            return hitPoint;
        }

        return Vector3.zero;
    }
}
