using UnityEngine;

public class OctopusState_Palsy : OctopusState
{
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
        if (timer >= stateMachine.PalsyTime)
        {
            stateMachine.GoToNextState();
        }
    }
}