using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlashSW : MonoBehaviour
{

    Light FL;
    public KeyCode FLC;
    public AudioClip FLS;

    void Start()
    {
        FL = GetComponent<Light>();
    }

    void Update()
    {
        if (Input.GetKeyUp(FLC))
        {
            FL.enabled = ! FL.enabled;
            GetComponent<AudioSource>().PlayOneShot(FLS);
        }
    }
}
