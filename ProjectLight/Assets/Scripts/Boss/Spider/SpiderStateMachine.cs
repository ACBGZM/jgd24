using System.Collections.Generic;
using UnityEngine;

public class SpiderStateMachine : StateMachine
{
    [Header("Boss属性")]
    public float moveSpeed;
    public int maxServantAmount;

    [ReadOnly]
    public int currentServantAmonut;
    
    [Header("状态机参数")]
    public Animator animator;
    public Rigidbody2D rb;
    public List<SpiderState> states;

    public LinkedList<SpiderState> stateList = new LinkedList<SpiderState>();
    public LinkedListNode<SpiderState> nextState;

    void Awake()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        currentServantAmonut = 0;

        foreach (SpiderState state in states)
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