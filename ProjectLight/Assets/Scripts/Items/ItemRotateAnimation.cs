
using UnityEngine;

public class ItemRotateAnimation : MonoBehaviour
{
  
    private Animator animator;

    void Start()
    {
        animator = GetComponent<Animator>();
        animator.SetTrigger("InRotate");
    }
}