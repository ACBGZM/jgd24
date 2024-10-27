using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.PlayerLoop;

public enum SpiderStateType
{
    Idle,
    Move,
    Skill,
    ChangingPhase
}

public class SpiderStateMachine : StateMachine
{
    [Header("Boss属性")]
#if UNITY_EDITOR
    [Label("可以被伤害")]
#endif

    public bool canBeDamaged = true;
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

    public Vector2 rangeX;
    public Vector2 rangeY;

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
    public SpiderState spiderState_ChangingPhase;
    public SpiderState spiderState_Dead;

    public LinkedList<SpiderState> stateList = new LinkedList<SpiderState>();
    public LinkedListNode<SpiderState> nextState;

    private SpiderStateType currentStateType;
    public SpiderStateType CurrentStateType => currentStateType;

    public HealthManager healthManager;

    void Awake()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        currentServantAmonut = 0;

        spiderState_ChangingPhase.Init(animator, this);
        spiderState_Dead.Init(animator, this);

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

    public void BulletCast(BulletSkill bulletSkill)
    {
        WwiseAudioManager.GetInstance().PostEvent("spider_shoot", gameObject);

        bulletSkill.UpdatePlayerPos(GetPlayerPosition());
        bulletSkill.Cast();
    }

    public void ChangePhase()
    {
        ChangeState(spiderState_ChangingPhase);
    }

    public void CloseLight()
    {
        healthManager.SwitchLight(false);
    }

    public void Dead()
    {
        ChangeState(spiderState_Dead);
    }

    public void PlayWalkAudio()
    {
        WwiseAudioManager.GetInstance().PostEvent("spider_walk", gameObject);
    }

    public void PlayIncubateAudio()
    {
        WwiseAudioManager.GetInstance().PostEvent("spider_incubate", gameObject);
    }
    public void PlayShootAudio()
    {
        WwiseAudioManager.GetInstance().PostEvent("spider_shoot", gameObject);
    }
}