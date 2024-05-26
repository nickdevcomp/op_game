using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipTrigger : MonoBehaviour
{
    public GameObject shipInventary;
    public GameObject puddle;
    public GameObject shipWithPuddle;
    public int meetingCount;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (meetingCount != 0) 
            return;
        GetComponent<AudioSource>().Play();
        shipWithPuddle.SetActive(false);
        puddle.SetActive(true);
        shipInventary.SetActive(true);
        meetingCount++;
        PlayerController.Ship = 1;
    }
}
