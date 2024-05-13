using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LiftController : MonoBehaviour
{
    public Animator Animator;
    // Start is called before the first frame update
    private void Start()
    {
        Animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    private void Update()
    {
        if (Input.GetKey(KeyCode.E))
        {
            Animator.SetBool("IsSwitchOn", true);
        }
    }
}
