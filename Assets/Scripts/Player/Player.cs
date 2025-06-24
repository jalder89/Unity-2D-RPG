using System;
using UnityEngine;

public class Player : MonoBehaviour
{
    private StateMachine stateMachine;
    public PlayerIdleState idleState { get; private set; }
    public PlayerMoveState moveState { get; private set; }

    private PlayerInputActions input;

    public Vector2 moveInput { get; private set; }

    private void Awake()
    {
        input = new PlayerInputActions();
        stateMachine = new StateMachine();
        idleState = new PlayerIdleState(this, stateMachine, "Idle");
        moveState = new PlayerMoveState(this, stateMachine, "Move");
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
