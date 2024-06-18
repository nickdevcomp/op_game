
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;
using Random = System.Random;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float speed = 15;
    private Vector3 input;
    private bool isWalking;
    private bool isRunning;

    public static bool IsAudioPlaying;

    private Animator animator;
    private bool isRight = true;
    private float timer = 0f;
    private float endTime;
    private float elapsedTime;
    public Text quantity;
    public static int Balance;

    public static float StartTime;
    public bool IsDied;

    [SerializeField]
    public bool IsScaryFloor;

    public static bool isScaryForPause;

    [SerializeField]
    private SpriteRenderer characterSprite;
    
    [SerializeField]
    private Light flashlight; 


    public static float ReflectStartTime;
    private bool isAdditionalTimerStarted;
    private float startTime;
    private bool isTimerRunning;

    private const float DeathVolume = 0.1f;
    private const float DontTurnAroundVolume = 1;
    public const float ShakeAmount = 0.1f;
    public const float BrokeFearTime = 2.5f;
    
    public AudioSource DeathSound;
    public AudioSource DontTurnAround;

    public static bool IsPosterActive;

    private void Start()
    {
        Balance = 0;
        animator = GetComponent<Animator>();
        StartTime = Time.realtimeSinceStartup;
        ReflectStartTime = 0f;  
        isAdditionalTimerStarted = false;
        isTimerRunning = false;
        DeathSound.volume = DeathVolume;
        DontTurnAround.volume = DontTurnAroundVolume;
        isScaryForPause = IsScaryFloor;

    }

    private void Update()
    {
        MoveCharacter();
        UpdateFear();
        UpdateQuantity();
        if (isAdditionalTimerStarted)
            AdditionalTimer();
    }

    private void UpdateQuantity()
    {
        if (quantity != null) 
            quantity.text = "<size=10><color=#fff>x " + Balance + "</color></size>";
    }

    private void MoveCharacter()
    {
        if (IsDied) 
            return;
        input = new Vector2(Input.GetAxis("Horizontal"), 0);
        isWalking = input.x != 0;
        isRunning = input.x != 0 && (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift));        
        if (isWalking || isRunning)
        {
            var animationToPlay = isRunning ? "Run" : "Walk";
            var movementSpeed = isRunning ? 3f * speed : speed;
            animator.Play(animationToPlay);
            Reflect(input);
            transform.position += input * (movementSpeed * Time.deltaTime);
        }
        else
        {
            animator.Play("Calm");
        }
    }

    private void UpdateFear()
    {
        if (!IsScaryFloor) 
            return;
        
        endTime = Time.realtimeSinceStartup;
        elapsedTime = endTime - StartTime;
        
        if (elapsedTime >= 13f && !IsDied)
        {
            Fear.FearValue = 1;
        }

        if (elapsedTime >= 19f)
        {
            IsDied = true;
            animator.Play("Falling");
            Fear.FearValue = 0;
            StartTime = Time.realtimeSinceStartup;
        }
    }

    private void Reflect(Vector2 movement)
    {
        if ((!(movement.x > 0) || isRight) && (!(movement.x < 0) || !isRight)) 
            return;

        if (Fear.FearValue == 1)
        {
            isAdditionalTimerStarted = true;
        }
        else
        {
            StartTime = Time.realtimeSinceStartup;
        }
        
        if (isTimerRunning)
        {
            isTimerRunning = false;
            isAdditionalTimerStarted = false;
        }

        isRight = !isRight;

        var newScale = transform.localScale;
        newScale.x *= -1;
        transform.localScale = newScale;

        if (flashlight != null)
        {
            var lightRotation = flashlight.transform.localEulerAngles;
            lightRotation.y += 180f;
            flashlight.transform.localEulerAngles = lightRotation;
        }
    }

    private void AdditionalTimer()
    {
        isTimerRunning = ReflectStartTime > 0f;
        StartTime += Time.deltaTime;
        ReflectStartTime += Time.deltaTime;
        DeathSound.volume = DeathVolume * (BrokeFearTime - ReflectStartTime) / BrokeFearTime;
        DontTurnAround.volume = DontTurnAroundVolume * (BrokeFearTime - ReflectStartTime) / BrokeFearTime;
        CameraController.ShakeAmount = ShakeAmount * (BrokeFearTime - ReflectStartTime) / BrokeFearTime;
        if (ReflectStartTime >= BrokeFearTime)
        {
            ResetFear();
        }
    }

    public void ResetFear()
    {
        Fear.FearValue = 0;
        isAdditionalTimerStarted = false;
        StartTime = Time.realtimeSinceStartup;
        ReflectStartTime = 0f;  
        isTimerRunning = false;
        CameraController.ShakeAmount = ShakeAmount;
        DeathSound.volume = DeathVolume;
        DontTurnAround.volume = DontTurnAroundVolume;
    }
}
