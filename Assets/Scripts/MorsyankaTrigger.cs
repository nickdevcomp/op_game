using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class MorsyankaTrigger : MonoBehaviour
{
    public static bool IsPlay;
    public Image game;
    public Button ButtonWin;
    public Sprite winBoard;
    public GameObject player;
    public GameObject feather;

    public float timeToCount = 2f;
    private bool isCounting;

    public Texture2D cursorTexture;
    public Vector2 hotSpot = Vector2.zero;
    public CursorMode cursorMode = CursorMode.Auto;

    private void Start() => IsPlay = false;

    void Update()
    {
        if (!IsPlay)
            return;
        
        PlayerController.StartTime = Time.realtimeSinceStartup;
        Fear.FearValue = 0;
        
        IsPlay = false;
        HandleGame();
        player.SetActive(false);
        feather.SetActive(false);
        Cursor.SetCursor(cursorTexture, hotSpot, cursorMode);
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
        PlayerController.StartTime = Time.realtimeSinceStartup;
        StartCoroutine(StartTimer());
        Inventory.Key = 1;
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
