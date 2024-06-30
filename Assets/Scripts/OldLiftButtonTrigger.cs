using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

// review(30.06.2024): Если он старый, то почему используется?
public class OldLiftButtonTrigger : MonoBehaviour
{
    public GameObject otherObject;
    private Animator otherObjectAnimator;
    private Animator animator;
    public AudioSource ChatacterThoughs;
    
    public AudioSource LiftSound;

    
    public float timeToCount;
    private bool isCounting;
    private bool startLoadScene;
    private bool isPlayLiftAnimation;
    
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
        if (startLoadScene && Input.GetKey(KeyCode.E) && Inventory.Key == 1)
        {
            StartCoroutine(StartTimer());
        }

        if (Inventory.Key == 1 && !isPlayLiftAnimation)
        {
            isPlayLiftAnimation = true;
            otherObjectAnimator.SetBool("IsSwitchOn", true);
            LiftSound.Play();
            Invoke("PlayThoughs",LiftSound.clip.length + 1f);
            
        }
    }
    
    private void PlayThoughs() => ChatacterThoughs.Play();
}
