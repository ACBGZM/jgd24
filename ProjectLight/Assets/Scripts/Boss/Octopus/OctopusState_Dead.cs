using UnityEngine;

[CreateAssetMenu(fileName = "OctopusState_Dead", menuName = "StateMachine/Octopus/Dead")]
public class OctopusState_Dead : OctopusState
{
    public override void Enter()
    {
        base.Enter();
        animator.Play("Octopus_Dead");
        AnimationTool.AwaitCurrentAnimWhenEnd(animator, () => { stateMachine.GetComponent<BossOnHit>().GameOver(true); });
    }
}