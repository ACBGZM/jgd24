using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ServantSpiderState_Dead", menuName = "StateMachine/ServantSpider/Dead")]
public class ServantSpiderState_Dead : ServantSpiderState
{
    public override void Enter()
    {
        base.Enter();
        if (animator == null)
        {
            return;
        }
        animator.Play("ServantSpider_Dead");
        AnimationTool.AwaitCurrentAnimWhenEnd(animator, () => { Destroy(stateMachine.gameObject); });
    }
}
