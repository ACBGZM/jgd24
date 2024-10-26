using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserLineSegment : MonoBehaviour
{
    
    public LineRenderer lineRenderer;
    public Material lineMaterial;

    void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.material = lineMaterial;
    }

     public void DrawLineSegment(Vector2 startPoint, Vector2 endPoint, Material material)
    {
        lineRenderer.material = material;
        lineRenderer.positionCount = 2;
        lineRenderer.SetPosition(0, startPoint);
        lineRenderer.SetPosition(1, endPoint);
    }

    public void DrawLineSegment(Vector2 startPoint, Vector2 endPoint)
    {
        lineRenderer.positionCount = 2;
        lineRenderer.SetPosition(0, startPoint);
        lineRenderer.SetPosition(1, endPoint);
    }
}
