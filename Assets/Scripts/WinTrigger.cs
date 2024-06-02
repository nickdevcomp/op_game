using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class WinTrigger : MonoBehaviour
{

    public GameObject otherObject;

    private Animator otherObjectAnimator;

    private Animator animator;

    private bool trigger = true;
    
    [SerializeField]
    public float timeToCount = 2f;
    private bool isCounting;
    private bool startLoadScene;

    
    void Start()
    {
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
        if (startLoadScene && Input.GetKey(KeyCode.E) && trigger)
        {
            trigger = false;
            StartCoroutine(StartTimer());
        }
    }
}
