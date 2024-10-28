using UnityEngine;

public class Debug_ServantSpider : MonoBehaviour
{
    public ServantSpiderStateMachine stateMachine;

    void OnEnable()
    {
        stateMachine = GetComponent<ServantSpiderStateMachine>();
    }

#if UNITY_EDITOR
    [Label("显示Idle时追击范围")]
#endif
    public bool showIdleChaseRadius = true;

    #if UNITY_EDITOR
    [Label("Idle时追击范围线框颜色"), ReadOnlyIfFalse("showIdleChaseRadius")]
#endif
    public Color idleChaseRadiusColor = Color.green;

#if UNITY_EDITOR
    [Label("显示Chase时追击范围")]
#endif
    public bool showCahseChaseRadius = true;

#if UNITY_EDITOR
    [Label("Cahse时追击范围线框颜色"), ReadOnlyIfFalse("showCahseChaseRadius")]
#endif
    public Color chaseChaseRadiusColor = Color.blue;

#if UNITY_EDITOR
    [Label("显示下砸攻击范围")]
#endif
    public bool showAttackRadius = true;

#if UNITY_EDITOR
    [Label("下砸攻击范围线框颜色"), ReadOnlyIfFalse("showAttackRadius")]
#endif
    public Color attackRadiusColor = Color.red;

    public void OnDrawGizmos()
    {
        if(showIdleChaseRadius)
        {
            Gizmos.color = idleChaseRadiusColor;
            Gizmos.DrawWireSphere(transform.position,stateMachine.ChaseDistance);
        }
        if(showCahseChaseRadius)
        {
            Gizmos.color = chaseChaseRadiusColor;
            Gizmos.DrawWireSphere(transform.position,stateMachine.MaxChaseDistance);
        }
        if(showAttackRadius)
        {
            Gizmos.color = attackRadiusColor;
            Gizmos.DrawWireSphere(transform.position,stateMachine.AttackRadius);
        }
    }
}