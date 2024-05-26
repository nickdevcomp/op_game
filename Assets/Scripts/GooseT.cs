using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class GooseT : MonoBehaviour
{
    public int meetingCount;
    public GameObject balance;
    public GameObject feather;
    public GameObject ticket;
    public GameObject dkr;
    public GameObject money;
    public GameObject wool;
    public Image dkrV;
    public Image ticketV;
    public Image imageComponent;

    public float timeToCount = 5f;
    private bool isCounting;

    // Новое изображение для замены
    public Sprite Big;
    public Sprite SVD;

    public bool IsDKRInInventory;
    public bool IsTicketInInventory;

    // Метод для замены изображения
    public void ChangeSourceImage(int meetingCount)
    {
        if (meetingCount == 1)
        {
            imageComponent.sprite = Big;
            money.SetActive(true);
        }
            
        if (meetingCount == 3) 
        {
            imageComponent.sprite = SVD;
            wool.SetActive(true);
            this.meetingCount += 1;
        }
            
    }


    void Update()
    {
        if (PlayerController.Dkr == 1)
        {
            PlayerController.Dkr = 0;
            dkrV.enabled = true;
            IsDKRInInventory = true;
            StartCoroutine(FadeInD());
            GetComponent<AudioSource>().Play();
        }
        if (PlayerController.Ticket == 1)
        {
            PlayerController.Ticket = 0;
            ticketV.enabled = true;
            IsTicketInInventory = true;
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

        if (meetingCount == 2 && IsDKRInInventory)
        {
            IsDKRInInventory = false;
            dkr.SetActive(false);
            GetComponent<AudioSource>().Play();
            meetingCount += 1;
        }
        
        if (meetingCount == 1)
        {
            money.SetActive(false);
            balance.SetActive(true);
            GetComponent<AudioSource>().Play();
            PlayerController.Balance = 1;
            meetingCount += 1;
        }
        
        if (meetingCount == 0 && IsTicketInInventory)
        {
            IsTicketInInventory = false;
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
}
