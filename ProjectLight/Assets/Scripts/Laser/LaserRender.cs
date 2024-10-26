using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserRender : MonoBehaviour
{
    public GameObject lineSegmentPrefab;
    public GameObject startVFX;
    public GameObject midVFX;
    public GameObject endVFX;
    public GameObject hitVFX;

    // lines
    private GameObject line_1;
    private GameObject line_2;


    public Material lineMaterial;
    public List<Vector2> lines;
    // private int pointCount = 0;

    void Start()
    {   
        // lines = new 
        //   = 
        line_1 = Instantiate(lineSegmentPrefab,Vector3.zero,Quaternion.identity,this.transform); 
        line_2 = Instantiate(lineSegmentPrefab,Vector3.zero,Quaternion.identity,this.transform); 

    }
    public void DrawLines(List<Vector2> keyPoints,Material material,bool laserStatus,float lineWidth)
    {      
        // Draw Segment
        line_1.GetComponent<LaserLineSegment>().DrawLineSegment(keyPoints[0],keyPoints[1]);
        if(keyPoints.Count == 2)
        {
            line_2.GetComponent<LineRenderer>().enabled = false;
        }
        else
        {
            line_1.GetComponent<LaserLineSegment>().DrawLineSegment(keyPoints[1],keyPoints[2]);
        }


        // Draw VFX

        if(keyPoints.Count == 3)
        {
            // 画中点
        }
        

    }

    // public void DrawLineSegment(Vector2 startPoint, Vector2 endPoint, Material material)
    
}
