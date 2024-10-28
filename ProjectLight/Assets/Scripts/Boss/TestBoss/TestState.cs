using UnityEngine;

public class TestState : ScriptableObject, IState
{
    protected Animator animator;
    protected TestStateMachine stateMachine;

    public void Init(Animator animator, TestStateMachine stateMachine)
    {
        this.animator = animator;
        this.stateMachine = stateMachine;
    }

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
}