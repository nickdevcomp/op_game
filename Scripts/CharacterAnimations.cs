using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterAnimations : MonoBehaviour
{
    private Animator animator;
    public bool IsWalking { private get; set; }
    public bool IsRunning { private get; set; }

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    private void FixedUpdate()
    {
        animator.SetBool("isWalking", IsWalking);
        animator.SetBool("isRunning", IsRunning);
    }
}
