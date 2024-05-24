using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipT : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject shipInventary;
    public GameObject puddle;
    public GameObject shipWithPuddle;
    public int meetingCount;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (meetingCount == 0)
        {
            GetComponent<AudioSource>().Play();
            shipWithPuddle.SetActive(false);
            puddle.SetActive(true);
            shipInventary.SetActive(true);
            meetingCount++;
            PlayerController.ship = 1;
        }
    }
}
