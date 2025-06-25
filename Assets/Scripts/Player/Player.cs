using System;
using UnityEngine;

public class Player : MonoBehaviour
{
    // Animation
    public Animator anim { get; private set; }
    public Rigidbody2D rb { get; private set; }

    // Input
    private PlayerInputActions input;
    public Vector2 moveInput { get; private set; }

    // State machine and states
    private StateMachine stateMachine;
    public PlayerIdleState idleState { get; private set; }
    public PlayerMoveState moveState { get; private set; }

    // Player state
    public float moveSpeed = 5f;
    private bool isFacingRight = true;

    private void Awake()
    {
        anim = GetComponentInChildren<Animator>();
        rb = GetComponent<Rigidbody2D>();
        input = new PlayerInputActions();
        stateMachine = new StateMachine();
        idleState = new PlayerIdleState(this, stateMachine, "idle");
        moveState = new PlayerMoveState(this, stateMachine, "move");
    }

    private void OnEnable()
    {
        input.Enable();
        input.Player.Move.performed += context => moveInput = context.ReadValue<Vector2>();
        input.Player.Move.canceled += context => moveInput = Vector2.zero;
    }

    private void Start()
    {
        stateMachine.Initialize(idleState);
    }

    private void Update()
    {
        stateMachine.UpdateActiveState();
    }

    private void OnDisable()
    {
        input.Disable();
    }

    public void SetVelocity(float xVelocity, float yVelocity)
    {
        rb.linearVelocity = new Vector2(xVelocity, yVelocity);
        HandleFlip(xVelocity);
    }

    private void HandleFlip(float xVelocity)
    {
        if (xVelocity > 0 && !isFacingRight || xVelocity < 0 && isFacingRight)
        {
            FlipSprite();
        }
    }

    private void FlipSprite()
    {
        if (moveInput.x > 0 && !isFacingRight)
        {
            isFacingRight = !isFacingRight;
            transform.Rotate(new Vector3(0, 180, 0));
        }
        else if (moveInput.x < 0 && isFacingRight)
        {
            isFacingRight = !isFacingRight;
            transform.Rotate(new Vector3(0, 180, 0));
        }
    }
}
