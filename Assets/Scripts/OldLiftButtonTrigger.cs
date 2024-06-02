using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class OldLiftButtonTrigger : MonoBehaviour
{
    public GameObject otherObject;
    private Animator otherObjectAnimator;
    private Animator animator;
    
    public float timeToCount;
    private bool isCounting;
    private bool startLoadScene;
    
    void Start()
    {
        otherObjectAnimator = otherObject.GetComponent<Animator>();
        timeToCount = 5f;
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
        
        SceneManager.LoadScene("Plotinka");
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        startLoadScene = other.CompareTag("Player");
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        startLoadScene = false;
    }

    private void Update()
    {
        if (startLoadScene && Input.GetKey(KeyCode.E))
        {
            otherObjectAnimator.SetBool("IsSwitchOn", true);
            GetComponent<AudioSource>().Play();
            StartCoroutine(StartTimer());
        }
    }
}
