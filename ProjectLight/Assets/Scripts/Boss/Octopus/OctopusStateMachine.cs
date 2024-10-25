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

    [Header("状态机参数")]
    public Animator animator;
    public Rigidbody2D rb;
    public List<OctopusState> states;

    public LinkedList<OctopusState> stateList = new LinkedList<OctopusState>();
    public LinkedListNode<OctopusState> nextState;

    void Awake()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        currentPosition = tpPoints[0];
        transform.position = currentPosition.position;

        foreach (OctopusState state in states)
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
            laserSkill.UpdateOriginalPos(transform.position);
            rowLaser.UpdatePlayerPos(GetPlayerPosition());
            rowLaser.Cast();
        }
        else if (laserSkill.GetType() == typeof(Octopus_FiveLaser_Phase2))
        {
            Octopus_FiveLaser_Phase2 fiveLaser = (Octopus_FiveLaser_Phase2)laserSkill;
            laserSkill.UpdateOriginalPos(transform.position);
            fiveLaser.UpdatePlayerPos(GetPlayerPosition());
            fiveLaser.Cast();
        }
        
    }
}
