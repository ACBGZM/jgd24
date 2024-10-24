using UnityEngine;

[CreateAssetMenu(menuName = "StateMachine/ServantSpiderState/Hanging", fileName = "ServantSpiderState_Hanging")]
public class ServantSpiderState_Hanging : ServantSpiderState
{
    private Vector2 playerPosition;

    public bool isHanging = false;
    public bool isChangingState = false;
    public bool isDroping = false;

    public override void Enter()
    {
        base.Enter();
        isHanging = false;
        isChangingState = false;

        animator.Play("ServantSpider_Up");
        AnimationTool.AwaitCurrentAnimWhenEnd(animator, () =>
        {
            isHanging = true;
            animator.Play("ServantSpider_ShadowWalk");
        });
    }

    public override void Execute()
    {
        base.Execute();

        if(isHanging)
        {
            // 移动到目标点
            playerPosition = stateMachine.GetPlayerPosition();
            float distance = ((Vector2)stateMachine.transform.position - playerPosition).magnitude;

            if(distance <= stateMachine.AttackRadius)
            {
                isChangingState = true;
                animator.Play("ServantSpider_Drop");
                AnimationTool.AwaitCurrentAnimWhenEnd(animator, () =>
                {
                    stateMachine.ChangeState(typeof(ServantSpiderState_Idle));
                });
            }
        }
    }

    public override void FixedExecute()
    {
        base.FixedExecute();
        if(isHanging && !isChangingState)
        {
            stateMachine.rb.MovePosition(Vector2.MoveTowards(stateMachine.transform.position, playerPosition, stateMachine.MoveSpeed * Time.fixedDeltaTime));
        }
    }
}