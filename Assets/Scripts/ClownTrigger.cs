using UnityEngine;
using UnityEngine.UI;

public class ClownTrigger : MonoBehaviour
{
    public Image shipInventary;
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
            PlayerController.Balance += 1;
        }
        if (meetingCount == 0 && PlayerController.Ship == 1)
        {
            Color color = shipInventary.color;
            color.a = 0.03f;
            shipInventary.color = color;
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
