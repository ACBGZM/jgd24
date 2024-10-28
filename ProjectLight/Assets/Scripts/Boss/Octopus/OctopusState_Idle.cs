using UnityEngine;

[CreateAssetMenu(fileName = "OctopusState_Idle", menuName = "StateMachine/OctopusState/Idle")]
public class OctopusState_Idle : OctopusState
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
        animator.Play("Octopus_Idle");
    }

    public override void Execute()
    {
        base.Execute();
        timer += Time.deltaTime;
        if (timer >= stateMachine.IdleTime)
        {
            stateMachine.GoToNextState();
        }
    }
}