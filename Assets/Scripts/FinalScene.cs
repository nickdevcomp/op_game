using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameCompletion : MonoBehaviour
{
    public Image[] imagesToShow = new Image[3];
    public Text completionText;
    public Button exitButton;

    private int currentImageIndex = 0;

    void Start()
    {
        Invoke("StartSlides",4f);
    }
    
    private void StartSlides() => StartCoroutine(ShowImages());

    IEnumerator ShowImages()
    {
        
        foreach (var image in imagesToShow)
        {
            image.gameObject.SetActive(true);

            var startColor = image.color;
            var endColor = new Color(image.color.r, image.color.g, image.color.b, 0f);

            var timer = 0f;
            while (timer < 4f)
            {
                timer += Time.deltaTime / 2f; 

                image.color = Color.Lerp(startColor, endColor, timer);
                yield return null;
            }

            image.gameObject.SetActive(false);
        }

        completionText.gameObject.SetActive(true);
        exitButton.gameObject.SetActive(true);
    }


    public void ExitGame() => Application.Quit(); 
}