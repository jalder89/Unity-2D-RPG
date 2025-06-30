using UnityEngine;

public class PlayerDashState : EntityState
{
    private float originalGravityScale;
    private int dashDirection;

    public PlayerDashState(Player player, StateMachine stateMachine, string animBoolName = "") : base(player, stateMachine, animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();

        dashDirection = player.moveInput.x != 0 ? (int)player.moveInput.x : player.facingDirection;
        stateTimer = player.dashDuration;
        
        originalGravityScale = player.rb.gravityScale;
        player.rb.gravityScale = 0f; // Disable gravity during dash
    }

    public override void Update()
    {
        base.Update();

        CancelDashIfNeeded();
        player.rb.linearVelocity = new Vector2(player.dashSpeed * dashDirection, 0);

        if (stateTimer <= 0f)
        {
            if (player.groundDetected)
            {
                stateMachine.ChangeState(player.idleState);
            }
            else
            {
                stateMachine.ChangeState(player.fallState);
            }
        }
    }

    public override void Exit()
    {
        base.Exit();
        player.rb.linearVelocity = new Vector2(0, 0);
        player.rb.gravityScale = originalGravityScale; // Restore gravity after dash
    }

    private void CancelDashIfNeeded()
    {
        if (player.wallDetected)
        {
            if (player.groundDetected)
            {
                stateMachine.ChangeState(player.idleState);
            }
            else
            {
                stateMachine.ChangeState(player.wallSlideState);
            }
        }
    }
}
