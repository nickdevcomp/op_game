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
    private bool isNear;

    private void Start()
    {
        meetingCount = 0;
    }

    private void Update()
    {
        if (!isNear)
            return;
        
        if (meetingCount == 0 && Inventory.Ship == 1 && Input.GetKeyDown(KeyCode.E))
        {
            var color = shipInventory.color;
            color.a = 0f;
            shipInventory.color = color;
            meetingCount += 1;
            Inventory.Ship = 0;
            Source.Play();
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        isNear = true;
        
        if (meetingCount == 1)
        {
            GetComponent<AudioSource>().Play();
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