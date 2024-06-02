using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class ShipTrigger : MonoBehaviour
{
    public Image shipInventory;
    public GameObject puddle;
    public GameObject shipWithPuddle;
    public int meetingCount;

    private void Start()
    {
        var color = shipInventory.color;
        color.a = 0.03f;
        shipInventory.color = color;
        meetingCount = 0;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (meetingCount != 0) 
            return;
        GetComponent<AudioSource>().Play();
        shipWithPuddle.SetActive(false);
        puddle.SetActive(true);
        var color = shipInventory.color;
        color.a = 1f;
        shipInventory.color = color;
        meetingCount++;
        Inventory.Ship = 1;
    }
}
