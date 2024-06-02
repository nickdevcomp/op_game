using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class MorsyankaTrigger : MonoBehaviour
{
    public static int MorsynkaPlay;
    public Image game;
    public Button ButtonWin;
    public Sprite winBoard;
    public GameObject player;
    public GameObject key;
    public GameObject feather;

    public float timeToCount = 2f;
    private bool isCounting;

    public Texture2D cursorTexture; // Текстура для кастомного курсора
    public Vector2 hotSpot = Vector2.zero; // Точка фокуса курсора
    public CursorMode cursorMode = CursorMode.Auto;
    
    void Update()
    {
        if (MorsynkaPlay == 1)
        {
            MorsynkaPlay = 0;
            HandleGame();
            player.SetActive(false);
            feather.SetActive(false);
            Cursor.SetCursor(cursorTexture, hotSpot, cursorMode);
        }
    }

    public void HandleGame()
    {
        game.enabled = true;
        ButtonWin.onClick.AddListener(OnButtonClick);
    }

    public void OnButtonClick()
    {
        game.sprite = winBoard;
        GetComponent<AudioSource>().Play();
        key.SetActive(true);
        StartCoroutine(StartTimer());
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
        player.SetActive(true);
        game.enabled = false;
        Cursor.SetCursor(null, Vector2.zero, cursorMode);
    }
}
