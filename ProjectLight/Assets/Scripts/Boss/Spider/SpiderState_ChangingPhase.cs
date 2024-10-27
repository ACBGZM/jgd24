using UnityEngine;

[CreateAssetMenu(menuName = "StateMachine/SpiderState/ChangingPhase", fileName ="State_ChangingPhase")]
public class SpiderState_ChangingPhase : SpiderState
{
    public override void Enter()
    {
        base.Enter();
        stateMachine.SetCurrentStateType(SpiderStateType.ChangingPhase);

        animator.Play("Spider_Roar");
        AnimationTool.AwaitCurrentAnimWhenEnd(animator, () => { stateMachine.GoToNextState(); });
    }
}