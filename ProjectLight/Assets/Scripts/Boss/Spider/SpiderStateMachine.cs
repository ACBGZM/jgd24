using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.PlayerLoop;

public enum SpiderStateType
{
    Idle,
    Move,
    Skill
}

public class SpiderStateMachine : StateMachine
{
    [Header("Boss属性")]
    [Header("Idle状态参数")]
    [SerializeField
#if UNITY_EDITOR
    , Label("待机时间")
#endif
    ]
    private float idleTime = 0;
    public float IdleTime => idleTime;

    [Header("Move状态参数")]
    [SerializeField
#if UNITY_EDITOR
    , Label("移动速度")
#endif
    ]
    private float moveSpeed = 0;
    public float MoveSpeed => moveSpeed;
    [SerializeField
#if UNITY_EDITOR
    , Label("追击范围外径")
#endif
    ]
    private float targetAreaExternalRadius = 0;
    public float TargetAreaExternalRadius => targetAreaExternalRadius;
    [SerializeField
#if UNITY_EDITOR
    , Label("追击范围内径")
#endif
    ]
    private float targetAreaInnerRadius = 0;
    public float TargetAreaInnerRadius => targetAreaInnerRadius;
    [SerializeField
#if UNITY_EDITOR
    , Label("最小追击角度")
#endif
    ]
    private float minTargetAngle;
    public float MinTargetAngle => minTargetAngle;
    [SerializeField
#if UNITY_EDITOR
    , Label("最大追击角度")
#endif
    ]
    private float maxTargetAngle;
    public float MaxTargetAngle => maxTargetAngle;

    [Header("随从参数")]
    [SerializeField
#if UNITY_EDITOR
    , Label("最大随从数量")
#endif
    ]
    public int maxServantAmount;

#if UNITY_EDITOR
    [ReadOnly, Label("当前随从数量")]
#endif
    public int currentServantAmonut;
    
    [Header("状态机参数")]
    public Animator animator;
    public Rigidbody2D rb;
    public List<SpiderState> states;

    public LinkedList<SpiderState> stateList = new LinkedList<SpiderState>();
    public LinkedListNode<SpiderState> nextState;

    private SpiderStateType currentStateType;
    public SpiderStateType CurrentStateType => currentStateType;

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

    public void UpdateServantAmount()
    {
        ServantSpiderStateMachine[] servants = FindObjectsByType<ServantSpiderStateMachine>(FindObjectsSortMode.None);

        currentServantAmonut = servants.Length;
    }

    public void SetCurrentStateType(SpiderStateType type)
    {
        currentStateType = type;
    }
}