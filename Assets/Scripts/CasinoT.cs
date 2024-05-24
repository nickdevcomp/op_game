using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CasinoT : MonoBehaviour
{
    public float timeToCount = 2f; // Время таймера в секундах
    private bool isCounting = false; // Флаг, показывающий, запущен ли таймер
    private bool btns = false;

    public int meetingCount;

    public GameObject balance;



    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (btns)
        {
            if (Input.GetKeyDown(KeyCode.E) && PlayerController.balance != 0 && meetingCount == 0)
            {
                PlayerController.balance -= 1;
                balance.SetActive(false);
                GetComponent<AudioSource>().Play();
                StartCoroutine(StartTimer());
            }
            if (Input.GetKeyDown(KeyCode.E) && PlayerController.balance != 0 && meetingCount == 1)
            {
                PlayerController.balance -= 1;
                balance.SetActive(false);
                GetComponent<AudioSource>().Play();
                StartCoroutine(StartTimer());
            }
        }
    }

    IEnumerator StartTimer()
    {
        isCounting = true;
        while (timeToCount > 0) 
        {
            yield return new WaitForSeconds(1f); 
            timeToCount -= 1f; 
            Debug.Log(timeToCount); 
        }
        isCounting = false; 
                        
        timeToCount = 2f;
        if (meetingCount == 1)
            PlayerController.dkr = 1;
        if (meetingCount == 0)
            PlayerController.ticket = 1;
        meetingCount += 1;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        btns = true;
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        btns = false;
    }

}
