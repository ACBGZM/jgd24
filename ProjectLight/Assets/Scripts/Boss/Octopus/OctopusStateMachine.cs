using System.Collections.Generic;
using UnityEngine;

public class OctopusStateMachine : StateMachine
{
    [Header("Idle状态参数")]
    [SerializeField
#if UNITY_EDITOR
    , Label("待机时间")
#endif
]
    private float idleTime = 2f;
    public float IdleTime => idleTime;

    [Header("Transport状态参数")]
    [SerializeField
#if UNITY_EDITOR
    , Label("传送点")
#endif
    ]
    private Transform[] tpPoints;
    public Transform[] TpPoints => tpPoints;

    public Transform currentPosition;
    public List<Transform> randomTpPoints = new List<Transform>();

    [Header("Skill状态参数")]
    public List<Jellyfish> jellyfishes;

    [Header("Palsy状态参数")]
    [SerializeField
#if UNITY_EDITOR
    , Label("瘫痪时间")
#endif
    ]
    private float palsyTime = 2f;
    public float PalsyTime => palsyTime;

    [Header("状态机参数")]
    public Animator animator;
    public Rigidbody2D rb;
    public List<OctopusState> states_Phase1;
    public OctopusState palsyState;
    public OctopusState_Dead deadState;
    public bool Phase2;
    public bool canBeDamaged = false;

    public LinkedList<OctopusState> stateList = new LinkedList<OctopusState>();
    public LinkedListNode<OctopusState> nextState;

    void Awake()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        currentPosition = tpPoints[0];
        transform.position = currentPosition.position;
        jellyfishes.Clear();

        palsyState.Init(animator, this);
        deadState.Init(animator, this);
        foreach (OctopusState state in states_Phase1)
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

    public void UpdateRandomPosition()
    {
        randomTpPoints.Clear();
        foreach(Transform transform in tpPoints)
        {
            if(transform != currentPosition)
            {
                randomTpPoints.Add(transform);
            }
        }
    }

    public Transform GetRandomPosition()
    {
        return randomTpPoints[Random.Range(0, randomTpPoints.Count)];
    }

    public void LaserCast(LaserSkill laserSkill)
    {
        if(laserSkill.GetType() == typeof(LaserSkill))
        {
            laserSkill.UpdateOriginalPos(transform.position);
            laserSkill.UpdatePlayerPos(GetPlayerPosition());
            laserSkill.Cast();
        }
        else if (laserSkill.GetType() == typeof(Octopus_RowLaser))
        {
            Octopus_RowLaser rowLaser = (Octopus_RowLaser)laserSkill;
            rowLaser.UpdateOriginalPos(transform.position);
            rowLaser.UpdatePlayerPos(GetPlayerPosition());
            rowLaser.Cast();
        }
        else if (laserSkill.GetType() == typeof(Octopus_FiveLaser_OneShot))
        {
            Octopus_FiveLaser_OneShot oneShot = (Octopus_FiveLaser_OneShot)laserSkill;
            oneShot.UpdateOriginalPos(transform.position);
            oneShot.UpdatePlayerPos(GetPlayerPosition());
            oneShot.Cast();
        }
        else if (laserSkill.GetType() == typeof(Octopus_FiveLaser_ProtectiveLaser))
        {
            Octopus_FiveLaser_ProtectiveLaser protectiveLaser = (Octopus_FiveLaser_ProtectiveLaser)laserSkill;
            protectiveLaser.UpdateOriginalPos(transform.position);
            protectiveLaser.UpdatePlayerPos(GetPlayerPosition());
            protectiveLaser.Cast();
        }
        else if (laserSkill.GetType() == typeof(Octopus_RotateLaser))
        {
            Octopus_RotateLaser rotateLaser = (Octopus_RotateLaser)laserSkill;
            rotateLaser.UpdateOriginalPos(transform.position);
            rotateLaser.UpdatePlayerPos(GetPlayerPosition());
            rotateLaser.Cast();
        }
    }

    public void Transport(OctopusState_Transport state)
    {
        currentPosition = state.nextPosition;
        transform.position = state.nextPosition.position;
        WwiseAudioManager.GetInstance().PostEvent("octopus_teleport", gameObject);
    }

    public void ChangePhase()
    {
        Phase2 = true;
        WwiseAudioManager.GetInstance().PostEvent("octopus_change_phase", gameObject);
    }

    public void Palsy()
    {
        ChangeState(palsyState);
    }

    public void Dead()
    {
        ChangeState(deadState);
        WwiseAudioManager.GetInstance().PostEvent("octopus_change_phase", gameObject);
    }
}
