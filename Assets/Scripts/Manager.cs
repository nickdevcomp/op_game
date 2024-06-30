using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ButtonManager : MonoBehaviour
{
    public Image fadeImage;
    public AudioSource audioSource;
    private float fadeSpeed;
    public AudioSource backgroundMusic;
    private bool isFirst = true;
    private void Start()
    {
        fadeImage.GameObject().SetActive(false);
        backgroundMusic.Play();
        audioSource.Pause();
        Application.targetFrameRate = 120;
        fadeSpeed = 0.5f;
        StopCoroutine(FadeIn());
    }

    public void Play()
    {
        if (isFirst)
        {
            isFirst = false;
            fadeImage.GameObject().SetActive(true);
            backgroundMusic.Pause();
            Invoke("Fade", 0);
            Invoke("PlayAudio", 2f);
            Invoke("LoadFirstScene", 9f);
        }
        else
        {
            SceneManager.LoadScene(1);
        }
    }

    private void Fade() =>  StartCoroutine(FadeIn());

    private void PlayAudio() => audioSource.Play();

    private void LoadFirstScene() => SceneManager.LoadScene("6 floor");

    private IEnumerator FadeIn()
    {
        // review(30.06.2024): Дублирование FadeIn-а
        var delta = 0.0f;
        while (delta < 1)
        {
            delta += fadeSpeed * Time.deltaTime;
            fadeImage.color = new Color(fadeImage.color.r, fadeImage.color.g, fadeImage.color.b, delta);
            yield return null;
        }
    }

    public void Exit() => Application.Quit();
}