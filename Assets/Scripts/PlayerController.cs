using System.Drawing;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

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
    public Text quantity;

    public static int Ticket;
    public static int Dkr;
    public static int Ship;
    public static int Morsynka;
    public static int Balance = 0;
    public static int Feather;


    [SerializeField] 
    private bool isScaryFloor;
    
    [SerializeField] 
    private SpriteRenderer characterSprite; 
    
    private void Start()
    {
        animator = GetComponent<Animator>();
        startTime = Time.realtimeSinceStartup;
    }
    
    private void Update()
    {
        MoveCharacter();
        UpdateFear();
        UpdateQuantity();
    }

    private void UpdateQuantity()
    {
        quantity.text = "<size=10><color=#fff>x " + Balance.ToString() + "</color></size>";
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
}