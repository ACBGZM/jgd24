using UnityEngine;

[CreateAssetMenu(fileName = "SpiderState_Dead", menuName = "StateMachine/Spider/Dead")]
public class SpiderState_Dead : SpiderState
{
    public override void Enter()
    {
        base.Enter();
        animator.Play("Spider_Dead");
        AnimationTool.AwaitCurrentAnimWhenEnd(animator, () => { stateMachine.GetComponent<BossOnHit>().GameOver(true); });
    }
}