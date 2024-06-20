using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class ClownTrigger : MonoBehaviour
{
    public Image shipInventory;
    public GameObject money;
    public GameObject balance;
    public int meetingCount;
    public AudioSource Source;
    public AudioSource Beep;
    public AudioSource GoAway;
    private bool isShipReturned;
    private bool isNear;

    private void Start()
    {
        meetingCount = 0;
    }

    private void Update()
    {
        if (!isNear)
        {
            return;
        }

        if (isNear && Input.GetKeyDown(KeyCode.E) && Inventory.Ship == 0)
        {
            if (!GoAway.isPlaying && isShipReturned && !Source.isPlaying)
                GoAway.Play();
            else if (!isShipReturned)
                Beep.Play();
            return;
        }
        
        if (meetingCount == 0 && Inventory.Ship == 1 && Input.GetKeyDown(KeyCode.E))
        {
            PlayerController.StartTime = Time.realtimeSinceStartup;
            Fear.FearValue = 0;
            
            var color = shipInventory.color;
            color.a = 0f;
            shipInventory.color = color;
            meetingCount += 1;
            Inventory.Ship = 0;
            Source.Play();
            isShipReturned = true;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        isNear = true;
        if (meetingCount == 1)
        {
            Beep.Play();
            meetingCount += 1;
            money.SetActive(false);
            balance.SetActive(true);
            PlayerController.Balance += 1;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        isNear = false;

        if (meetingCount == 1)
        {
            money.SetActive(true);
        }
    }
}