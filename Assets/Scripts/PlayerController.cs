
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.UI;
using Random = System.Random;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float speed = 15;
    private Vector3 input;
    private bool isWalking;
    private bool isRunning;

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
    private bool isScaryFloor;

    [SerializeField]
    private SpriteRenderer characterSprite;
    
    [SerializeField]
    private Light flashlight; // Добавляем ссылку на источник света

    [CanBeNull] public AudioSource RasinSound;

    private float reflectStartTime;
    private bool isAdditionalTimerStarted;
    private float startTime;
    private bool isTimerRunning;

    private const float DeathVolume = 0.067f;
    private const float DontTurnAroundVolume = 1;
    private const float ShakeAmount = 0.07f;
    
    public AudioSource DeathSound;
    public AudioSource DontTurnAround;

    private void Start()
    {
        Balance = 0;
        animator = GetComponent<Animator>();
        StartTime = Time.realtimeSinceStartup;
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
            quantity.text = "<size=10><color=#fff>x " + Balance.ToString() + "</color></size>";
    }

    private void MoveCharacter()
    {
        if (IsDied) 
            return;
        input = new Vector2(Input.GetAxis("Horizontal"), 0);
        isWalking = input.x != 0;
        isRunning = !isScaryFloor && input.x != 0 
                                  && (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift));        
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
        if (!isScaryFloor) 
            return;
        
        endTime = Time.realtimeSinceStartup;
        elapsedTime = endTime - StartTime;
        if (elapsedTime >= 10f && !IsDied)
        {
            Fear.FearValue = 1;
        }

        if (elapsedTime >= 15f)
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
        /*if (Fear.FearValue == 1 && RasinSound != null)
            RasinSound.Play();*/

        if (Fear.FearValue == 1)
        {
            isAdditionalTimerStarted = true;
        }
        
        isRight = !isRight;
        if (isTimerRunning)
        {
            isAdditionalTimerStarted = false;
        }

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
        isTimerRunning = reflectStartTime > 0;
        StartTime += Time.deltaTime;
        reflectStartTime += Time.deltaTime;
        DeathSound.volume = (2f - reflectStartTime) / 2f * DeathVolume;
        DontTurnAround.volume = (2f - reflectStartTime) / 2f * DontTurnAroundVolume;
        CameraController.ShakeAmount = (2f - reflectStartTime) / 2f * ShakeAmount;
        if (reflectStartTime > 2f)
        {
            var rnd = new Random();
            var shouldPlay = rnd.Next(5, 8);
            if (shouldPlay == 7)
                RasinSound.Play();
            Fear.FearValue = 0;
            isAdditionalTimerStarted = false;
            StartTime = Time.realtimeSinceStartup;
            reflectStartTime = 0f;  
            isTimerRunning = false;
            CameraController.ShakeAmount = ShakeAmount;
            DeathSound.volume = DeathVolume;
            DontTurnAround.volume = DontTurnAroundVolume;
        }
    }
}
