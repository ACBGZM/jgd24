using UnityEngine;
using System;
using Cysharp.Threading.Tasks;

public class AnimationTool
{
    public static async UniTask AwaitAnimStartByName(Animator animator, Action callback = null, string name = "",
        int layer = 0)
    {
        var animInfo = animator.GetCurrentAnimatorStateInfo(layer);
        var nameHash = Animator.StringToHash(name);
        while (animInfo.shortNameHash != nameHash)
        {
            await UniTask.Yield();
        }

        callback?.Invoke();
    }
    
    public static async UniTask AwaitAnimEndByName(Animator animator, Action callback = null, string name = "",
        int layer = 0)
    {
        var animInfo = animator.GetCurrentAnimatorStateInfo(layer);
        var nameHash = Animator.StringToHash(name);
        if (animInfo.shortNameHash != nameHash)
        {
            await AwaitAnimStartByName(animator, null, name, layer);
        }
        while (animator.GetCurrentAnimatorStateInfo(layer).shortNameHash == nameHash)
        {
            await UniTask.Yield();
        }
        callback?.Invoke();
    }
    
    public static void AwaitCurrentAnimWhenEnd(Animator animator, Action callback, int layer = 0)
    {
        AwaitAnim(animator, () =>
        {
            AwaitAnimEnd(animator, callback, layer);
        }, layer);
    }

    public static async void AwaitAnim(Animator animator, Action callback = null, int layer = 0)
    {
        AwaitAnimAsync(animator, callback, layer).Forget();
    }

    public static async UniTask AwaitAnimAsync(Animator animator, Action callback = null, int layer = 0)
    {
        var animInfo = animator.GetCurrentAnimatorStateInfo(layer);
        var nameHash = animInfo.fullPathHash;

        await UniTask.WaitUntil(() =>
        {
            var info = animator.GetCurrentAnimatorStateInfo(layer);
            return nameHash != info.fullPathHash;
        });

        callback?.Invoke();
    }
    
    public static void AwaitNextAnim(Animator animator, Action callback, int layer = 0)
    {
        AwaitNextAnimAsync(animator, callback, layer).Forget();
    }
    public static async UniTask AwaitNextAnimAsync(Animator animator, Action callback, int layer = 0)
    {
        await AwaitAnimAsync(animator, null, layer);
        await AwaitAnimAsync(animator, callback, layer);
    }
    
    public static void PreAwaitAnimEnd(Animator animator, Action callback, float time, int layer = 0)
    {
        AwaitAnimEndAsync(animator, callback, time, layer).Forget();
    }
    public static void PreAwaitNextAnimEnd(Animator animator, Action callback, float time = 0, int layer = 0)
    {
        AwaitAnimEndAsync(animator, () =>
        {
            AwaitAnimEndAsync(animator, callback, time, layer).Forget();
        }, 0, layer).Forget();
    }
    
    public static void AwaitAnimEnd(Animator animator, Action callback, int layer = 0)
    {
        AwaitAnimEndAsync(animator, callback, layer).Forget();
    }
    
    public static async UniTask AwaitAnimEndAsync(Animator animator, Action callback = null, float time = 0, int layer = 0)
    {
        while (animator != null && (animator.GetCurrentAnimatorStateInfo(layer).normalizedTime < (0.95f - time) || animator.IsInTransition(layer)))
        {
            // 在动画未结束或者在过渡中时继续等待
            await UniTask.Yield();
        }

        if (animator != null)
        {
            callback?.Invoke();
        }
    }
    
    
    
}