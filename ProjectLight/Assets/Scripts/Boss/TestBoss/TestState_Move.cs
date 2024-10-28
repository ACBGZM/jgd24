using UnityEngine;

[CreateAssetMenu(menuName = "StateMachine/TestBossState/Move", fileName = "TestState_Move")]
public class TestState_Move : TestState
{
    private float m_timer;

    public override void Enter()
    {
        base.Enter();
        Debug.Log("Move");
        m_timer = 0;
    }

    public override void Execute()
    {
        base.Execute();

        if(stateMachine.FoundPlayer())
        {
            if(stateMachine.IsPlayerInRange() && m_timer >= stateMachine.skillCoolTime)
            {
                stateMachine.ChangeState(typeof(TestState_Skill));
            }
        }
        else
        {
            stateMachine.ChangeState(typeof(TestState_Idle));
        }

        m_timer += Time.deltaTime;
    }
}