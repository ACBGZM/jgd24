using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;

[CreateAssetMenu(menuName = "StateMachine/SpiderState/Idle", fileName = "SpiderState_Idle")]
public class SpiderState_Idle : SpiderState
{
    private float timer = 0;
    private bool isChangingState = false;
    public override void Enter()
    {
        base.Enter();
        stateMachine.SetCurrentStateType(SpiderStateType.Idle);
        animator.Play("Spider_CocoonIdle");
        timer = 0;
        isChangingState = false;
    }

    public override void Execute()
    {
        base.Execute();
        timer += Time.deltaTime;

        if(timer >= stateMachine.IdleTime && !isChangingState)
        {
            isChangingState = true;
            //stateMachine.animationPlayControl.PlayAnimation("Spider_Cocoon2Spider", null,
            //    () => { stateMachine.GoToNextState(); });
            stateMachine.GoToNextState();
        }
    }
}