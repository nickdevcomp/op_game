using UnityEngine;

public class ClownTrigger : MonoBehaviour
{
    public GameObject shipInventary;
    public GameObject money;
    public GameObject balance;
    public int meetingCount;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (meetingCount == 1)
        {
            GetComponent<AudioSource>().Play();
            meetingCount += 1;
            money.SetActive(false);
            balance.SetActive(true);
            PlayerController.Balance = 1;
        }
        if (meetingCount == 0 && PlayerController.Ship == 1)
        {
            shipInventary.SetActive(false);
            meetingCount += 1;
            PlayerController.Ship = 0;
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
