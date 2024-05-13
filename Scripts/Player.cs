using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D rb;
    public float speed = 5;
    public Animator Animator;
    public bool isRight;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        Animator = GetComponent<Animator>();
    }

    private void Update()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        Vector2 movement = new Vector2(moveHorizontal, 0);
        Animator.SetFloat("moveX", Mathf.Abs(moveHorizontal));

        if (Input.GetKey(KeyCode.LeftShift))
        {
            Animator.SetBool("running", true);
            rb.velocity = movement * speed * 2;
        }
        else
        {
            Animator.SetBool("running", false);
            rb.velocity = movement * speed;
        }

        Reflect(movement);
    }

    private void Reflect(Vector2 movement)
    {
        if (movement.x > 0 && !isRight || movement.x < 0 && isRight)
        {
            transform.localScale *= new Vector2(-1, 1);
            isRight = !isRight;
        }
    }

}