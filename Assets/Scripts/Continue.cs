using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Continue : MonoBehaviour
{    
    public GameObject Panel;
    public GameObject ContinueButton;
    public GameObject GoToMenuButton;

    public void Update()
    {
        if (PlayerController.isScaryForPause)
        {
            var canvases = FindObjectsOfType(typeof(Canvas), true);
            foreach (var canvas in canvases)
            {
                if (canvas.name == "inven")
                    canvas.GameObject().SetActive(true);
            }
        }
        Pause.isPaused = false;
        var audios = Pause.AuidosToContinue;
        foreach (var audio in audios)
        {
            audio.Play();
            audios.Remove(audio);
        }
        Time.timeScale = 1f;
        Panel.SetActive(false);
        ContinueButton.SetActive(false);
        GoToMenuButton.SetActive(false);
        this.GameObject().SetActive(false);
    }
}
