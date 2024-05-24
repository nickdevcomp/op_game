using System;
using UnityEngine;
using System.Collections;
using UnityEngine.UI;

// review(24.05.2024): Непонятное название класса
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
        GetComponent<AudioSource>().Play(); // review(24.05.2024): Имеет смысл навесить атрибут RequiredComponent и положить компонент в поле класса при создании либо при Awake
    }

    IEnumerator FadeIn()
    {
        image.color = new Color(image.color.r, image.color.g, image.color.b, 0);

        while (image.color.a < 1)
        {
            // review(24.05.2024): Тут как будто бы не хватает extension method для Color: image.color.WithAlpha(). Код бы стало проще читать кмк
            image.color = new Color(image.color.r, image.color.g, image.color.b, image.color.a + Time.deltaTime);
            yield return null;
        }

        // review(24.05.2024): Показательный пример, почему комментарии в большинстве случаев - зло
        yield return new WaitForSeconds(4); // Ждем 2 секунды

        StartCoroutine(FadeOut());
    }

    IEnumerator FadeOut()
    {
        // review(24.05.2024): Логика дублирует FadeIn. Кажется, что можно выделить общий метод. Плюс к тому, тут никак не ограничивается
        // альфа-компонента цвета. Стоит добавить ограничение, чтобы она всегда была от 0 до 255 включительно
        while (image.color.a > 0)
        {
            image.color = new Color(image.color.r, image.color.g, image.color.b, image.color.a - Time.deltaTime);
            yield return null;
        }

        image.enabled = false;
    }
}
