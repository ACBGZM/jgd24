using UnityEngine;

public class SpiderState : ScriptableObject, IState
{
    protected Animator animator;
    protected SpiderStateMachine stateMachine;

    public virtual void Enter()
    {
        
    }

    public virtual void Execute()
    {
    }

    public virtual void FixedExecute()
    {
        
    }

    public virtual void Exit()
    {
    }

    public void Init(Animator animator, SpiderStateMachine stateMachine)
    {
        this.animator = animator;
        this.stateMachine = stateMachine;
    }
}