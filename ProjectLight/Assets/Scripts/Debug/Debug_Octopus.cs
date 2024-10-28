using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// using UnityEditor.Experimental;

#if UNITY_EDITOR
using UnityEditor;
#endif

public class Debug_Octopus : MonoBehaviour
{
    public OctopusStateMachine stateMachine;

#if UNITY_EDITOR
    [Label("显示并排激光范围")]
#endif
    public bool showRowLaserRange = true;

#if UNITY_EDITOR
    [Label("并排激光SO"), ReadOnlyIfFalse("showRowLaserRange")]
#endif
    public Octopus_RowLaser RowLaserSO;
#if UNITY_EDITOR
    [Label("并排激光指示线长度"), ReadOnlyIfFalse("showRowLaserRange")]
#endif
    public float RowLaserLineLength = 3;

#if UNITY_EDITOR    
    [Label("显示旋转激光范围")]
#endif
    public bool showRotateLaserRange = true;

#if UNITY_EDITOR
    [Label("旋转激光SO"), ReadOnlyIfFalse("showRotateLaserRange")]
#endif
    public Octopus_RotateLaser RotateLaserSO;
#if UNITY_EDITOR
    [Label("旋转激光指示线长度"), ReadOnlyIfFalse("showRotateLaserRange")]
#endif
    public float RotateLaserLineLength = 3;
#if UNITY_EDITOR
    [Label("旋转光线-二阶段"), ReadOnlyIfFalse("showRotateLaserRange")]
#endif
    public bool RotateLaserPhase2 = false;

#if UNITY_EDITOR    
    [Label("显示五连激光范围")]
#endif
    public bool showFiveLaserRange = true;

#if UNITY_EDITOR
    [Label("五连激光SO"), ReadOnlyIfFalse("showRotateLaserRange")]
#endif
    public Octopus_FiveLaser FiveLaserSO;
#if UNITY_EDITOR
    [Label("五连激光指示线长度"), ReadOnlyIfFalse("showRotateLaserRange")]
#endif
    public float FiveLaserLineLength = 3;
#if UNITY_EDITOR
    [Label("五连光线-二阶段"), ReadOnlyIfFalse("showRotateLaserRange")]
#endif
    public bool FiveLaserPhase2 = false;


    void OnValidate()
    {
        stateMachine = GetComponent<OctopusStateMachine>();
    }

