using System;
using UnityEngine;

public class PlayerAnimationController : MonoBehaviour
{
    public PlayerStatus PlayerStatus { get; set; }

    private Animator m_animator = null;

    private Action m_reset_hit_action = null;

    private void Start()
    {
        m_animator = GetComponent<Animator>();
    }

    void Update()
    {
        m_animator.SetBool("IsMoving", PlayerStatus == PlayerStatus.moving);
        m_animator.SetBool("IsPlanting", PlayerStatus == PlayerStatus.planting_a_bomb);

    }

    public void SetResetHitAction(Action action)
    {
        m_reset_hit_action = action;
    }

    public void ResetHit()
    {
        m_animator.ResetTrigger("Hit");
        m_reset_hit_action?.Invoke();
    }

    public AnimatorStateInfo GetCurrentAnimationStatus()
    {
        return m_animator.GetCurrentAnimatorStateInfo(0);
    }

}
