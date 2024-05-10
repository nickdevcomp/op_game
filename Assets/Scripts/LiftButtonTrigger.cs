using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LiftButtonTrig : MonoBehaviour
{
    private Animator animator;
    public float timeToCount = 1f; // Время таймера в секундах
    private bool isCounting = false; // Флаг, показывающий, запущен ли таймер

    IEnumerator StartTimer()
    {
        isCounting = true; // Устанавливаем флаг, что таймер запущен
        while (timeToCount > 0) // Пока время не истечет
        {
            yield return new WaitForSeconds(1f); // Ждем одну секунду
            timeToCount -= 1f; // Уменьшаем время на одну секунду
            Debug.Log(timeToCount); // Выводим оставшееся время в консоль
        }
        isCounting = false; // Таймер закончился, сбрасываем флаг
        SceneManager.LoadScene("Lift"); // Сообщаем, что время истекло
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            GetComponent<AudioSource>().Play();
            StartCoroutine(StartTimer());
        }
    }
    
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            animator.SetTrigger("isTriggered");
        }
    }
}
