using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TempTowerDragging : MonoBehaviour
{
    public Transform center;

    [SerializeField]
    private float radius = 5f;

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

    private void Start()
    {
        center = GameObject.Find("Base Tower").transform;
    }

    private void Update()
    {
        if (isDragging)
        {
            Vector3 movPosition = getMousePoint();
            Vector3 checkCirclePosiion = movPosition - center.position;
            Vector3 newPosition = center.position + checkCirclePosiion.normalized * radius;
            newPosition.y = transform.position.y;

            transform.position = newPosition;
        }
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
