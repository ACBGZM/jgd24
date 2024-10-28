using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserRender : MonoBehaviour
{
    public GameObject lineSegmentPrefab;
    public GameObject beamVFXPrefab;
    // public GameObject startVFXPrefab;
    // public GameObject midVFXPrefab;
    // public GameObject endVFXPrefab;
    // public GameObject hitVFXPrefab;

    // lines
    private GameObject line_1;
    private GameObject line_2;
    private GameObject startVFX;
    private GameObject midVFX;
    private GameObject endVFX;

    public float lineWidth = 1.0f;
    public Vector3 startVFXScale;
    public Vector3 midVFXScale ;
    public Vector3 endVFXScale ;

    // private Color midVFXColor;
    private ParticleSystemRenderer midVFXRenderer;

    // public Material lineMaterial;
    public List<Vector2> lines;
    // private int pointCount = 0;

    void Start()
    {   
        // lines = new 
        //   = 
        line_1 = Instantiate(lineSegmentPrefab,Vector3.zero,Quaternion.identity,this.transform); 
        line_2 = Instantiate(lineSegmentPrefab,Vector3.zero,Quaternion.identity,this.transform); 
        startVFX = Instantiate(beamVFXPrefab,Vector3.zero,Quaternion.identity,this.transform); 
        midVFX = Instantiate(beamVFXPrefab,Vector3.zero,Quaternion.identity,this.transform); 
        endVFX = Instantiate(beamVFXPrefab,Vector3.zero,Quaternion.identity,this.transform); 
        startVFX.transform.localScale = startVFXScale;
        midVFX.transform.localScale = midVFXScale;
        endVFX.transform.localScale = endVFXScale;

        // midVFXColor = midVFX.GetComponent<ParticleSystemRenderer>().material.color;
        midVFXRenderer = midVFX.GetComponent<ParticleSystemRenderer>();


    }
    // public void DrawLines(List<Vector2> keyPoints,Material material,bool laserStatus,float lineWidth)
    public void DrawLines(List<Vector2> keyPoints,bool laserStatus,float lineWidth)
    {      
       

        if(keyPoints.Count == 2)
        {   
            // midVFXColor.a = 0f;
            midVFXRenderer.enabled = false;

            // Draw Segment
            line_1.GetComponent<LaserLineSegment>().DrawLineSegment(keyPoints[0],keyPoints[1]);
            line_2.GetComponent<LineRenderer>().enabled = false;
            // Draw VFX
            startVFX.transform.position = keyPoints[0];
            endVFX.transform.position = keyPoints[1];
            
          



        }
        else if(keyPoints.Count == 3)
        {   
             // Draw Segment
            line_1.GetComponent<LaserLineSegment>().DrawLineSegment(keyPoints[0],keyPoints[1]);
            line_2.GetComponent<LineRenderer>().enabled = true;
            line_2.GetComponent<LaserLineSegment>().DrawLineSegment(keyPoints[1],keyPoints[2]);

            // Draw VFX 
            startVFX.transform.position = keyPoints[0];
            endVFX.transform.position = keyPoints[2];
            
            midVFX.transform.position = keyPoints[1];
            // midVFXColor.a = 1f;
            midVFXRenderer.enabled = true;

           


        }


        // Debug.Log("激光关键点");
        // foreach (var k in keyPoints)
        // {
        //      Debug.Log(k);
        // }
        // Debug.Log("---------");
       
        

    }

    // public void DrawLineSegment(Vector2 startPoint, Vector2 endPoint, Material material)
    
}
