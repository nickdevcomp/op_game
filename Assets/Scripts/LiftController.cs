using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LiftController : MonoBehaviour
{
    private Animator animator;
    public float timeToCount; // Время таймера в секундах
    private bool isCounting = false; // Флаг, показывающий, запущен ли таймер
    public AudioSource WhyDidntStop;

    IEnumerator StartTimer()
    {
        isCounting = true; // Устанавливаем флаг, что таймер запущен
        while (timeToCount > 0) // Пока время не истечет
        {
            yield return new WaitForSeconds(1f); // Ждем одну секунду
            timeToCount -= 1f; // Уменьшаем время на одну секунду
            if (Mathf.Approximately(timeToCount, 4f))
                WhyDidntStop.Play();
            Debug.Log(timeToCount); // Выводим оставшееся время в консоль
        }
        isCounting = false; // Таймер закончился, сбрасываем флаг
        SceneManager.LoadScene("Dungeon"); // Сообщаем, что время истекло
    }

    private void Start()
    {
        animator = GetComponent<Animator>();
        StartCoroutine(StartTimer());
    }
}
