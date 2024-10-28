using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

[RequireComponent(typeof(SpiderStateMachine))]
public class Debug_Spider : MonoBehaviour
{
    public SpiderStateMachine stateMachine;

#if UNITY_EDITOR
    [Label("当前蜘蛛状态"), ReadOnly]
#endif
    public SpiderStateType currentState;

    #region 移动范围

#if UNITY_EDITOR
    [Label("显示移动范围")]
#endif
    public bool showMoveRange = true;

    #if UNITY_EDITOR
[Label("移动范围限制"), ReadOnlyIfFalse("showMoveLimit")]
    #endif
    public bool showMoveLimit = true;

    #endregion

    #region 狙击参数

#if UNITY_EDITOR
    [Label("显示狙击方向")]
#endif
    public bool showSnipeDirection = true;

#if UNITY_EDITOR 
    [Label("狙击SO"), ReadOnlyIfFalse("showSnipeDirection")]
#endif
    public BulletSkill SnipeSO;

#if UNITY_EDITOR
    [Label("狙击方向线颜色"), ReadOnlyIfFalse("showSnipeDirection")]
#endif
    public Color SnipeLineColor = Color.red;

#if UNITY_EDITOR
    [Label("狙击方向线长度"), ReadOnlyIfFalse("showSnipeDirection")]
#endif
    public float SnipeLineLength = 3;

    #endregion

    #region 四周射击参数

#if UNITY_EDITOR
    [Label("显示四周射击方向")]
#endif
    public bool showOctoShotDirection = true;

#if UNITY_EDITOR 
    [Label("四周射击SO"), ReadOnlyIfFalse("showOctoShotDirection")]
#endif
    public BulletSkill OctoShotSO;

#if UNITY_EDITOR
    [Label("四周射击方向线颜色"), ReadOnlyIfFalse("showOctoShotDirection")]
#endif
    public Color OctoShotLineColor = Color.red;

#if UNITY_EDITOR
    [Label("四周射击方向线长度"), ReadOnlyIfFalse("showOctoShotDirection")]
#endif
    public float OctoShotLineLength = 3;

    #endregion

    void OnValidate()
    {
        stateMachine = GetComponent<SpiderStateMachine>();
    }

    void Update()
    {
        currentState = stateMachine.CurrentStateType;
    }
    private void OnDrawGizmos()
    {
        if (stateMachine != null)
        {
            Vector2 playerPos = GameObject.FindGameObjectWithTag("Player").transform.position;
            Vector2 spiderPos = stateMachine.gameObject.transform.position;
            if (showMoveRange)
            {
                Vector2 ps = spiderPos - playerPos;
                Vector2 direction = ps.normalized;
                float distance = ps.magnitude;
                (float, float) externalAngleRange = MathTool.CalculateAngleRange(playerPos, stateMachine.TargetAreaExternalRadius, spiderPos);
                float oppsiteAngle = Mathf.Rad2Deg * (externalAngleRange.Item2 - externalAngleRange.Item1);
                float angle = distance <= stateMachine.TargetAreaExternalRadius
                ? 180
                : Mathf.Clamp(oppsiteAngle, stateMachine.MinTargetAngle, stateMachine.MaxTargetAngle);
                int angleINT = (int)angle;

                GizmosTool.DrawWireSemicircle2D(playerPos, direction, stateMachine.TargetAreaExternalRadius, angleINT);
                GizmosTool.DrawWireSemicircle2D(playerPos, direction, stateMachine.TargetAreaInnerRadius, angleINT);


                /*
                if ((spiderPos - (Vector2)transform.position).magnitude <= spiderState_Move.TargetAreaExternalRadius)
                {
                    spiderPos = (Vector2)transform.position + (spiderPos - (Vector2)transform.position).normalized *
                        spiderState_Move.TargetAreaExternalRadius * 2;
                }
                (Vector2, Vector2) tangentPoints = MathTool.CalculateTangent(transform.position, spiderState_Move.TargetAreaExternalRadius, spiderPos);
                (float, float) angleRange = MathTool.CalculateAngleRange(transform.position,
                    spiderState_Move.TargetAreaExternalRadius, spiderPos);
                int angle = (int)(Mathf.Rad2Deg * (Mathf.PI - (angleRange.Item2 - angleRange.Item1)));
                Vector2 direction = (spider.transform.position - transform.position).normalized;
                GizmosTool.DrawWireSemicircle2D(transform.position, direction, spiderState_Move.TargetAreaExternalRadius, angle);
                GizmosTool.DrawWireSemicircle2D(transform.position, direction, spiderState_Move.TargetAreaInnerRadius, angle);
                Gizmos.DrawLine(tangentPoints.Item1, spiderPos);
                Gizmos.DrawLine(tangentPoints.Item2, spiderPos);
                */
            }
            if (showSnipeDirection)
            {
                Gizmos.color = SnipeLineColor;
                foreach (BulletLauncherStat launcher in SnipeSO.launcherStats)
                {
                    Vector2 direction = SnipeSO.aimToPlayer
                        ? (playerPos - spiderPos).normalized
                        : Vector2.up;
                    direction = MathTool.RotateVector2(direction, Mathf.Deg2Rad * launcher.InitLaunchAngle);
                    Gizmos.DrawLine(spiderPos, spiderPos + direction * SnipeLineLength);
                }
            }
            if (showOctoShotDirection)
            {
                Gizmos.color = OctoShotLineColor;
                foreach (BulletLauncherStat launcher in OctoShotSO.launcherStats)
                {
                    Vector2 direction = OctoShotSO.aimToPlayer
                        ? (playerPos - spiderPos).normalized
                        : Vector2.up;
                    direction = MathTool.RotateVector2(direction, Mathf.Deg2Rad * launcher.InitLaunchAngle);
                    Gizmos.DrawLine(spiderPos, spiderPos + direction * OctoShotLineLength);
                }
            }
            if (showMoveLimit)
            {
                float minX = stateMachine.rangeX.x;
                float maxX = stateMachine.rangeX.y;
                float minY = stateMachine.rangeY.x;
                float maxY = stateMachine.rangeY.y;
                Vector2 p1 = new Vector2(minX, minY);
                Vector2 p2 = new Vector2(minX, maxY);
                Vector2 p3 = new Vector2(maxX, minY);
                Vector2 p4 = new Vector2(maxX, maxY);
                Gizmos.color = Color.blue;
                Gizmos.DrawLine(p1, p2);
                Gizmos.DrawLine(p1, p3);
                Gizmos.DrawLine(p2, p4);
                Gizmos.DrawLine(p3, p4);
            }
        }
    }
}

#if UNITY_EDITOR
[CustomEditor(typeof(Debug_Spider))]
public class Debug_Spider_Editor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();
        Debug_Spider debugSpider = (Debug_Spider)target;
        if (GUILayout.Button("转阶段"))
        {
            if (EditorApplication.isPlaying)
            {
                debugSpider.stateMachine.ChangePhase();
            }
            else
            {
                Debug.LogError("请在运行时使用");
            }
        }
    }
}
#endif