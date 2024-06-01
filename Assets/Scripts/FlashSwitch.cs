using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class FlashSwitch : MonoBehaviour
{
    public static Light flashLight;
    private readonly KeyCode flashLightKeyCode = KeyCode.F;
    public AudioClip FlashLightSwitchSound;

    private void Start() => flashLight = GetComponent<Light>();

    private void Update()
    {
        if (!Input.GetKeyUp(flashLightKeyCode)) 
            return;
        flashLight.enabled = !flashLight.enabled;
        GetComponent<AudioSource>().PlayOneShot(FlashLightSwitchSound);
    }
}
