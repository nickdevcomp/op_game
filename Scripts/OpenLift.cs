using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenLift : MonoBehaviour
{
    private Animator animator;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        if (Input.GetKey(KeyCode.E))
        {
            animator.SetBool("IsSwitchOn", true);
        }
    }

}
