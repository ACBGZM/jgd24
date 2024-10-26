using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Laser : MonoBehaviour
{   
    [SerializeField] private int m_damage = 20;

    // Test
    public float testAngle;

    // Laser Param    
    public float laserLifeTime = 5;

    [Header("逻辑参数")]
    // 最大反射次数
    public int maxReflectTimes = 1;

    // 碰撞逻辑
    public string[] layerMasks;
    private LayerMask layerMask;

    // 激光状态
    private bool laserStatus = true; // 激光未造成伤害状态
    private int laserStage = 0; // 激光阶段: Aim, Emit

    // KeyPoint
    private int pointCount;
    private Vector2 currentPosion;
    private List<Vector2> keyPoints;

    // 其他设置
    private int MAX_LENGTH = 10000; // todo : private
    private float OFFSET = 0.0001f; // todo : private

    // 
    public float initLaunchAngle = 0;
    public float initLaunchDistanceOffet = 0;
    public float rotateTime = 0;
    public float rotateAngle = 0;

    private float currentRotateAngle = 0;
    private float rotateTimer = 0;

    
    [Header("渲染参数")]
    // private LineRenderer lineRenderer;
    public GameObject LaserLinePrefab;
    public GameObject laserRender;
    public float lineWidth = 0.1f;
    [Tooltip("激光外观")]
    public List<Material> meterialList; // 0: Aim ; 1: Emit
   

    // 判定
    private GameObject hitObject;

    private void Awake()
    {   
        // LaserLineRenderer 基础属性配置
        // lineRenderer = Instantiate(LaserLinePrefab).GetComponent<LineRenderer>();
        // lineRenderer.startWidth = lineWidth;
        // lineRenderer.endWidth = lineWidth;
        // lineRenderer.material = meterialList[0];


        // 碰撞关系配置
        layerMask = LayerMask.GetMask(layerMasks);

        currentRotateAngle = 0;
    }

    void Start()
    {
        // lineRenderer = Instantiate(LaserLineRenderer).GetComponent<LineRenderer>();
        keyPoints = new List<Vector2>();
        laserRender = Instantiate(LaserLinePrefab,Vector3.zero,Quaternion.identity); 
    }

    void Update()
    {
        /*
        // Test: 鼠标方向射线
        Vector2 mousePosition;
        if (Mouse.current != null)
        {
            mousePosition = Mouse.current.position.ReadValue();
        }
        else
        {
            mousePosition = Vector2.zero;
        }

        Vector3 worldMousePosition = Camera.main.ScreenToWorldPoint(
            new Vector3(mousePosition.x, mousePosition.y, 0)
        );
        Vector2 startDirection = new Vector2(
            worldMousePosition.x - transform.position.x,
            worldMousePosition.y - transform.position.y
        ).normalized;
        // Vector2 startDirection =  AngleToUnitVector2D(testAngle);
        */


        if (currentRotateAngle < rotateAngle && rotateTime != 0)
        {
            currentRotateAngle += (rotateAngle / rotateTime) * Time.deltaTime;
        }

        Vector2 startDirection = MathTool.RotateVector2(Vector2.up, Mathf.Deg2Rad * (initLaunchAngle + currentRotateAngle));
        Vector2 StartPosition = transform.position + (Vector3)startDirection * initLaunchDistanceOffet;

        LaserRay(StartPosition, startDirection);
    }



    Vector2 AngleToUnitVector2D(float angleInDegrees)
    {
        float radians = angleInDegrees * Mathf.Deg2Rad;
        return new Vector2(Mathf.Cos(radians), Mathf.Sin(radians));
    }

    public void LaserRay(Vector2 startPosition, Vector2 rayDirection)
    {
        // Init lineRenderer
        
        // lineRenderer.positionCount = 1;
        // lineRenderer.SetPosition(0, startPosition);
        laserStatus = true;
        keyPoints.Add(startPosition);    
        pointCount = 1;   
        
         

        // Raycast Hit
        RaycastHit2D hit = Physics2D.Raycast(
            transform.position,
            rayDirection,
            MAX_LENGTH,
            layerMask
        );

        while (hit.collider != null && laserStatus && pointCount<=maxReflectTimes+1)
        {
            currentPosion = hit.point;

            hitObject = hit.collider.gameObject;

            // lineRenderer.positionCount += 1;
            // lineRenderer.SetPosition(pointCount++, hit.point);
            pointCount += 1 ;
            keyPoints.Add(hit.point);

            if (hitObject.tag == "Mirror")
            {
                // 创建新射线
                rayDirection = Vector2.Reflect(rayDirection, hit.normal);
                currentPosion = hit.point + OFFSET * rayDirection;

                hit = Physics2D.Raycast(currentPosion, rayDirection, MAX_LENGTH, layerMask);
                
                LaserDamage damageable = hitObject?.GetComponent<LaserDamage>();
                if (damageable != null)
                {
                    damageable.OnLaserHit(m_damage);
                }

            }
            else if (hitObject.tag == "Player" | hitObject.tag == "Boss")
            {
                HitRole(hitObject.tag, hit);
                laserStatus = false;
            }
            else
            {
                laserStatus = false; // 销毁射线
            }
        }

        // if (laserStatus)
        // {
            // lineRenderer.positionCount += 1;
            // pointCount += 1 ;
            // lineRenderer.SetPosition(++pointCount, (currentPosion + rayDirection) * 1000);
        // }
    }

    public void DrawLaser()
    {   
        
        laserRender.GetComponent<LaserRender>().DrawLines(keyPoints,meterialList[laserStage],laserStatus,lineWidth);
        // keyPoints

    }

    public void HitRole(string hitTag, RaycastHit2D hit)
    {   
            LaserDamage damageable = hitObject?.GetComponent<LaserDamage>();
            if (damageable != null)
            {
                damageable.OnLaserHit(m_damage);
            }
    }

    public void SetLaserStat(LaserLauncherStat launcherStat)
    {
        lineWidth = launcherStat.LaserWidth;
        laserLifeTime = launcherStat.LaserLifeTime;
        initLaunchAngle = launcherStat.InitLaunchAngle;
    }
}
