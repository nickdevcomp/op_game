using System;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UIElements;

public class Pause : MonoBehaviour
{
    public GameObject Panel;
    public GameObject ContinueButton;
    public GameObject GoToMenuButton;

    public void Update()
    {
        if (Panel.activeSelf)
        {
            PlayerController.StartTime = Time.realtimeSinceStartup;
        }
        if (Input.GetKey(KeyCode.Escape) && !Panel.activeSelf)
        {
            Panel.SetActive(true);
            ContinueButton.SetActive(true);
            GoToMenuButton.SetActive(true);
            Time.timeScale = 0f;
        }
    }
}
