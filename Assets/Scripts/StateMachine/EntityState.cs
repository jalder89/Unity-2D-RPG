using UnityEngine;

public abstract class EntityState
{
    protected Player player;
    protected StateMachine stateMachine;
    protected string animBoolName;
    protected Animator anim;
    protected Rigidbody2D rb;
    protected PlayerInputActions input;

    protected float stateTimer;
    protected bool triggerCalled;

    public EntityState(Player player, StateMachine stateMachine, string animBoolName = "")
    {
        this.player = player;
        this.stateMachine = stateMachine;
        this.animBoolName = animBoolName;
        anim = player.anim;
        rb = player.rb;
        input = player.input;
    }

    public virtual void Enter()
    {
        // Code to execute when entering the state
        Debug.Log($"Entering state: {animBoolName}");
        anim.SetBool(animBoolName, true);
        triggerCalled = false;
    }

    public virtual void Update()
    {
        // Code to execute during the state update
        anim.SetFloat("yVelocity", rb.linearVelocityY);

        if (input.Player.Dash.WasPressedThisFrame() && CanDash())
        {
            stateMachine.ChangeState(player.dashState);
        }

        stateTimer -= Time.deltaTime;
    }

    public void CallAnimationTrigger()
    {
        triggerCalled = true;
    }

    private bool CanDash()
    {
        // Check if the player can dash based on conditions like cooldown, input, etc.
        return !player.wallDetected && stateMachine.currentState != player.dashState;
    }

    public virtual void Exit()
    {
        // Code to execute when exiting the state
        Debug.Log($"Exiting state: {animBoolName}");
        anim.SetBool(animBoolName, false);
    }
}
