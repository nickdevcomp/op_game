using UnityEngine;
using System.Collections;
using UnityEngine.Serialization;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D rb;
    [SerializeField] private float speed = 5;
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


    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        startTime = Time.realtimeSinceStartup;
    }

    private void Update()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        Vector2 movement = new Vector2(moveHorizontal, 0);
        animator.SetFloat("moveX", Mathf.Abs(moveHorizontal));

        if (Input.GetKey(KeyCode.LeftShift))
        {
            animator.SetBool("running", true);
            rb.velocity = movement * speed * 2;
        }
        else
        {
            animator.SetBool("running", false);
            rb.velocity = movement * speed;
        }
        Reflect(movement);

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