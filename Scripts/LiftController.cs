using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LiftController : MonoBehaviour
{
    private Animator animator;
    public float timeToCount = 4f; // Время таймера в секундах
    private bool isCounting = false; // Флаг, показывающий, запущен ли таймер

    IEnumerator StartTimer()
    {
        // review(24.05.2024): Код очень часто дублируется
        isCounting = true; // Устанавливаем флаг, что таймер запущен
        while (timeToCount > 0) // Пока время не истечет
        {
            yield return new WaitForSeconds(1f); // Ждем одну секунду
            timeToCount -= 1f; // Уменьшаем время на одну секунду
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

    private void Update()
    {
    }
}
