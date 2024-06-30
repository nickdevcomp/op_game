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
    public AudioSource GoAway;

    public AudioSource ErrorSound;
    private AudioSource casinoSound;

    public int meetingCount;

    public GameObject balance;

    private void Start()
    {
        casinoSound = GetComponent<AudioSource>();
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

        // review(30.06.2024): Зачем каждый раз проверять E? Может, стоило вынести метод типа if (E pressed) HandleCasinoActivating();
        if (Input.GetKeyDown(KeyCode.E) && Inventory.Feather == 0 && meetingCount == 2 && PlayerController.Balance == 1)
        {
            if (!GoAway.isPlaying)
            {
                GoAway.Play();
                return;
            }
            return;
        }
        

        if (Input.GetKeyDown(KeyCode.E) && ((Inventory.Feather == 0 && meetingCount == 2) || PlayerController.Balance == 0))
        {
            // review(30.06.2024): Хорошо бы тут использовать метод AnySoundPlaying()
            if (TicketSound.isPlaying || DKRSound.isPlaying || MorsyankaSound.isPlaying 
                || GoAway.isPlaying || casinoSound.isPlaying) return;
            ErrorSound.Play(); 
            return; 
        }

        if (Input.GetKeyDown(KeyCode.E) && PlayerController.Balance != 0 && meetingCount <= 2)
        {
            // review(30.06.2024): Проверка дублируется
            if (TicketSound.isPlaying || DKRSound.isPlaying || MorsyankaSound.isPlaying 
                || GoAway.isPlaying || casinoSound.isPlaying) return;
            casinoSound.Play();
            PlayerController.StartTime = Time.realtimeSinceStartup;
            Fear.FearValue = 0;
            PlayerController.Balance -= 1;
            StartCoroutine(StartTimer());
        }
    }

    private IEnumerator StartTimer()
    {
        // review(30.06.2024): Эта логика по прежнему дублируется
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
            // review(30.06.2024): Интересно, почему в обратном порядке?
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

        // review(30.06.2024): Я бы изменил этот счетчик на enum CasinoStage, т.к. по замыслу это как будто ближе, да и более понятно, чем какое-то число
        meetingCount += 1;
    }

    private void OnTriggerEnter2D(Collider2D other) => isPlayerNear = true;

    private void OnTriggerExit2D(Collider2D other) => isPlayerNear = false;

}
