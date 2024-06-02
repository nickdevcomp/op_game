using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class Fear : MonoBehaviour
{

    public static int FearValue;
    public int Saved;
    
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
                GetComponent<AudioSource>().Play();
                CameraController.FearValue = 1;
                break;
            case 0:
                GetComponent<AudioSource>().Pause();
                CameraController.FearValue = 0;
                break;
        }
    }
}
