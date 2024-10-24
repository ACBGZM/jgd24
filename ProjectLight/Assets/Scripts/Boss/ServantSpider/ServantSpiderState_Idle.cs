using UnityEngine;

[CreateAssetMenu(menuName = "StateMachine/ServantSpiderState/Idle", fileName = "ServantSpiderState_Idle")]
public class ServantSpiderState_Idle : ServantSpiderState
{
    [SerializeField
#if UNITY_EDITOR
        , ReadOnly
#endif
        ]
    private float timer = 0;
    public float Timer => timer;

    public override void Enter()
    {
        base.Enter();
        timer = 0;
        animator.Play("ServantSpider_Idle");
    }

    public override void Execute()
    {
        base.Execute();
        timer += Time.deltaTime;

        if(timer >= stateMachine.IdleTime)
        {
            Vector2 playerPos = stateMachine.GetPlayerPosition();
            float distance = (playerPos - (Vector2)stateMachine.transform.position).magnitude;
            if(distance <= stateMachine.ChaseDistance)
            {
                stateMachine.ChangeState(typeof(ServantSpiderState_Chase));
            }
            else
            {
                stateMachine.ChangeState(typeof(ServantSpiderState_Hanging));
            }
        }
    }
}