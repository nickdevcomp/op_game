using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMovement : MonoBehaviour
{
    [SerializeField] private float speed;
    private Vector3 movement;
    private bool isWalking;
    private bool isRunning;
    private Animator animator;
    private CharacterAnimations animations;
    private float startTime;
    private float endTime;
    private float elapsedTime;
    private float timer = 0f;
    private bool isRight;
    [SerializeField] private SpriteRenderer characterSprite;


    private void Start()
    {
        animator = GetComponent<Animator>();
        animations = GetComponentInChildren<CharacterAnimations>();
        startTime = Time.realtimeSinceStartup;
        animator.Play("Calm");
    }

    private void FixedUpdate() => Move();

    private void Move()
    {
        movement = new Vector2(Input.GetAxis("Horizontal"), 0);
        isWalking = movement.x != 0;
        isRunning = movement.x != 0 && (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift));
    
        if (isWalking || isRunning)
        {
            var animationToPlay = isRunning ? "Run" : "Walk";
            var movementSpeed = isRunning ? 3f * speed : speed;
            animator.Play(animationToPlay);
            transform.position += movement * (movementSpeed * Time.deltaTime);
        }
        else
            animator.Play("Calm");
        
        Reflect(movement);

        
        animations.IsWalking = isWalking;
        animations.IsRunning = isRunning;
        
        endTime = Time.realtimeSinceStartup;
        elapsedTime = endTime - startTime;

        if (elapsedTime >= 5f)
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