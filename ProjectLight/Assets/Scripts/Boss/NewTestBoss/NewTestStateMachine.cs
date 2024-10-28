using System.Collections.Generic;
using UnityEngine;

public class NewTestStateMachine : StateMachine
{
    [Header("Boss属性")]
    public float moveSpeed;

    [Header("状态机参数")]
    public Animator animator;
    public Rigidbody2D rb;
    public List<NewTestState> states;

    public LinkedList<NewTestState> stateList = new LinkedList<NewTestState>();
    public LinkedListNode<NewTestState> nextState;

    void Awake()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();

        foreach (NewTestState state in states)
        {
            state.Init(animator, this);
            stateList.AddLast(state);
        }
        nextState = stateList.First;
    }

    void Start()
    {
        SwitchOn(nextState.Value);
    }

    public void GoToNextState()
    {
        if (nextState.Next == null)
        {
            nextState = stateList.First;
        }
        else
        {
            nextState = nextState.Next;
        }
        ChangeState(nextState.Value);
    }

    public Vector2 GetPlayerPosition()
    {
        return GameObject.FindGameObjectWithTag("Player").transform.position;
    }
}