using UnityEngine;

public class NewTestState : ScriptableObject, IState
{
    protected Animator animator;
    protected NewTestStateMachine stateMachine;

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

    public void Init(Animator animator, NewTestStateMachine stateMachine)
    {
        this.animator = animator;
        this.stateMachine = stateMachine;
    }
}