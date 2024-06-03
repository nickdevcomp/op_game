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
    public Image dkrView;
    public Image ticketView;
    public Image MorsynkaView;
    public Image imageComponent;

    public AudioSource GooseSound1;
    public AudioSource GooseSound2;
    public AudioSource GooseSound3;

    public float timeToCount = 5f;
    private bool isCounting;
    private bool isNear;

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
        ShowItem();
        
        if (!isNear || !Input.GetKeyDown(KeyCode.E))
            return;

        if (!IsDKRInInventory && !IsTicketInInventory)
        {
            GooseSound3.Play();
            return;
        }

        if (meetingCount == 2 && IsDKRInInventory)
        {
            IsDKRInInventory = false;
            var color = dkr.color;
            color.a = 0f;
            dkr.color = color;
            GooseSound2.Play();
            meetingCount += 1;
        }
        
        if (meetingCount == 0 && IsTicketInInventory)
        {
            IsTicketInInventory = false;
            var color = ticket.color;
            color.a = 0f;
            ticket.color = color;
            GooseSound1.Play();
            meetingCount += 1;
        }
    }

    private void ShowItem()
    {
        if (Inventory.Dkr == 1)
        {
            Inventory.Dkr = 0;
            dkrView.enabled = true;
            IsDKRInInventory = true;
            StartCoroutine(FadeInDKR());
            GetComponent<AudioSource>().Play();
        }

        if (Inventory.Ticket == 1)
        {
            Inventory.Ticket = 0;
            ticketView.enabled = true;
            IsTicketInInventory = true;
            StartCoroutine(FadeInTicket());
            GetComponent<AudioSource>().Play();
        }

        if (Inventory.Morsynka == 1)
        {
            Inventory.Morsynka = 0;
            MorsynkaView.enabled = true;
            StartCoroutine(FadeInMorsyanka());
            GetComponent<AudioSource>().Play();
        }
    }

    IEnumerator FadeInDKR()
    {
        dkrView.color = new Color(dkrView.color.r, dkrView.color.g, dkrView.color.b, 0);

        while (dkrView.color.a < 1)
        {
            dkrView.color = new Color(dkrView.color.r, dkrView.color.g, dkrView.color.b, dkrView.color.a + Time.deltaTime);
            yield return null;
        }

        yield return new WaitForSeconds(1);

        StartCoroutine(FadeOutDKR());
    }

    IEnumerator FadeOutDKR()
    {
        while (dkrView.color.a > 0)
        {
            dkrView.color = new Color(dkrView.color.r, dkrView.color.g, dkrView.color.b, dkrView.color.a - Time.deltaTime);
            yield return null;
        }

        dkrView.enabled = false;
        Color color = dkr.color;
        color.a = 1f;
        dkr.color = color;
    }

    IEnumerator FadeInTicket()
    {
        ticketView.color = new Color(ticketView.color.r, ticketView.color.g, ticketView.color.b, 0);
                while (ticketView.color.a < 1)
        {
            ticketView.color = new Color(ticketView.color.r, ticketView.color.g, ticketView.color.b, ticketView.color.a + Time.deltaTime);
            yield return null;
        }

        yield return new WaitForSeconds(1);

        StartCoroutine(FadeOutTicket());
    }

    IEnumerator FadeOutTicket()
    {
        while (ticketView.color.a > 0)
        {
            ticketView.color = new Color(ticketView.color.r, ticketView.color.g, ticketView.color.b, ticketView.color.a - Time.deltaTime);
            yield return null;
        }

        ticketView.enabled = false;
        Color color = ticket.color;
        color.a = 1f;
        ticket.color = color;
    }

    IEnumerator FadeInMorsyanka()
    {
        MorsynkaView.color = new Color(MorsynkaView.color.r, MorsynkaView.color.g, MorsynkaView.color.b, 0);

        while (MorsynkaView.color.a < 1)
        {
            MorsynkaView.color = new Color(MorsynkaView.color.r, MorsynkaView.color.g, MorsynkaView.color.b, MorsynkaView.color.a + Time.deltaTime);
            yield return null;
        }

        yield return new WaitForSeconds(1);

        StartCoroutine(FadeOutMorsyanka());
    }

    IEnumerator FadeOutMorsyanka()
    {
        while (MorsynkaView.color.a > 0)
        {
            MorsynkaView.color = new Color(MorsynkaView.color.r, MorsynkaView.color.g, MorsynkaView.color.b, MorsynkaView.color.a - Time.deltaTime);
            yield return null;
        }

        MorsynkaView.enabled = false;
        MorsyankaTrigger.IsPlay = true;
        var color = feather.color;
        color.a = 1f;
        feather.color = color;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        isNear = true;
        if (meetingCount == 1)
        {
            money.SetActive(false);
            GetComponent<AudioSource>().Play();
            PlayerController.Balance += 1;
            meetingCount += 1;
        }

        if (meetingCount == 4)
        {
            wool.SetActive(false);
            var color = feather.color;
            color.a = 1f;
            feather.color = color;
            GooseSound3.Play();
            meetingCount += 1;
            Inventory.Feather = 1;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        isNear = false;
        if (other.CompareTag("Player"))
        {
            ChangeSourceImage(meetingCount);
        }
    }
}