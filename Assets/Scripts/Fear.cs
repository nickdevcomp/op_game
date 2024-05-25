using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fear : MonoBehaviour
{

    public static int FearValue = 0;
    public int saved;

    void Update()
    {
        if (saved != FearValue)
        {
            saved = FearValue;
            if (FearValue == 1)
            {
                //Camera.main.gameObject.AddComponent<CameraShake>();
                GetComponent<AudioSource>().Play();
                CameraController.FearValue = 1;
            }
            else if (FearValue == 0) {
                GetComponent<AudioSource>().Pause();
                CameraController.FearValue = 0;
            }
        }
       
    }
}
