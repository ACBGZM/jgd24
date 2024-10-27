using UnityEngine;

[CreateAssetMenu(fileName = "OctopusState_Transport", menuName = "StateMachine/OctopusState/Transport")]
public class OctopusState_Transport : OctopusState
{
    public Transform nextPosition;

    private float timer;
    public override void Enter()
    {
        base.Enter();
        timer = 0;
        stateMachine.UpdateRandomPosition();
        nextPosition = stateMachine.GetRandomPosition();

        animator.Play("Octopus_Transport");
        AnimationTool.AwaitCurrentAnimWhenEnd(animator, () =>
        {
            stateMachine.GoToNextState();
        });
    }

    public override void Execute()
    {
        base.Execute();
    }

    public override void Exit()
    {
        base.Exit();
    }
}