using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class ChessTrigger : MonoBehaviour
{
    private bool isPlayerNear;
    private static int win;    // review(30.06.2024): bool?
    private static int isPlay; // review(30.06.2024): Почему статичекое поле и почему не bool?
    public GameObject balance;
    public GameObject board;
    public GameObject boardOnLavka;
    public GameObject player;
    public GameObject rightOne;
    public GameObject rightTwo;
    public Button ButtonONE;
    public Button ButtonTWO;
    public AudioSource audioSource;
    public AudioClip buttonClickSound;

    public int gameState; // review(30.06.2024): enum GameState, пусть даже приватный
    public float timeToCount;
    private bool isCounting;

    private void Start()
    {
        ButtonONE.onClick.AddListener(OnButtonClick1);
        ButtonTWO.onClick.AddListener(OnButtonClick2);
        audioSource = gameObject.AddComponent<AudioSource>();
        win = 0;
        isPlay = 0;
        gameState = 0;
        timeToCount = 2f;
        isCounting = false;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && isPlayerNear)
        {
            PlayerController.StartTime = Time.realtimeSinceStartup;
            Fear.FearValue = 0;
        }
        if (!isPlayerNear && isPlay == 0)
        {
            return;
        }
        if (Input.GetKeyDown(KeyCode.E) && win == 0 && isPlay == 0)
        {
            GetComponent<AudioSource>().Play();
            boardOnLavka.SetActive(false);
            player.SetActive(false);
            board.SetActive(true);
            isPlay = 1;
        }
        else if ((Input.GetKeyDown(KeyCode.E) || Input.GetKeyDown(KeyCode.Escape)) && win == 0 && isPlay == 1)
        {
            GetComponent<AudioSource>().Play();
            boardOnLavka.SetActive(true);
            player.SetActive(true);
            board.SetActive(false);
            isPlay = 0;
            PlayerController.StartTime = Time.realtimeSinceStartup;
        }
    }

    private void OnButtonClick1()
    {
        if (isPlay == 1 && gameState == 0)
        {
            rightOne.SetActive(true);
            gameState = 1;
            GetComponent<AudioSource>().Play();
        }
        
    }

    private void OnButtonClick2()
    {
        if (isPlay == 1 && gameState == 1)
        {
            rightTwo.SetActive(true);
            gameState = 2;
            audioSource.PlayOneShot(buttonClickSound);
            win = 1;
            PlayerController.Balance += 1;
            balance.SetActive(true);
            PlayerController.StartTime = Time.realtimeSinceStartup;
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
        boardOnLavka.SetActive(true);
        player.SetActive(true);
        board.SetActive(false);
        isPlay = 0;
    }

    // review(30.06.2024): Тут как будто не хватает проверки на то, что это действительно игрок столкнулся
    private void OnTriggerEnter2D(Collider2D other) => isPlayerNear = true;

    private void OnTriggerExit2D(Collider2D other) => isPlayerNear = false;
}
