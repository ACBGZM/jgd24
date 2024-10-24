using UnityEngine;

[CreateAssetMenu(menuName = "StateMachine/ServantSpiderState/Chase", fileName = "ServantSpiderState_Chase")]
public class ServantSpiderState_Chase : ServantSpiderState
{
    [SerializeField,
#if UNITY_EDITOR
        ReadOnly
#endif
        ]
    private float timer = 0;
    public float Timer { get; set; }

    private Vector2 playerPosition;
    private Vector2 targetPosition;

    private bool isChangingState = false;
    public override void Enter()
    {
        base.Enter();
        timer = 0;
        isChangingState = false;
        animator.Play("ServantSpider_Move");
    }

    public override void Execute()
    {
        base.Execute();
        timer += Time.deltaTime;

        playerPosition = stateMachine.GetPlayerPosition();

        /*'
        stateMachine.rb.MovePosition(Vector2.MoveTowards(stateMachine.transform.position, CalculateTargetPosition(playerPosition, (Vector2)stateMachine.transform.position, chaseDistanceOffset), stateMachine.moveSpeed * Time.deltaTime));
        */
        
        if(timer >= stateMachine.ChaseTime && !isChangingState)
        {
            playerPosition = stateMachine.GetPlayerPosition();
            float distance = (playerPosition - (Vector2)stateMachine.transform.position).magnitude;
            
            if(distance > stateMachine.MaxChaseDistance)
            {
                isChangingState = true;
                stateMachine.ChangeState(typeof(ServantSpiderState_Hanging));
            }
            timer = 0;
        }
    }

    public override void FixedExecute()
    {
        base.FixedExecute();

        // 移动到目标点
        if(!isChangingState)
        {
            stateMachine.rb.MovePosition(Vector2.MoveTowards(stateMachine.transform.position, playerPosition, stateMachine.MoveSpeed * Time.deltaTime));
        }
    }

    private Vector2 CalculateTargetPosition(Vector2 playerPos, Vector2 currentPos, float chaseDistanceOffset)
    {
        Vector2 chaseVector = (playerPos - currentPos).normalized;
        return playerPos + chaseVector * chaseDistanceOffset;
    }
}