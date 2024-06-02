using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class Fear : MonoBehaviour
{

    public static int FearValue;
    public int Saved;
    public AudioSource DeathSound;
    public AudioSource DontTurnAround;
    
    private void Start()
    {
        FearValue = 0;
        Saved = 0;
    }

    void Update()
    {
        if (Saved == FearValue) 
            return;
        Saved = FearValue;
        switch (FearValue)
        {
            case 1:
                DeathSound.Play();
                DontTurnAround.Play();
                GetComponent<AudioSource>().Play();
                CameraController.FearValue = 1;
                break;
            case 0:
                GetComponent<AudioSource>().Pause();
                DontTurnAround.Stop();
                DeathSound.Stop();
                CameraController.FearValue = 0;
                break;
        }
    }
}
