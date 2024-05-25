using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMovement : MonoBehaviour
{
    [SerializeField] private float speed;
    private Vector3 input;
    private bool isWalking;
    private bool isRunning;
    private Animator animator;
    private CharacterAnimations animations;
    [SerializeField] private SpriteRenderer characterSprite;

    private void Start()
    {
        animator = GetComponent<Animator>();
        animations = GetComponentInChildren<CharacterAnimations>();
    }

    private void FixedUpdate() => Move();

    // review(24.05.2024): Я уже видел этот код в PlayerController
    private void Move()
    {
        input = new Vector2(Input.GetAxis("Horizontal"), 0);
        isWalking = input.x != 0;
        isRunning = input.x != 0 && (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift));

        if (isWalking || isRunning)
        {
            var animationToPlay = isRunning ? "Run" : "Walk";
            var movementSpeed = isRunning ? 3f * speed : speed;
            animator.Play(animationToPlay);
            characterSprite.flipX = input.x > 0;
            transform.position += input * (movementSpeed * Time.deltaTime);
        }
        else
            animator.Play("Calm");
        
        animations.IsWalking = isWalking;
        animations.IsRunning = isRunning;
    }
}