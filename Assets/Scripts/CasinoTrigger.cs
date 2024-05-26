using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CasinoTrigger : MonoBehaviour
{
    public float timeToCount = 2f;
    private bool isCounting;
    private bool isPlayerNear;

    public int meetingCount;

    public GameObject balance;

    public void Update()
    {
        if (!isPlayerNear) 
            return;
        if (Input.GetKeyDown(KeyCode.E) && PlayerController.Balance != 0 && meetingCount <= 1)
        {
            PlayerController.Balance -= 1;
            balance.SetActive(false);
            GetComponent<AudioSource>().Play();
            StartCoroutine(StartTimer());
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
            PlayerController.Dkr = 1;
        if (meetingCount == 0)
            PlayerController.Ticket = 1;
        meetingCount += 1;
    }

    private void OnTriggerEnter2D(Collider2D other) => isPlayerNear = true;

    private void OnTriggerExit2D(Collider2D other) => isPlayerNear = false;

}
