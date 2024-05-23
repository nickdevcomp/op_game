using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class GooseT : MonoBehaviour
{
    [FormerlySerializedAs("k")] public int meetingCount = 0;
    public GameObject balance;
    public GameObject feather;
    public GameObject ticket;
    public GameObject dkr;
    public GameObject money;
    public GameObject wool;
    public Image dkrV;
    public Image ticketV;
    public Image imageComponent;

    public float timeToCount = 5f; // Время таймера в секундах
    private bool isCounting = false;

    // Новое изображение для замены
    public Sprite Big;
    public Sprite SVD;

    public bool d = false;
    public bool t = false;

    // Метод для замены изображения
    public void ChangeSourceImage(int t)
    {
        if (t == 1)
        {
            imageComponent.sprite = Big;
            money.SetActive(true);
        }
            
        if (t == 3) 
        {
            imageComponent.sprite = SVD;
            wool.SetActive(true);
            meetingCount += 1;
        }
            
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (PlayerController.dkr == 1)
        {
            PlayerController.dkr = 0;
            dkrV.enabled = true;
            d = true;
            StartCoroutine(FadeInD());
            GetComponent<AudioSource>().Play();
        }
        if (PlayerController.ticket == 1)
        {
            PlayerController.ticket = 0;
            ticketV.enabled = true;
            t = true;
            StartCoroutine(FadeInT());
            GetComponent<AudioSource>().Play();
        }
    }

    IEnumerator FadeInD()
    {
        dkrV.color = new Color(dkrV.color.r, dkrV.color.g, dkrV.color.b, 0);

        while (dkrV.color.a < 1)
        {
            dkrV.color = new Color(dkrV.color.r, dkrV.color.g, dkrV.color.b, dkrV.color.a + Time.deltaTime);
            yield return null;
        }

        yield return new WaitForSeconds(1); // Ждем 2 секунды

        StartCoroutine(FadeOutD());
    }

    IEnumerator FadeOutD()
    {
        while (dkrV.color.a > 0)
        {
            dkrV.color = new Color(dkrV.color.r, dkrV.color.g, dkrV.color.b, dkrV.color.a - Time.deltaTime);
            yield return null;
        }

        dkrV.enabled = false;
        dkr.SetActive(true);
    }

    IEnumerator FadeInT()
    {
        ticketV.color = new Color(ticketV.color.r, ticketV.color.g, ticketV.color.b, 0);

        while (ticketV.color.a < 1)
        {
            ticketV.color = new Color(ticketV.color.r, ticketV.color.g, ticketV.color.b, ticketV.color.a + Time.deltaTime);
            yield return null;
        }

        yield return new WaitForSeconds(1); // Ждем 2 секунды

        StartCoroutine(FadeOutT());
    }

    IEnumerator FadeOutT()
    {
        while (ticketV.color.a > 0)
        {
            ticketV.color = new Color(ticketV.color.r, ticketV.color.g, ticketV.color.b, ticketV.color.a - Time.deltaTime);
            yield return null;
        }

        ticketV.enabled = false;
        ticket.SetActive(true);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (meetingCount == 4)
        {
            wool.SetActive(false);
            feather.SetActive(true);
            GetComponent<AudioSource>().Play();
            meetingCount += 1;
        }

        if (meetingCount == 2 && d)
        {
            d = false;
            dkr.SetActive(false);
            GetComponent<AudioSource>().Play();
            meetingCount += 1;
        }
        
        if (meetingCount == 1)
        {
            money.SetActive(false);
            balance.SetActive(true);
            GetComponent<AudioSource>().Play();
            StartCoroutine(StartTimer());
            meetingCount += 1;
        }
        
        if (meetingCount == 0 && t)
        {
            t = false;
            ticket.SetActive(false);
            GetComponent<AudioSource>().Play();
            meetingCount += 1;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            ChangeSourceImage(meetingCount);
        }
    }

    IEnumerator StartTimer()
    {
        isCounting = true; // Устанавливаем флаг, что таймер запущен
        while (timeToCount > 0) // Пока время не истечет
        {
            yield return new WaitForSeconds(1f); // Ждем одну секунду
            timeToCount -= 1f; // Уменьшаем время на одну секунду
            Debug.Log(timeToCount); // Выводим оставшееся время в консоль
        }
        isCounting = false; // Таймер закончился, сбрасываем флаг
        balance.SetActive(false); // Сообщаем, что время истекло
    }
}
