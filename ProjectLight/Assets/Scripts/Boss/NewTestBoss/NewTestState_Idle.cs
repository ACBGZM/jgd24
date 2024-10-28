using UnityEngine;

[CreateAssetMenu(menuName = "StateMachine/NewTestBossState/Idle", fileName = "NewTestState_Idle")]
public class NewTestState_Idle : NewTestState
{
    [SerializeField]
    private float idleTime = 0;
    public float IdleTime => idleTime;

    [SerializeField
#if UNITY_EDITOR
        , ReadOnly
#endif
        ]
    private float timer = 0;
    public float Timer => timer;

    public override void Enter()
    {
        base.Enter();
        timer = 0;
    }

    public override void Execute()
    {
        base.Execute();
        timer += Time.deltaTime;

        if(timer >= IdleTime)
        {
            
            stateMachine.GoToNextState();
        }
    }
}