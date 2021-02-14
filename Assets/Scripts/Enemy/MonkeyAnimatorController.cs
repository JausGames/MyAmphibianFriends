using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonkeyAnimatorController : MonoBehaviour
{
    [SerializeField] Animator animator;
    private void Awake()
    {
        animator = GetComponentInChildren<Animator>();
    }

    public void SetWalk(bool value)
    {
        animator.SetBool("Walk", value);
    }
    public void SetAttack(bool value)
    {
        animator.SetBool("Attack", value);
    }
}
