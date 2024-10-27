using UnityEngine;

[CreateAssetMenu(menuName = "StateMachine/OctopusState/Palsy", fileName = "OctopusState_Palsy")]
public class OctopusState_Palsy : OctopusState
{
    [SerializeField
#if UNITY_EDITOR
        , ReadOnly
#endif
    ]
    private float timer = 0;
    public float Timer => timer;

    private bool timerStart = false;

    public override void Enter()
    {
        base.Enter();
        timerStart = false;
        animator.Play("Octopus_Idle2Palsy");
        AnimationTool.AwaitCurrentAnimWhenEnd(animator, () =>
        {
            timerStart = true;
            animator.Play("Octopus_Palsy");
            AnimationTool.AwaitCurrentAnimWhenEnd(animator, () =>
            {
                animator.Play("Octopus_Palsy2Idle");
            });
        });
    }

    public override void Execute()
    {
        base.Execute();
    }
}