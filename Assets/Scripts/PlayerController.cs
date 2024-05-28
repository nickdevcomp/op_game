using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float speed = 15;
    private Vector3 input;
    private bool isWalking;
    private bool isRunning;
    
    private Animator animator;
    private bool isRight;
    private float timer = 0f;
    private float startTime;
    private float endTime;
    private float elapsedTime;

    public static int Ticket;
    public static int Dkr;
    public static int Ship;
    public static int Balance;
    public static int Feather;
    
    public AudioSource footstepSound;


    [SerializeField] 
    private bool isScaryFloor;
    
    [SerializeField] 
    private SpriteRenderer characterSprite; 
    
    private void Start()
    {
        animator = GetComponent<Animator>();
        startTime = Time.realtimeSinceStartup;
        Application.targetFrameRate = 120;
    }
    
    private void Update()
    {
        MoveCharacter();
        UpdateFear();
    }

    private void MoveCharacter()
    {
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
            animator.Play("Calm");
    }

    private void UpdateFear()
    {
        if (!isScaryFloor)
            return;
        endTime = Time.realtimeSinceStartup;
        elapsedTime = endTime - startTime;
        if (elapsedTime >= 10f)
            Fear.FearValue = 1;
    }

    private void Reflect(Vector2 movement)
    {
        if (movement.x > 0 && !isRight || movement.x < 0 && isRight)
        {
            transform.localScale *= new Vector2(-1, 1);
            isRight = !isRight;
            Fear.FearValue = 0;
            startTime = Time.realtimeSinceStartup;
        }
    }
    
    public void PlayFootstep()
    {
        footstepSound.Play();
    }
}