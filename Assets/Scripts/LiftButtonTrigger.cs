using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LiftButtonTrig : MonoBehaviour
{
    public GameObject otherObject; // ссылка на другой объект

    private Animator otherObjectAnimator; // компонент аниматора другого объекта

    private Animator animator;
    
    [SerializeField]
    public float timeToCount; // Время таймера в секундах
    private bool isCounting; // Флаг, показывающий, запущен ли таймер
    private bool startLoadScene;

    
    void Start()
    {
        otherObjectAnimator = otherObject.GetComponent<Animator>();
    }

    IEnumerator StartTimer()
    {
        isCounting = true;
        while (timeToCount > 0 && isCounting)
        {
            yield return new WaitForSeconds(1f);
            timeToCount -= 1f;
            Debug.Log(timeToCount);
        }
        isCounting = false;
        SceneManager.LoadScene("Lift");
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        startLoadScene = other.CompareTag("Player");
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        startLoadScene = false;
    }

    private void Update()
    {
        if (startLoadScene && Input.GetKey(KeyCode.E))
        {
            otherObjectAnimator.SetBool("IsSwitchOn", true);
            GetComponent<AudioSource>().Play();
            StartCoroutine(StartTimer());
        }
    }
}
