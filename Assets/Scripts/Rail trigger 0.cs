using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;

public class RailTrig0 : MonoBehaviour
{
    public GameObject Player; // ссылка на другой объект

    private Animator otherObjectAnimator; // компонент аниматора другого объекта

    private Animator animator;
    
    public float timeToCount = 2f; // Время таймера в секундах
    private bool isCounting = false; // Флаг, показывающий, запущен ли таймер
    private bool btns = false;
    
    void Start()
    {
        // Получаем компонент аниматора другого объекта
        
    }

    void Update()
    {
        if (btns)
        {
            if (Input.GetKeyDown(KeyCode.E) && false)
            {
                Player.SetActive(false);
                GetComponent<AudioSource>().Play();
                StartCoroutine(StartTimer());
                Player.transform.position = new Vector3(1126, 128, -1);
            }
            if (Input.GetKeyDown(KeyCode.R))
            {
                Player.SetActive(false);
                GetComponent<AudioSource>().Play();
                StartCoroutine(StartTimer());
                Player.transform.position = new Vector3(1126, 29, -1);
            }
        }
    }


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
                            // Сообщаем, что время истекло
        timeToCount = 2f;
        Player.SetActive(true);
    }

    

    private void OnTriggerEnter2D(Collider2D other)
    {
        btns = true;
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        // Проверяем, что объект, покидающий триггер, имеет нужный тег
        btns = false;
            // Здесь можно добавить необходимую логику
    }

}
