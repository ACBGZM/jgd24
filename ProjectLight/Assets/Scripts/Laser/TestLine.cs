using System.Collections.Generic;
using System.Drawing.Text;
using UnityEngine;

public class TestLine : MonoBehaviour
{
    private LineRenderer lineRenderer;
    public GameObject LinePrefab;

    private int pointCount;
    private Vector2 startPoint;
    private Vector3 EndPoint;
    public float lineWidth = 0.1f;
    public List<Material> materialList;

    private void Awake()
    {
        lineRenderer = Instantiate(LinePrefab).GetComponent<LineRenderer>();
        lineRenderer.startWidth = lineWidth;
        lineRenderer.endWidth = lineWidth;
        lineRenderer.material = materialList[0];
    }

    void Start()
    {
        startPoint = transform.position;
        EndPoint = transform.position;
    }

    private void Update()
    {
        DrawLine(startPoint, EndPoint);
    }

    private void FixedUpdate()
    {
        EndPoint.x += 5f * Time.deltaTime;
    }

    void DrawLine(Vector2 startPosition, Vector2 endPosition)
    {
        pointCount = 1;
        lineRenderer.positionCount = pointCount;
        lineRenderer.SetPosition(0, startPosition);

        lineRenderer.positionCount += 1;
        lineRenderer.SetPosition(pointCount++, endPosition);
    }
}