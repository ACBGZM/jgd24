using UnityEngine;

[CreateAssetMenu(menuName = "StateMachine/ServantSpiderState/Hanging", fileName = "ServantSpiderState_Hanging")]
public class ServantSpiderState_Hanging : ServantSpiderState
{
    private Vector2 playerPosition;

    public override void Enter()
    {
        base.Enter();
        animator.Play("ServantSpider_Up");
        stateMachine.transform.localScale = 1.5f * Vector3.one;
    }

    public override void Execute()
    {
        base.Execute();

        // 移动到目标点
        playerPosition = stateMachine.GetPlayerPosition();
        float distance = ((Vector2)stateMachine.transform.position - playerPosition).magnitude;

        if(distance <= stateMachine.AttackRadius)
        {
            animator.Play("ServantSpider_Drop");
            stateMachine.transform.localScale = Vector3.one;
            stateMachine.ChangeState(typeof(ServantSpiderState_Idle));
        }
    }

    public override void FixedExecute()
    {
        base.FixedExecute();
        stateMachine.rb.MovePosition(Vector2.MoveTowards(stateMachine.transform.position, playerPosition, stateMachine.MoveSpeed * Time.fixedDeltaTime));
    }
}