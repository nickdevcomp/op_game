using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class PostersManager : MonoBehaviour
{
    public Transform player;
    public Transform hintObject;
    public Image image;
    private const KeyCode KeyToPress = KeyCode.E;
    private float fadeSpeed = 4;
    private float hintDistance = 0.5f;

    private void Start() => image.gameObject.SetActive(false);

    private void Update()
    {
        var distance = Vector2.Distance(player.position, transform.position);
        if (distance <= hintDistance)
        {
            hintObject.gameObject.SetActive(true);
        }
        else
        {
            hintObject.gameObject.SetActive(false);
            image.gameObject.SetActive(false);
            return;
        }

        if (!Input.GetKeyDown(KeyToPress)) 
            return;
        
        if (!image.gameObject.activeInHierarchy)
        {
            image.gameObject.SetActive(true);
            StartCoroutine(FadeIn());
        }
        else
        {
            image.gameObject.SetActive(false);
            StartCoroutine(FadeOut());
        }
    }

    private IEnumerator FadeIn()
    {
        var delta = 0.0f;
        while (delta < 1)
        {
            delta += fadeSpeed * Time.deltaTime;
            image.color = new Color(1, 1, 1, delta);
            yield return null;
        }
    }

    private IEnumerator FadeOut()
    {
        var delta = 1.0f;
        while (delta > 0)
        {
            delta -= fadeSpeed * Time.deltaTime;
            image.color = new Color(1, 1, 1, delta);
            yield return null;
        }
        image.gameObject.SetActive(false);
    }
}