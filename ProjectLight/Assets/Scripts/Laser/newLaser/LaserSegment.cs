using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserLineSegment : MonoBehaviour
{
    
    private LineRenderer lineRenderer;
    // private Material lineMaterial;

    void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();
        // lineRenderer.material = lineMaterial;
    }

    //  public void DrawLineSegment(Vector2 startPoint, Vector2 endPoint, Material material)
    // {
    //     lineRenderer.material = material;
    //     lineRenderer.positionCount = 2;
    //     lineRenderer.SetPosition(0, startPoint);
    //     lineRenderer.SetPosition(1, endPoint);
    // }

    public void DrawLineSegment(Vector2 startPoint, Vector2 endPoint)
    {   
        if(lineRenderer != null)
        {
            lineRenderer.positionCount = 2;
            lineRenderer.SetPosition(0, startPoint);
            lineRenderer.SetPosition(1, endPoint);
        }
        else
        {
            Debug.Log("LineRender is null");
        }
       
    }
}
