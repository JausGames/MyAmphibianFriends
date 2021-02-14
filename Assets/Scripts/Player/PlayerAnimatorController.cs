using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimatorController : MonoBehaviour
{
    [SerializeField] Animator animator;

    private void Awake()
    {
        animator = GetComponentInChildren<Animator>();
    }

    public void StartCharge()
    {
        animator.SetTrigger("Charge");
    }
    public void StartJump()
    {
        animator.SetTrigger("Jump");
    }

}
