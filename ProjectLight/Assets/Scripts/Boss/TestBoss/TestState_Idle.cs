using UnityEngine;

[CreateAssetMenu(menuName = "StateMachine/TestBossState/Idle", fileName = "TestState_Idle")]
public class TestState_Idle : TestState
{

    public override void Enter()
    {
        base.Enter();
        Debug.Log("Idle");
    }

    public override void Execute()
    {
        base.Execute();
        Debug.Log("Idle Execute");
        if(stateMachine.FoundPlayer())
        {
            stateMachine.ChangeState(typeof(TestState_Move));
        }
    }

    public override void Exit()
    {
        base.Exit();
        Debug.Log("Idle Exit");
    }

}