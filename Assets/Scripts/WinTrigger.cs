using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class WinTrigger : MonoBehaviour
{

    public GameObject otherObject; // ссылка на другой объект

    private Animator otherObjectAnimator; // компонент аниматора другого объекта

    private Animator animator;

    private bool triger = true;
    
    [SerializeField]
    public float timeToCount = 2f; // Время таймера в секундах
    private bool isCounting; // Флаг, показывающий, запущен ли таймер
    private bool startLoadScene;

    
    void Start()
    {
        // Получаем компонент аниматора другого объекта
        otherObjectAnimator = otherObject.GetComponent<Animator>();
    }

    IEnumerator StartTimer()
    {
        isCounting = true;
        while (timeToCount > 0) 
        {
            yield return new WaitForSeconds(1f);
            timeToCount -= 1f;
            Debug.Log(timeToCount);
        }
        isCounting = false;
        timeToCount = 4f;
        SceneManager.LoadScene("Plotinka");
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        startLoadScene = other.CompareTag("Finish");
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        startLoadScene = false;
    }

    private void Update()
    {
        if (startLoadScene && Input.GetKey(KeyCode.E) && triger)
        {
            triger = false;
            otherObjectAnimator.SetBool("IsSwitchOn", true);
            GetComponent<AudioSource>().Play();
            StartCoroutine(StartTimer());
        }
    }
}
