using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fear : MonoBehaviour
{

    public static int sharedValue = 0;
    public int saved = 0;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (saved != sharedValue)
        {
            saved = sharedValue;
            if (sharedValue == 1)
            {
                //Camera.main.gameObject.AddComponent<CameraShake>();
                GetComponent<AudioSource>().Play();
                CameraController.sharedValue = 1;
            }
            else if (sharedValue == 0) {
                GetComponent<AudioSource>().Pause();
                CameraController.sharedValue = 0;
            }
        }
       
    }
}
