using System.Collections;
using System.Collections.Generic;
using System.Net.Sockets;
using UnityEngine;

public class clownT : MonoBehaviour
{
    public GameObject shipInventary;
    public GameObject money;
    public GameObject balance;
    public int meetingCount;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (meetingCount == 1)
        {
            GetComponent<AudioSource>().Play();
            meetingCount += 1;
            money.SetActive(false);
            balance.SetActive(true);
            PlayerController.balance = 1;
        }
        if (meetingCount == 0 && PlayerController.ship == 1)
        {
            shipInventary.SetActive(false);
            meetingCount += 1;
            PlayerController.ship = 0;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            if (meetingCount == 1)
                money.SetActive(true);
        }
    }
}
