using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class TestStateMachine : StateMachine
{
    public Animator animator;

    public List<Skill> skills;
    public int nextSkillIndex;
    public float skillCoolTime;

    public float viewDistance;
    public LayerMask targetLayer;

    public bool isWeak;

    [SerializeField]
    private TestState[] m_states;


    public float moveSpeed;

    void Awake()
    {
        animator = GetComponent<Animator>();

        stateTable = new Dictionary<System.Type, IState>(m_states.Length);

        nextSkillIndex = 0;

        foreach (TestState state in m_states)
        {
            state.Init(animator, this);
            stateTable.Add(state.GetType(), state);
        }
    }

    void Start()
    {
        SwitchOn(stateTable[typeof(TestState_Idle)]);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, viewDistance);
    }

    public bool FoundPlayer()
    {
        return Physics2D.OverlapCircle(transform.position, viewDistance, targetLayer);
    }

    public bool IsPlayerInRange()
    {
        return true;
    }
}