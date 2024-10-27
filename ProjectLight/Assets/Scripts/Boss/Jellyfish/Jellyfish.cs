using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jellyfish : MonoBehaviour
{
    public Animator animator;
    public OctopusStateMachine OctopusStateMachine;

    public GameObject warningLinePrefab;
    public float warningLineLength = 10;
    public float warningLineWidth = 0.1f;
    public float warningTime = 1;
    public Material warningLineMaterial;

    public GameObject LaserLauncherPrefab;
    public Vector2 LaserDirection;
    public bool aimPlayer = false;

    public float rotateTime = 0;
    public float rotateAngle = 0;

    private bool isShooting = false;
    private float timer;
    private LineRenderer warningLine;
    private GameObject laser;
    public float currentRotateAngle = 0;

    void Start()
    {
        animator= GetComponent<Animator>();
        timer = 0;
        isShooting = false;

        if(warningLine == null)
        {
            warningLine = Instantiate(warningLinePrefab).GetComponent<LineRenderer>();
            warningLine.startWidth = 0;
            warningLine.endWidth = 0;
            warningLine.material = warningLineMaterial;
        }

        animator.Play("Jellyfish_Idle");
    }

    void Update()
    {
        Vector3 playerPosition = GameObject.FindGameObjectWithTag("Player") ? GameObject.FindGameObjectWithTag("Player").transform.position : Vector3.zero;
        timer += Time.deltaTime;
        if(timer <= warningTime)
        {
            LaserDirection = aimPlayer 
            ? (playerPosition - transform.position).normalized 
            : LaserDirection;
            warningLine.startWidth = Mathf.Lerp(0, warningLineWidth, timer / warningTime);
            warningLine.endWidth = Mathf.Lerp(0, warningLineWidth, timer / warningTime);
            warningLineMaterial.SetFloat("_WarningLineAlpha", Mathf.Lerp(0, 0.5f, timer / warningTime));

            warningLine.SetPosition(0, transform.position);
            warningLine.SetPosition(1, transform.position + (Vector3)LaserDirection * warningLineLength);
        }

        if(timer >= warningTime)
        {
            if(warningLine != null)
            {
                Destroy(warningLine.gameObject);
            }
            
            if(!isShooting)
            {
                isShooting = true;
                LaunchLaser();
            }

            if(rotateTime > 0 && currentRotateAngle < rotateAngle)
            {
                currentRotateAngle += (rotateAngle / rotateTime) * Time.deltaTime;
                Vector2 ocotoPosition = OctopusStateMachine.transform.position;
                Vector2 directionVector = (Vector2)transform.position - ocotoPosition;
                Vector2 newDirection = MathTool.RotateVector2(directionVector, Mathf.Deg2Rad * (rotateAngle / rotateTime) * Time.deltaTime);
                Vector2 targetPosition = (Vector2)OctopusStateMachine.transform.position + newDirection;
                transform.position = targetPosition;
                laser.transform.position = targetPosition;
                LaserDirection = newDirection;
            }

            if(laser != null)
            {
                laser.GetComponent<Laser>().SetLaunchParameter(transform.position, LaserDirection);
            }
        }
    }

    void FixedUpdate()
    {
        if(timer >= warningTime && rotateTime > 0)
        {
        }
    }

    public void LaunchLaser()
    {
        animator.Play("Jellyfish_Shot");
        laser = Instantiate(LaserLauncherPrefab, transform.position, Quaternion.identity);
        laser.GetComponent<Laser>().SetLaunchParameter(transform.position, LaserDirection);
        AnimationTool.AwaitCurrentAnimWhenEnd(animator, () =>
        {
            animator.Play("Jellyfish_Fade");
        });
    }

    public void Init(OctopusStateMachine octopusStateMachine)
    {
        OctopusStateMachine = octopusStateMachine;
    }

    public void Dead()
    {
        if(laser)
        {
            laser.GetComponent<Laser>().DestroyLaser();
        }
        Destroy(gameObject);
    }
}

