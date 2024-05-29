using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ButtonManager : MonoBehaviour
{
    public void Play() => SceneManager.LoadScene(1);

    public void Exit() => Application.Quit();
}