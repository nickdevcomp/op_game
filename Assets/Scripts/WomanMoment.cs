using UnityEngine;
using System.Collections;

public class GirlAppear : MonoBehaviour
{
    public GameObject girl; 
    public float appearDuration = 2f; 
    private AudioSource audioSource;
    private bool hasAppeared;

    void Start()
    {
        if (girl != null)
        {
            girl.SetActive(false);
            audioSource = girl.GetComponent<AudioSource>();
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (!hasAppeared && other.CompareTag("Player") && girl != null)
        {
            StartCoroutine(ShowGirl());
        }
    }

    private IEnumerator ShowGirl()
    {
        hasAppeared = true; 
        girl.SetActive(true);
        audioSource.Play();
        yield return new WaitForSeconds(appearDuration);
        girl.SetActive(false);
    }
}