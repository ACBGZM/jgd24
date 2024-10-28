using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewLaserLogic : MonoBehaviour
{ 
    [SerializeField] private int m_damage = 20;

    // Test
    public float testAngle;

    // private LineRenderer lineRenderer;
    public GameObject LaserLinePrefab;
     private GameObject laserRender;

    // Laser Param
    private int pointCount;
    private Vector2 currentPosion;

    private List<Vector2> keyPoints;

    public string[] layerMasks;
    private LayerMask layerMask;

    private bool laserStatus = true;
        public int maxReflectTimes = 1;

    public int MAX_LENGTH = 1000; // todo : private
    public float OFFSET = 0.0001f; // todo : private

    public float lineWidth = 0.1f;

    public float laserLifeTime = 5;
    

    public float initLaunchAngle = 0;
    public float initLaunchDistanceOffet = 0;

    public Vector2 startPosition;
    public Vector2 startDirection;

    private float lifeTimer = 0;


    public Material meterial; // 0: Aim ; 1: Emit


    // 判定
    private GameObject hitObject;

    private void Awake()
    {   
        // LaserLineRenderer 基础属性配置
        // lineRenderer = Instantiate(LaserLinePrefab).GetComponent<LineRenderer>();
        // lineRenderer.startWidth = lineWidth;
        // lineRenderer.endWidth = lineWidth;
        // lineRenderer.material = meterial;

        startPosition = Vector2.zero;
        startDirection = Vector2.up;

        // 碰撞关系配置
        layerMask = LayerMask.GetMask(layerMasks);

        lifeTimer = 0;
    }

    void Start()
    {
        // lineRenderer = Instantiate(LaserLineRenderer).GetComponent<LineRenderer>();
        keyPoints = new List<Vector2>();
        Debug.Log((startPosition, startDirection));
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

        LaserRay(startPosition, startDirection);
        lifeTimer += Time.deltaTime;
        if(lifeTimer >= laserLifeTime)
        {
            Destroy(gameObject);
            Destroy(laserRender.gameObject);
        }
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

        keyPoints.Clear();
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

        while (hit.collider != null && laserStatus&& pointCount<=maxReflectTimes+1)
        {
            currentPosion = hit.point;
            keyPoints.Add(currentPosion);    


            hitObject = hit.collider.gameObject;

            // lineRenderer.positionCount += 1;
            // lineRenderer.SetPosition(pointCount++, hit.point);

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

        DrawLaser();

        // if (laserStatus)
        // {
        //     lineRenderer.positionCount += 1; 
        //     lineRenderer.SetPosition(++pointCount, (currentPosion + rayDirection) * 1000);
        // }
    }

    public void HitRole(string hitTag, RaycastHit2D hit)
    {   
            LaserDamage damageable = hitObject?.GetComponent<LaserDamage>();
            if (damageable != null)
            {
                damageable.OnLaserHit(m_damage);
            }
    }


    public void DrawLaser()
    {       
        // laserRender.GetComponent<LaserRender>().DrawLines(keyPoints,meterial,laserStatus,lineWidth);
        laserRender.GetComponent<LaserRender>().DrawLines(keyPoints,laserStatus,lineWidth);
    }

    public void SetLaunchParameter(Vector2 startPosition, Vector2 startDirection)
    {
        this.startPosition = startPosition;
        this.startDirection = startDirection;
    }

    public void MoveLaser(Vector2 direction)
    {
        startPosition = startPosition + direction;
    }

    public void DestroyLaser()
    {
        Destroy(laserRender.gameObject);
        Destroy(gameObject);
    }
}
