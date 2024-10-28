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

    public float lineWidth = 1.0f;

    // public Material lineMaterial;
    public List<Vector2> lines;
    // private int pointCount = 0;

    void Start()
    {   
        // lines = new 
        //   = 
        line_1 = Instantiate(lineSegmentPrefab,Vector3.zero,Quaternion.identity,this.transform); 
        line_2 = Instantiate(lineSegmentPrefab,Vector3.zero,Quaternion.identity,this.transform); 

    }
    // public void DrawLines(List<Vector2> keyPoints,Material material,bool laserStatus,float lineWidth)
    public void DrawLines(List<Vector2> keyPoints,bool laserStatus,float lineWidth)
    {      
        // // Draw Segment

        if(keyPoints.Count == 2)
        {   
            line_1.GetComponent<LaserLineSegment>().DrawLineSegment(keyPoints[0],keyPoints[1]);
            line_2.GetComponent<LineRenderer>().enabled = false;
        }
        else if(keyPoints.Count == 3)
        {   
            line_1.GetComponent<LaserLineSegment>().DrawLineSegment(keyPoints[0],keyPoints[1]);
            line_2.GetComponent<LineRenderer>().enabled = true;
            line_2.GetComponent<LaserLineSegment>().DrawLineSegment(keyPoints[1],keyPoints[2]);
        }


        // // Draw VFX

        // if(keyPoints.Count == 3)
        // {
        //     // 画中点
        // }




        // Debug.Log("激光关键点");
        // foreach (var k in keyPoints)
        // {
        //      Debug.Log(k);
        // }
        // Debug.Log("---------");
       
        

    }

    // public void DrawLineSegment(Vector2 startPoint, Vector2 endPoint, Material material)
    
}
