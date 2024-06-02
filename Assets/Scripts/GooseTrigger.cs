using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class GooseTrigger : MonoBehaviour
{
    public int meetingCount;
    public Image balance;
    public Image feather;
    public Image ticket;
    public Image dkr;
    public GameObject money;
    public GameObject wool;
    public Image dkrV;
    public Image ticketV;
    public Image MorsynkaV;
    public Image imageComponent;

    public AudioSource GooseSound1;
    public AudioSource GooseSound2;
    public AudioSource GooseSound3;

    public float timeToCount = 5f;
    private bool isCounting;

    // Новое изображение для замены
    public Sprite Big;
    public Sprite SVD;

    public bool IsDKRInInventory;
    public bool IsTicketInInventory;

    // Метод для замены изображения
    private void ChangeSourceImage(int meetingCount)
    {
        switch (meetingCount)
        {
            case 1:
                imageComponent.sprite = Big;
                money.SetActive(true);
                break;
            case 3:
                imageComponent.sprite = SVD;
                wool.SetActive(true);
                this.meetingCount += 1;
                break;
        }
    }

    private void Start()
    {
        meetingCount = 0;

        Color color = feather.color;
        color.a = 0f;
        feather.color = color;

        color = ticket.color;
        color.a = 0f;
        ticket.color = color;

        color = dkr.color;
        color.a = 0f;
        dkr.color = color;
    }


    private void Update()
    {
        if (Inventory.Dkr == 1)
        {
            Inventory.Dkr = 0;
            dkrV.enabled = true;
            IsDKRInInventory = true;
            StartCoroutine(FadeInD());
            GetComponent<AudioSource>().Play();
            
        }
        if (Inventory.Ticket == 1)
        {
            Inventory.Ticket = 0;
            ticketV.enabled = true;
            IsTicketInInventory = true;
            StartCoroutine(FadeInT());
            GetComponent<AudioSource>().Play();
        }
        if (Inventory.Morsynka == 1)
        {
            Inventory.Morsynka = 0;
            MorsynkaV.enabled = true;
            StartCoroutine(FadeInM());
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

        yield return new WaitForSeconds(1);

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
        Color color = dkr.color;
        color.a = 1f;
        dkr.color = color;
    }

    IEnumerator FadeInT()
    {
        ticketV.color = new Color(ticketV.color.r, ticketV.color.g, ticketV.color.b, 0);

        while (ticketV.color.a < 1)
        {
            ticketV.color = new Color(ticketV.color.r, ticketV.color.g, ticketV.color.b, ticketV.color.a + Time.deltaTime);
            yield return null;
        }

        yield return new WaitForSeconds(1);

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
        Color color = ticket.color;
        color.a = 1f;
        ticket.color = color;
    }

    IEnumerator FadeInM()
    {
        MorsynkaV.color = new Color(MorsynkaV.color.r, MorsynkaV.color.g, MorsynkaV.color.b, 0);

        while (MorsynkaV.color.a < 1)
        {
            MorsynkaV.color = new Color(MorsynkaV.color.r, MorsynkaV.color.g, MorsynkaV.color.b, MorsynkaV.color.a + Time.deltaTime);
            yield return null;
        }

        yield return new WaitForSeconds(1);

        StartCoroutine(FadeOutM());
    }

    IEnumerator FadeOutM()
    {
        while (MorsynkaV.color.a > 0)
        {
            MorsynkaV.color = new Color(MorsynkaV.color.r, MorsynkaV.color.g, MorsynkaV.color.b, MorsynkaV.color.a - Time.deltaTime);
            yield return null;
        }

        MorsynkaV.enabled = false;
        MorsyankaTrigger.IsPlay = true;
        Color color = feather.color;
        color.a = 1f;
        feather.color = color;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (meetingCount == 4)
        {
            wool.SetActive(false);
            Color color = feather.color;
            color.a = 1f;
            feather.color = color;
            //GetComponent<AudioSource>().Play();
            GooseSound3.Play();
            meetingCount += 1;
            Inventory.Feather = 1;
        }

        if (meetingCount == 2 && IsDKRInInventory)
        {
            IsDKRInInventory = false;
            Color color = dkr.color;
            color.a = 0f;
            dkr.color = color;
            //GetComponent<AudioSource>().Play();
            GooseSound2.Play();
            meetingCount += 1;
        }
        
        if (meetingCount == 1)
        {
            money.SetActive(false);
            GetComponent<AudioSource>().Play();
            PlayerController.Balance += 1;
            meetingCount += 1;
        }
        
        if (meetingCount == 0 && IsTicketInInventory)
        {
            IsTicketInInventory = false;
            Color color = ticket.color;
            color.a = 0f;
            ticket.color = color;
            //GetComponent<AudioSource>().Play(); 
            GooseSound1.Play();
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
}
