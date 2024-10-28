using UnityEngine;

public class OctopusState : ScriptableObject, IState
{
    protected Animator animator;
    protected OctopusStateMachine stateMachine;

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

    public void Init(Animator animator, OctopusStateMachine stateMachine)
    {
        this.animator = animator;
        this.stateMachine = stateMachine;
    }
}