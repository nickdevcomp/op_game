using UnityEngine;
using System.Collections;

public class GirlAppear : MonoBehaviour
{
    public GameObject girl;
    public float appearDuration = 4f;
    private float moveSpeed = 4f;
    private AudioSource audioSource;
    private bool hasAppeared;
    private Transform playerTransform;
    public PlayerController Player;
    public bool IsLast;

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
            if (!IsLast || (IsLast && Inventory.Feather == 1))
            {
                playerTransform = other.transform;
                StartCoroutine(ShowGirl());
            }
        }
    }

    private IEnumerator ShowGirl()
    {
        hasAppeared = true;
        girl.SetActive(true);
        audioSource.Play();
        var elapsedTime = 0f;
        var startPosition = girl.transform.position;
        

        while (elapsedTime < appearDuration)
        {
            Fear.FearValue = 1;
            elapsedTime += Time.deltaTime;
            var direction = (playerTransform.position - startPosition).normalized;
            girl.transform.position = Vector3.MoveTowards(girl.transform.position, playerTransform.position, moveSpeed * Time.deltaTime);
            
            if (Vector3.Distance(girl.transform.position, playerTransform.position) < 0.7f)
            {
                Player.IsDied = true;
                Player.animator.Play("Falling");
                Fear.FearValue = 0;
                PlayerController.StartTime = Time.realtimeSinceStartup;
            }

            yield return null;
        }
        girl.SetActive(false);
    }
}