    void OnDrawGizmos()
    {
        if (showRowLaserRange)
        {
            Gizmos.color = Color.white;
            Vector2 originalPos = transform.position;
            Vector2 playerPos = GameObject.FindGameObjectWithTag("Player").transform.position;
            Vector2 laserDirection = RowLaserSO.aimToPlayer
            ? (playerPos - originalPos).normalized
            : Vector2.up;
            Vector2 laserStartPosition = originalPos + laserDirection * RowLaserSO.instantiateDistanceOffset;
            Vector3 verticalVector = Vector3.Cross(laserDirection, Vector3.forward).normalized;

            Gizmos.DrawSphere(laserStartPosition, 0.1f);
            Gizmos.DrawSphere(laserStartPosition + (Vector2)verticalVector * RowLaserSO.laserGap, 0.1f);
            Gizmos.DrawSphere(laserStartPosition + (Vector2)verticalVector * RowLaserSO.laserGap * 2, 0.1f);
            Gizmos.DrawSphere(laserStartPosition + (Vector2)verticalVector * RowLaserSO.laserGap * -1, 0.1f);
            Gizmos.DrawSphere(laserStartPosition + (Vector2)verticalVector * RowLaserSO.laserGap * -2, 0.1f);

            Gizmos.color = Color.red;

            Gizmos.DrawLine(laserStartPosition, laserStartPosition + laserDirection * RowLaserLineLength);
            Gizmos.DrawLine(laserStartPosition + (Vector2)verticalVector * RowLaserSO.laserGap, laserStartPosition + (Vector2)verticalVector * RowLaserSO.laserGap + laserDirection * RowLaserLineLength);
            Gizmos.DrawLine(laserStartPosition + (Vector2)verticalVector * RowLaserSO.laserGap * 2, laserStartPosition + (Vector2)verticalVector * RowLaserSO.laserGap * 2 + laserDirection * RowLaserLineLength);
            Gizmos.DrawLine(laserStartPosition + (Vector2)verticalVector * RowLaserSO.laserGap * -1, laserStartPosition + (Vector2)verticalVector * RowLaserSO.laserGap * -1 + laserDirection * RowLaserLineLength);
            Gizmos.DrawLine(laserStartPosition + (Vector2)verticalVector * RowLaserSO.laserGap * -2, laserStartPosition + (Vector2)verticalVector * RowLaserSO.laserGap * -2 + laserDirection * RowLaserLineLength);
        }
        if (showRotateLaserRange)
        {
            Gizmos.color = Color.white;
            Gizmos.DrawSphere((Vector2)stateMachine.transform.position + new Vector2(0, 1).normalized * RotateLaserSO.instantiateDistanceOffset, 0.1f);
            Gizmos.color = Color.red;
            Gizmos.DrawLine((Vector2)stateMachine.transform.position + new Vector2(0, 1).normalized * RotateLaserSO.instantiateDistanceOffset, (Vector2)stateMachine.transform.position + new Vector2(0, 1).normalized * RotateLaserSO.instantiateDistanceOffset + new Vector2(0, 1) * RotateLaserLineLength);

            Gizmos.color = Color.white;
            Gizmos.DrawSphere((Vector2)stateMachine.transform.position + new Vector2(1, 0).normalized * RotateLaserSO.instantiateDistanceOffset, 0.1f);
            Gizmos.color = Color.red;
            Gizmos.DrawLine((Vector2)stateMachine.transform.position + new Vector2(1, 0).normalized * RotateLaserSO.instantiateDistanceOffset, (Vector2)stateMachine.transform.position + new Vector2(1, 0).normalized * RotateLaserSO.instantiateDistanceOffset + new Vector2(1, 0) * RotateLaserLineLength);

            Gizmos.color = Color.white;
            Gizmos.DrawSphere((Vector2)stateMachine.transform.position + new Vector2(0, -1).normalized * RotateLaserSO.instantiateDistanceOffset, 0.1f);
            Gizmos.color = Color.red;
            Gizmos.DrawLine((Vector2)stateMachine.transform.position + new Vector2(0, -1).normalized * RotateLaserSO.instantiateDistanceOffset, (Vector2)stateMachine.transform.position + new Vector2(0, -1).normalized * RotateLaserSO.instantiateDistanceOffset + new Vector2(0, -1) * RotateLaserLineLength);

            Gizmos.color = Color.white;
            Gizmos.DrawSphere((Vector2)stateMachine.transform.position + new Vector2(-1, 0).normalized * RotateLaserSO.instantiateDistanceOffset, 0.1f);
            Gizmos.color = Color.red;
            Gizmos.DrawLine((Vector2)stateMachine.transform.position + new Vector2(-1, 0).normalized * RotateLaserSO.instantiateDistanceOffset, (Vector2)stateMachine.transform.position + new Vector2(-1, 0).normalized * RotateLaserSO.instantiateDistanceOffset + new Vector2(-1, 0) * RotateLaserLineLength);
            if (RotateLaserPhase2)
            {
                Gizmos.color = Color.white;
                Gizmos.DrawSphere((Vector2)stateMachine.transform.position + new Vector2(0.7071f, 0.7071f).normalized * RotateLaserSO.instantiateDistanceOffset, 0.1f);
                Gizmos.color = Color.red;
                Gizmos.DrawLine((Vector2)stateMachine.transform.position + new Vector2(0.7071f, 0.7071f).normalized * RotateLaserSO.instantiateDistanceOffset, (Vector2)stateMachine.transform.position + new Vector2(0.7071f, 0.7071f).normalized * RotateLaserSO.instantiateDistanceOffset + new Vector2(0.7071f, 0.7071f) * RotateLaserLineLength);

                Gizmos.color = Color.white;
                Gizmos.DrawSphere((Vector2)stateMachine.transform.position + new Vector2(-0.7071f, 0.7071f).normalized * RotateLaserSO.instantiateDistanceOffset, 0.1f);
                Gizmos.color = Color.red;
                Gizmos.DrawLine((Vector2)stateMachine.transform.position + new Vector2(-0.7071f, 0.7071f).normalized * RotateLaserSO.instantiateDistanceOffset, (Vector2)stateMachine.transform.position + new Vector2(-0.7071f, 0.7071f).normalized * RotateLaserSO.instantiateDistanceOffset + new Vector2(-0.7071f, 0.7071f) * RotateLaserLineLength);

                Gizmos.color = Color.white;
                Gizmos.DrawSphere((Vector2)stateMachine.transform.position + new Vector2(-0.7071f, -0.7071f).normalized * RotateLaserSO.instantiateDistanceOffset, 0.1f);
                Gizmos.color = Color.red;
                Gizmos.DrawLine((Vector2)stateMachine.transform.position + new Vector2(-0.7071f, -0.7071f).normalized * RotateLaserSO.instantiateDistanceOffset, (Vector2)stateMachine.transform.position + new Vector2(-0.7071f, -0.7071f).normalized * RotateLaserSO.instantiateDistanceOffset + new Vector2(-0.7071f, -0.7071f) * RotateLaserLineLength);

                Gizmos.color = Color.white;
                Gizmos.DrawSphere((Vector2)stateMachine.transform.position + new Vector2(0.7071f, -0.7071f).normalized * RotateLaserSO.instantiateDistanceOffset, 0.1f);
                Gizmos.color = Color.red;
                Gizmos.DrawLine((Vector2)stateMachine.transform.position + new Vector2(0.7071f, -0.7071f).normalized * RotateLaserSO.instantiateDistanceOffset, (Vector2)stateMachine.transform.position + new Vector2(0.7071f, -0.7071f).normalized * RotateLaserSO.instantiateDistanceOffset + new Vector2(0.7071f, -0.7071f) * RotateLaserLineLength);
            }
        }
        if (showFiveLaserRange)
        {
            Vector2 playerPos = GameObject.FindGameObjectWithTag("Player").transform.position;
            Vector2 originalPos = stateMachine.transform.position;
            Vector2 directionVector = playerPos - originalPos;
            Gizmos.color = Color.white;
            Gizmos.DrawSphere(originalPos + directionVector.normalized * FiveLaserSO.onShot.instantiateDistanceOffset, 0.1f);
            Gizmos.color = Color.red;
            Gizmos.DrawLine(originalPos + directionVector.normalized * FiveLaserSO.onShot.instantiateDistanceOffset, originalPos + directionVector.normalized * FiveLaserSO.onShot.instantiateDistanceOffset + directionVector * FiveLaserLineLength);

            if(FiveLaserPhase2)
            {
                Gizmos.color = Color.white;
                Vector2 p1 = playerPos + new Vector2(1, 1).normalized * FiveLaserSO.protectiveLaser.laserOffset;
                Vector2 p2 = playerPos + new Vector2(-1, 1).normalized * FiveLaserSO.protectiveLaser.laserOffset;
                Vector2 p3 = playerPos + new Vector2(1, -1).normalized * FiveLaserSO.protectiveLaser.laserOffset;
                Vector2 p4 = playerPos + new Vector2(-1, -1).normalized * FiveLaserSO.protectiveLaser.laserOffset;
                Gizmos.DrawSphere(p1, 0.1f);
                Gizmos.DrawSphere(p2, 0.1f);
                Gizmos.DrawSphere(p3, 0.1f);
                Gizmos.DrawSphere(p4, 0.1f);

                Gizmos.color = Color.red;
                Gizmos.DrawLine(p1, p2);
                Gizmos.DrawLine(p1, p3);
                Gizmos.DrawLine(p2, p4);
                Gizmos.DrawLine(p3, p4);

            }
        }
    }
}

#if UNITY_EDITOR
[CustomEditor(typeof(Debug_Octopus))]
public class Debug_Octopus_Editor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();
        Debug_Octopus debugOctopus = (Debug_Octopus)target;
        if (GUILayout.Button("瘫痪"))
        {
            if (UnityEditor.EditorApplication.isPlaying)
            {
                debugOctopus.stateMachine.Palsy();
            }
            else
            {
                Debug.LogError("请在运行时使用");
            }
        }
    }
}
#endif