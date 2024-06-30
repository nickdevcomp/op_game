using System.Collections;
using UnityEngine;

public class AnimationFadeOut : MonoBehaviour
{
    public float FadeDuration = 1.0f;
    private bool hasKeyPressed;
    private Coroutine fadeCoroutine;
    private SpriteRenderer spriteRenderer;
    public bool isAorD;
    public bool isShift;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        if (isShift)
            UpdateShift();
        else if (isAorD)
            UpdateAorD();
    }

    private void UpdateAorD()
    {
        if (!hasKeyPressed 
            && (Input.GetKeyDown(KeyCode.A) 
                || Input.GetKeyDown(KeyCode.D)
                || Input.GetKeyDown(KeyCode.LeftArrow) 
                || Input.GetKeyDown(KeyCode.RightArrow)))
        {
            hasKeyPressed = true;
            StartCoroutine(WaitAndStartFadeOut(1.3f)); 
        }
    }
    
    private void UpdateShift()
    {
        if (!hasKeyPressed && (Input.GetKeyDown(KeyCode.LeftShift) || Input.GetKeyDown(KeyCode.RightShift)))
        {
            hasKeyPressed = true;
            StartCoroutine(WaitAndStartFadeOut(1.3f)); 
        }
    }

    private IEnumerator WaitAndStartFadeOut(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        fadeCoroutine = StartCoroutine(FadeOutAnimation());
    }

    private IEnumerator FadeOutAnimation()
    {
        // review(30.06.2024): var
        float elapsedTime = 0.0f;
        Color initialColor = spriteRenderer.color;

        while (elapsedTime < FadeDuration)
        {
            elapsedTime += Time.deltaTime;
            float newAlpha = Mathf.Lerp(initialColor.a, 0.0f, elapsedTime / FadeDuration);
            spriteRenderer.color = new Color(initialColor.r, initialColor.g, initialColor.b, newAlpha);
            yield return null;
        }

        spriteRenderer.color = new Color(initialColor.r, initialColor.g, initialColor.b, 0.0f);
    }
}