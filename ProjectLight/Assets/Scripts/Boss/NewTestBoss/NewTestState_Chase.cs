using UnityEngine;

[CreateAssetMenu(menuName = "StateMachine/NewTestBossState/Chase", fileName = "NewTestState_Chase")]
public class NewTestState_Chase : NewTestState
{
    [SerializeField]
    private float distanceToPlayer = 0;
    public float DistanceToPlayer => distanceToPlayer;

    private Vector2 playerPosition;
    public Vector2 PlayerPosition => playerPosition;
    private Vector2 targetPosition;
    public Vector2 TargetPosition => targetPosition;

    public override void Enter()
    {
        base.Enter();
        playerPosition = stateMachine.GetPlayerPosition();

        // 目标点为距离玩家distanceToPlayer的位置
        targetPosition = playerPosition + ((Vector2)stateMachine.transform.position - playerPosition).normalized * distanceToPlayer;

        // 绘制路径
        Debug.DrawLine(stateMachine.transform.position, targetPosition, Color.red, 1f);
    }

    public override void Execute()
    {
        base.Execute();

        // 移动到目标点
        stateMachine.rb.MovePosition(Vector2.MoveTowards(stateMachine.transform.position, targetPosition, stateMachine.moveSpeed * Time.deltaTime));

        // 判断是否到达目标点
        if(Vector2.Distance(stateMachine.transform.position, targetPosition) <= 0.1f)
        {
            stateMachine.GoToNextState();
        }
    }
}