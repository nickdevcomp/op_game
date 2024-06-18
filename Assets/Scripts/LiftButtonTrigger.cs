using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LiftButtonTrig : MonoBehaviour
{
    public GameObject otherObject;


    private Animator animator;
    
    [SerializeField]
    public float timeToCount;
    private bool isCounting;
    private bool startLoadScene;

    
    IEnumerator StartTimer()
    {
        isCounting = true;
        while (timeToCount > 0 && isCounting)
        {
            yield return new WaitForSeconds(1f);
            timeToCount -= 1f;
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
            otherObject.GetComponent<Animator>().SetBool("IsSwitchOn", true);
            GetComponent<AudioSource>().Play();
            StartCoroutine(StartTimer());
        }
    }
}
