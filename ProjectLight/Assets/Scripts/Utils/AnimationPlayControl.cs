using System;
using System.Collections;
using UnityEngine;

public class AnimationPlayControl : MonoBehaviour
{
    private Animator animator;

    public void Init(Animator animator)
    {
        this.animator = animator;
    }

    public void PlayAnimation(Animator animator, string clipName, Action startAct = null, Action endAct = null)
    {
        StartCoroutine(PlayAnimationItor(animator, clipName, startAct, endAct));
    }
    
    public void PlayAnimation(string clipName, Action startAct = null, Action endAct = null)
    {
        StartCoroutine(PlayAnimationItor(this.animator, clipName, startAct, endAct));
    }

    IEnumerator PlayAnimationItor(Animator animator, string clipName, Action startAct, Action endAct)
    {
        startAct?.Invoke();

        animator.Play(clipName);
        AnimatorClipInfo clipInfo = animator.GetCurrentAnimatorClipInfo(0)[0];
        Debug.Log(clipInfo.clip.name);
        Debug.Log("animatorInfo.length: " + clipInfo.clip.length);
        yield return new WaitForSeconds(clipInfo.clip.length);

        endAct?.Invoke();
    }

    IEnumerator PlayAnimationItor(string clipName, Action startAct, Action endAct)
    {
        startAct?.Invoke();
        
        animator.Play(clipName);
        AnimatorClipInfo clipInfo = animator.GetCurrentAnimatorClipInfo(0)[0];
        Debug.Log("clipInfo.clip.length: " + clipInfo.clip.length);
        yield return new WaitForSeconds(clipInfo.clip.length);

        endAct?.Invoke();
    }
}