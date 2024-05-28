using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class ChessTrigger : MonoBehaviour
{
    private bool isPlayerNear;
    public static int win;
    public static int isPlay;
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

    public int gameState = 0;
    public float timeToCount = 2f;
    private bool isCounting;


    // Start is called before the first frame update
    void Start()
    {
        ButtonONE.onClick.AddListener(OnButtonClick1);
        ButtonTWO.onClick.AddListener(OnButtonClick2);
        audioSource = gameObject.AddComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if ((!isPlayerNear && isPlay == 0) || PlayerController.Feather == 0)
            return;
        if (Input.GetKeyDown(KeyCode.E) && win == 0 && isPlay == 0)
        {
            GetComponent<AudioSource>().Play();
            boardOnLavka.SetActive(false);
            player.SetActive(false);
            board.SetActive(true);
            isPlay = 1;
        }
        else if (Input.GetKeyDown(KeyCode.E) && win == 0 && isPlay == 1)
        {
            GetComponent<AudioSource>().Play();
            boardOnLavka.SetActive(true);
            player.SetActive(true);
            board.SetActive(false);
            isPlay = 0;
        }
    }

    public void OnButtonClick1()
    {
        if (isPlay == 1 && gameState == 0)
        {
            rightOne.SetActive(true);
            gameState = 1;
            GetComponent<AudioSource>().Play();
        }
        
    }

    public void OnButtonClick2()
    {
        if (isPlay == 1 && gameState == 1)
        {
            rightTwo.SetActive(true);
            gameState = 2;
            audioSource.PlayOneShot(buttonClickSound);
            win = 1;
            PlayerController.Balance = 1;
            balance.SetActive(true);
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

    private void OnTriggerEnter2D(Collider2D other) => isPlayerNear = true;

    private void OnTriggerExit2D(Collider2D other) => isPlayerNear = false;
}
