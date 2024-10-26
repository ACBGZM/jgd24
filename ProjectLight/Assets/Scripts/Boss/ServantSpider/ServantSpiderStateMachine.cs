using System.Collections.Generic;
using UnityEngine;

public class ServantSpiderStateMachine : StateMachine
{
    [Header("Boss属性")]
#if UNITY_EDITOR
    [Label("可以被伤害")]
#endif
    public bool canBeDamaged = true;
    [Header("Cocoon状态参数")]
    [SerializeField 
#if UNITY_EDITOR 
    ,Label("孵化时间") 
#endif
    ]
    private float cocoonTime;
    public float CocoonTime => cocoonTime;

    [Header("Idle状态参数")]

    [SerializeField 
#if UNITY_EDITOR 
    ,Label("待机时间") 
#endif
    ]
    private float idleTime;
    public float IdleTime => idleTime;
    [SerializeField 
#if UNITY_EDITOR 
    ,Label("追击距离") 
#endif
    ]
    private float chaseDistance;
    public float ChaseDistance => chaseDistance;

    [Header("Chase状态参数")]
    [SerializeField 
#if UNITY_EDITOR 
    ,Label("追击速度") 
#endif
    ]
    private float moveSpeed;
    public float MoveSpeed => moveSpeed;
    [SerializeField 
#if UNITY_EDITOR 
    ,Label("追击时间") 
#endif
    ]
    private float chaseTime;
    public float ChaseTime => chaseTime;
    [SerializeField 
#if UNITY_EDITOR 
    ,Label("最大追击距离") 
#endif
    ]
    private float maxChaseDistance;
    public float MaxChaseDistance => maxChaseDistance;

    [Header("Hanging状态参数")]
    [SerializeField
#if UNITY_EDITOR
    , Label("攻击半径")
#endif
    ]
    private float attackRadius;
    public float AttackRadius => attackRadius;
    

    [Header("状态机参数")]
    public Animator animator;
    public Rigidbody2D rb;
    public SpriteRenderer sr;

    [SerializeField]
    private List<ServantSpiderState> states;

    public List<ServantSpiderState> AvailableStates;

    void Awake()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        stateTable = new Dictionary<System.Type, IState>(states.Count);

        foreach (ServantSpiderState state in states)
        {
            state.Init(animator, this);
            if(state.GetType() == typeof(ServantSpiderState_Egg))
            {
                ServantSpiderState_Egg newState = ScriptableObject.CreateInstance<ServantSpiderState_Egg>();
                newState.Init((ServantSpiderState_Egg)state);
                stateTable.Add(state.GetType(), newState);
                AvailableStates.Add(newState);
            }
            else if(state.GetType() == typeof(ServantSpiderState_Idle))
            {
                ServantSpiderState_Idle newState = ScriptableObject.CreateInstance<ServantSpiderState_Idle>();
                newState.Init((ServantSpiderState_Idle)state);
                stateTable.Add(state.GetType(), newState);
                AvailableStates.Add(newState);
            }
            else if (state.GetType() == typeof(ServantSpiderState_Chase))
            {
                ServantSpiderState_Chase newState = ScriptableObject.CreateInstance<ServantSpiderState_Chase>();
                newState.Init((ServantSpiderState_Chase)state);
                stateTable.Add(state.GetType(), newState);
                AvailableStates.Add(newState);
            }
            else if (state.GetType() == typeof(ServantSpiderState_Hanging))
            {
                ServantSpiderState_Hanging newState = ScriptableObject.CreateInstance<ServantSpiderState_Hanging>();
                newState.Init((ServantSpiderState_Hanging)state);
                stateTable.Add(state.GetType(), newState);
                AvailableStates.Add(newState);
            }
        }
    }

    void Start()
    {
        SwitchOn(stateTable[typeof(ServantSpiderState_Egg)]);
    }

    public Vector2 GetPlayerPosition()
    {
        return GameObject.FindGameObjectWithTag("Player").transform.position;
    }

    public void PlayServantSpiderWalkAudio()
    {
        WwiseAudioManager.GetInstance().PostEvent("servant_spider_walk", gameObject);
    }

    public void PlayServantSpiderFallAudio()
    {
        WwiseAudioManager.GetInstance().PostEvent("servant_spider_fall", gameObject);
    }

    public void PlayServantSpiderIncubateAudio()
    {
        WwiseAudioManager.GetInstance().PostEvent("servant_spider_incubate", gameObject);
    }
}
