using UnityEngine;

[CreateAssetMenu(menuName = "StateMachine/ServantSpiderState/Cocoon", fileName = "ServantSpiderState_Cocoon")]
public class ServantSpiderState_Egg : ServantSpiderState
{
    [SerializeField
#if UNITY_EDITOR
        , ReadOnly
#endif
        ]
    private float timer = 0;
    public float Timer => timer;
    public bool isEggLanded = false;
    public bool isChangingState = false;

    public override void Enter()
    {
        base.Enter();
        timer = 0;
        isEggLanded = false;
        isChangingState = false;
        animator.Play("ServantSpider_EggDrop");
        AnimationTool.AwaitCurrentAnimWhenEnd(animator, () => 
        {
            isEggLanded = true;
            animator.Play("ServantSpider_EggIdle");
        });
    }

    public override void Execute()
    {
        base.Execute();
        if(isEggLanded)
        {
            timer += Time.deltaTime;
        }
        

        if(timer >= stateMachine.CocoonTime && !isChangingState)
        {
            isChangingState = true;
            animator.Play("ServantSpider_Egg2Spider");
            AnimationTool.AwaitCurrentAnimWhenEnd(animator, () => 
            {
                stateMachine.ChangeState(typeof(ServantSpiderState_Idle));
            });
        }
    }

    public override void Exit()
    {
        base.Exit();
    }
}