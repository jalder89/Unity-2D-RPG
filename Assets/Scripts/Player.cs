using UnityEngine;

public class Player : MonoBehaviour
{
    private Animator anim;
    private Rigidbody2D rb;

    [Header("Movement Details")]
    [SerializeField] private float moveSpeed = 8.0f;
    [SerializeField] private int jumpForce = 8;
    private float xInput;

    [Header("Collision Details")]
    [SerializeField] private float groundCheckDistance = 1.4f;
    [SerializeField] private bool isGrounded;
    [SerializeField] private LayerMask whatIsGround;

    private bool facingRight = true;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponentInChildren<Animator>();
    }

    private void Update()
    {
        HandleInput();
        HandleMovement();
        HandleFlip();
        HandleCollision();
        HandleAnimations();
    }

    private void HandleAnimations()
    {
        anim.SetBool("isGrounded", isGrounded);
        anim.SetFloat("xVelocity", rb.linearVelocityX);
        anim.SetFloat("yVelocity", rb.linearVelocityY);
    }

    private void HandleInput()
    {
        xInput = Input.GetAxisRaw("Horizontal");
        Debug.Log("xInput: " + xInput);

        if (Input.GetButtonDown("Jump"))
        {
            Jump();
        }
    }

    private void HandleMovement()
    {
        rb.linearVelocity = new Vector2(xInput * moveSpeed, rb.linearVelocityY);
    }

    private void Jump()
    {
        if (isGrounded) {
            rb.linearVelocity = new Vector2(rb.linearVelocityX, jumpForce);
        }
    }

    private void HandleCollision()
    {
        isGrounded = Physics2D.Raycast(transform.position, Vector2.down, groundCheckDistance, whatIsGround);
    }

    private void HandleFlip()
    {
        if (rb.linearVelocityX > 0 && facingRight == false)
        {
            Flip();
        }
        else if (rb.linearVelocityX < 0 && facingRight == true)
        {
            Flip();
        }
    }

    private void Flip()
    {
        transform.Rotate(0, 180, 0);
        facingRight = !facingRight;
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawLine(transform.position, transform.position + new Vector3(0, -groundCheckDistance));
    }

}
