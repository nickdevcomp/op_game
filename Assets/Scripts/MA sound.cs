using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class IntroManager : MonoBehaviour
{
    public Image fadeImage;
    public Text introText;
    public AudioSource audioSource;
    public float fadeDuration = 1f;
    public float textDuration = 2f;

    private bool isFading;
    private float fadeTimer;

    private void Start()
    {
        fadeImage.gameObject.SetActive(true);
        introText.gameObject.SetActive(false);
        fadeTimer = fadeDuration;
        isFading = true;
    }

    private void Update()
    {
        if (isFading)
        {
            fadeTimer -= Time.deltaTime;
            float alpha = Mathf.Lerp(1, 0, fadeTimer / fadeDuration);
            fadeImage.color = new Color(0, 0, 0, alpha);

            if (fadeTimer <= 0)
            {
                isFading = false;
                introText.gameObject.SetActive(true);
                Invoke("HideIntroText", textDuration);
                Invoke("PlayAudioAndLoadScene", textDuration + 0.5f);
            }
        }
    }

    private void HideIntroText()
    {
        introText.gameObject.SetActive(false);
    }

    private void PlayAudioAndLoadScene()
    {
        audioSource.Play();
        Invoke("LoadScene", audioSource.clip.length);
    }

    private void LoadScene()
    {
        SceneManager.LoadScene(1);
    }
}