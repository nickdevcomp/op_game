using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UIElements;
using Image = UnityEngine.UI.Image;

public class Pause : MonoBehaviour
{
    public GameObject Panel;
    public GameObject ContinueButton;
    public GameObject GoToMenuButton;
    public static List<AudioSource> AuidosToContinue = new List<AudioSource>();
    public static bool isPaused;
    

    public void Update()
    {
        if (Panel.activeSelf)
        {
            PlayerController.StartTime = Time.realtimeSinceStartup;
        }
        if (Input.GetKey(KeyCode.Escape) && !Panel.activeSelf)
        {
            isPaused = true;
            foreach (var audio in FindObjectsOfType<AudioSource>())
            {
                if (audio.isPlaying)
                {
                    AuidosToContinue.Add(audio);
                    audio.Pause();
                }
                if (PlayerController.IsPosterActive)
                {
                    foreach (var image in FindObjectsOfType<Image>())
                    {
                        image.GameObject().SetActive(false);
                    }
                    PlayerController.IsPosterActive = false;
                }

                if (PlayerController.isScaryForPause)
                {
                    var canvases = FindObjectsOfType<Canvas>();
                    foreach (var canvas in canvases)
                    {
                        if (canvas.name == "inven")
                            canvas.gameObject.SetActive(false);
                    }
                }
            }
            Panel.SetActive(true);
            ContinueButton.SetActive(true);
            GoToMenuButton.SetActive(true);
            Time.timeScale = 0f;
        }
    }
}
