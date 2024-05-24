using UnityEngine;
using System.Collections;
using UnityEngine.Serialization;

// review(24.05.2024): Название класса не соответствует названию файла
public class PlayerController : MonoBehaviour
{
    [SerializeField] private float speed = 15;
    private Vector3 input;
    private bool isWalking; // review(24.05.2024): Если isWalking, isRunning, isCalm взаимоисключающие состояния, то стоит создать enum PlayerState и использовать его
    private bool isRunning;
    private CharacterAnimations animations;
    [SerializeField] private SpriteRenderer characterSprite;

    public float Speed // review(24.05.2024): Зачем потребовалось делать это свойством?
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
    
    private void FixedUpdate() => Update(); // review(24.05.2024): Разве в таком случае не будет вызываться Update слишком часто?

    // review(24.05.2024): Перемешана бизнес-логика и логика отображения. Давайте попробуем на уровне кода структурировать это
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

        float moveHorizontal = Input.GetAxis("Horizontal"); // review(24.05.2024): var
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

        // review(24.05.2024): Мне кажется, что за страх должен отвечать отдельный MonoBehavior
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