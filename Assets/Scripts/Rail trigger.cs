using System.Collections;
using UnityEngine;

public class RailTrigger : MonoBehaviour
{
    public GameObject Player;

    private Animator otherObjectAnimator;

    private Animator animator;
    
    public float timeToCount = 1f;
    private bool isCounting;
    private bool btns;

    [SerializeField]
    private float upY;
    [SerializeField]
    private float downY;
    [SerializeField] 
    private bool isHighestFloor;
    [SerializeField] 
    private bool isLowestFloor;


    private void Update()
    {
        if (!btns)
            return;
        if ((Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow)) && !isHighestFloor)
        {
            Player.SetActive(false);
            GetComponent<AudioSource>().Play();
            StartCoroutine(StartTimer());
            Player.transform.position = new Vector3(980, upY, -2);
        }
        if ((Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow)) && !isLowestFloor)
        {
            Player.SetActive(false);
            GetComponent<AudioSource>().Play();
            StartCoroutine(StartTimer());
            Player.transform.position = new Vector3(980, downY, -2);
        }
    }


    IEnumerator StartTimer()
    {
        isCounting = true;
        while (timeToCount > 0) 
        {
            yield return new WaitForSeconds(1f); 
            timeToCount -= 1f; 
            Debug.Log(timeToCount); 
        }
        isCounting = false; 
        timeToCount = 1f;
        Player.SetActive(true);
    }

    

    private void OnTriggerEnter2D(Collider2D other) => btns = true;

    private void OnTriggerExit2D(Collider2D other) => btns = false;

}
