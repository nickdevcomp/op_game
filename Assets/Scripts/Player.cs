using UnityEngine;
using System.Collections;
using UnityEngine.Serialization;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float speed = 15;
    private Vector3 input;
    private bool isWalking;
    private bool isRunning;
    private CharacterAnimations animations;
    [SerializeField] private SpriteRenderer characterSprite;

    public float Speed
    {
        get => speed;
        set => speed = value;
    }
    
    private Animator animator;
    private bool isRight;
    private float timer = 0f;
    private float startTime;
    private float endTime;
    private float elapsedTime;
    private Rigidbody2D rb;

    public static int ticket = 0;
    public static int dkr = 0;


    private void Start()
    {
        animator = GetComponent<Animator>();
        startTime = Time.realtimeSinceStartup;
        rb = new Rigidbody2D();
    }
    
    private void FixedUpdate() => Update();

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.T))
        {
            ticket = 1;
        }

        if (Input.GetKeyDown(KeyCode.Y))
        {
            dkr = 1;
        }

        float moveHorizontal = Input.GetAxis("Horizontal");
        input = new Vector2(moveHorizontal, 0);
        animator.SetFloat("moveX", Mathf.Abs(moveHorizontal));
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

        endTime = Time.realtimeSinceStartup;
        elapsedTime = endTime - startTime;
        if (elapsedTime >= 10f)
            Fear.sharedValue = 1;
    }

    private void Reflect(Vector2 movement)
    {
        if (movement.x > 0 && !isRight || movement.x < 0 && isRight)
        {
            transform.localScale *= new Vector2(-1, 1);
            isRight = !isRight;
            Fear.sharedValue = 0;
            startTime = Time.realtimeSinceStartup;
        }
    }
}