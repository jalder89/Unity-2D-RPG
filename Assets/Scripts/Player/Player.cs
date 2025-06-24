using System;
using UnityEngine;

public class Player : MonoBehaviour
{
    // Animation
    public Animator anim { get; private set; }

    // Input
    private PlayerInputActions input;
    public Vector2 moveInput { get; private set; }

    // State machine and states
    private StateMachine stateMachine;
    public PlayerIdleState idleState { get; private set; }
    public PlayerMoveState moveState { get; private set; }

    private void Awake()
    {
        anim = GetComponentInChildren<Animator>();
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
}
