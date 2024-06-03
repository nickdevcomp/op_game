using System.Collections;
using UnityEngine;

public class CasinoTrigger : MonoBehaviour
{
    public float timeToCount;
    private bool isCounting;
    private bool isPlayerNear;

    public AudioSource TicketSound;
    public AudioSource DKRSound;
    public AudioSource MorsyankaSound;

    public AudioSource ErrorSound;

    public int meetingCount;

    public GameObject balance;

    private void Start()
    {
        timeToCount = 2f;
        isCounting = false;
        isPlayerNear = false;
    }

    private void Update()
    {
        if (!isPlayerNear)
        {
            return;
        }

        if (Input.GetKeyDown(KeyCode.E) && ((Inventory.Feather == 0 && meetingCount == 2) || PlayerController.Balance == 0))
        {
            ErrorSound.Play(); 
            return; 
        }

        if (Input.GetKeyDown(KeyCode.E) && PlayerController.Balance != 0 && meetingCount <= 2)
        {
            PlayerController.StartTime = Time.realtimeSinceStartup;
            Fear.FearValue = 0;

            PlayerController.Balance -= 1;
            GetComponent<AudioSource>().Play();
            StartCoroutine(StartTimer());
        }
    }

    private IEnumerator StartTimer()
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
        switch (meetingCount)
        {
            case 2:
                Inventory.Morsynka = 1;
                MorsyankaSound.Play();
                break;
            case 1:
                Inventory.Dkr = 1;
                DKRSound.Play();
                break;
            case 0:
                Inventory.Ticket = 1;
                TicketSound.Play();
                break;
        }

        meetingCount += 1;
    }

    private void OnTriggerEnter2D(Collider2D other) => isPlayerNear = true;

    private void OnTriggerExit2D(Collider2D other) => isPlayerNear = false;

}
