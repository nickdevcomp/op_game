using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class WST : MonoBehaviour
{
    public Image image;

    void Start()
    {
        image.enabled = false;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.L))
        {
            ShowWastedScreen();
        }
    }

    void ShowWastedScreen()
    {
        image.enabled = true;
        StartCoroutine(FadeIn());
        GetComponent<AudioSource>().Play();
    }

    IEnumerator FadeIn()
    {
        image.color = new Color(image.color.r, image.color.g, image.color.b, 0);

        while (image.color.a < 1)
        {
            image.color = new Color(image.color.r, image.color.g, image.color.b, image.color.a + Time.deltaTime);
            yield return null;
        }

        yield return new WaitForSeconds(4);

        StartCoroutine(FadeOut());
    }

    IEnumerator FadeOut()
    {
        while (image.color.a > 0)
        {
            image.color = new Color(image.color.r, image.color.g, image.color.b, image.color.a - Time.deltaTime);
            yield return null;
        }

        image.enabled = false;
    }
}
