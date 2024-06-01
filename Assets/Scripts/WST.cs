using System;
using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class WST : MonoBehaviour
{
    public Image image;
    private bool isShowed;
    public AudioSource wastedSound;
    [SerializeField] 
    public PlayerController Player;

    void Start()
    {
        image.enabled = false;
    }

    void Update()
    {
        if (Player.IsDied && !isShowed)
        {
            isShowed = true;
            ShowWastedScreen();
        }
    }

    void ShowWastedScreen()
    {
        image.enabled = true;
        StartCoroutine(FadeIn());
        wastedSound.Play();
    }

    IEnumerator FadeIn()
    {
        image.color = new Color(image.color.r, image.color.g, image.color.b, 0);

        while (image.color.a < 1)
        {
            image.color = new Color(image.color.r, image.color.g, image.color.b, image.color.a + Time.deltaTime);
            yield return null;
        }

        yield return new WaitForSeconds(10);
        
        SceneManager.LoadScene("Menu");

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
