using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;

public class RailTrigger : MonoBehaviour
{
    public GameObject Player;

    private Animator otherObjectAnimator;

    private Animator animator;
    
    public float timeToCount = 2f;
    private bool isCounting;
    private bool btns;

    [SerializeField]
    private float upY;
    [SerializeField]
    private float downY;
    [SerializeField] 
    private bool isHighestFloor;
    [SerializeField] 
    private bool isLowestFloor;


    private void Update()
    {
        if (!btns)
            return;
        if ((Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow)) && !isHighestFloor)
        {
            Player.SetActive(false);
            GetComponent<AudioSource>().Play();
            StartCoroutine(StartTimer());
            Player.transform.position = new Vector3(980, upY, -2);
        }
        if ((Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow)) && !isLowestFloor)
        {
            Player.SetActive(false);
            GetComponent<AudioSource>().Play();
            StartCoroutine(StartTimer());
            Player.transform.position = new Vector3(980, downY, -2);
        }
    }


    IEnumerator StartTimer()
    {
        isCounting = true;
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

    

    private void OnTriggerEnter2D(Collider2D other) => btns = true;

    private void OnTriggerExit2D(Collider2D other) => btns = false;

}
