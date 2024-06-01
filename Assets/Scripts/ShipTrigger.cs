using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShipTrigger : MonoBehaviour
{
    public Image shipInventary;
    public GameObject puddle;
    public GameObject shipWithPuddle;
    public int meetingCount;

    private void Start()
    {
        Color color = shipInventary.color;
        color.a = 0.03f;
        shipInventary.color = color;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (meetingCount != 0) 
            return;
        GetComponent<AudioSource>().Play();
        shipWithPuddle.SetActive(false);
        puddle.SetActive(true);
        Color color = shipInventary.color;
        color.a = 1f;
        shipInventary.color = color;
        meetingCount++;
        PlayerController.Ship = 1;
    }
}
