using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fear : MonoBehaviour
{
    // review(24.05.2024): Публичное статическое изменяемое поле, хм... Можем ли мы хотя бы методами его закрыть? 
    // review(24.05.2024): Ну и название непонятноеы
    public static int sharedValue = 0;
    public int saved = 0; // review(24.05.2024): Почему оно публичное? Может, это previousFear?
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
                CameraController.sharedValue = 1; // review(24.05.2024): Может, лучше в CameraController обращаться к Fear.sharedValue, чтобы был только один источник правды?
            }
            else if (sharedValue == 0) {
                GetComponent<AudioSource>().Pause();
                CameraController.sharedValue = 0;
            }
        }
       
    }
}
