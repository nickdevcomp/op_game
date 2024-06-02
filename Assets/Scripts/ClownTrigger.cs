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

    private void Start()
    {
        meetingCount = 0;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        
        if (meetingCount == 1)
        {
            GetComponent<AudioSource>().Play();
            meetingCount += 1;
            money.SetActive(false);
            balance.SetActive(true);
            PlayerController.Balance += 1;
        }
        if (meetingCount == 0 && Inventory.Ship == 1)
        {
            var color = shipInventory.color;
            color.a = 0.03f;
            shipInventory.color = color;
            meetingCount += 1;
            Inventory.Ship = 0;
            Source.Play();
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (!other.CompareTag("Player")) 
            return;
        if (meetingCount == 1)
            money.SetActive(true);
    }
}
