using UnityEngine;

[CreateAssetMenu(fileName = "OctopusState_Transport", menuName = "StateMachine/OctopusState/Transport")]
public class OctopusState_Transport : OctopusState
{
    private Transform nextPosition;

    private float timer;
    public override void Enter()
    {
        base.Enter();
        timer = 0;
        stateMachine.UpdateRandomPosition();
        nextPosition = stateMachine.GetRandomPosition();

        stateMachine.transform.position = nextPosition.position;
        stateMachine.currentPosition = nextPosition;
    }

    public override void Execute()
    {
        base.Execute();

        timer += Time.deltaTime;

        if(timer >= 3)
        {
            stateMachine.GoToNextState();
        }
    }

    public override void Exit()
    {
        base.Exit();
    }
}