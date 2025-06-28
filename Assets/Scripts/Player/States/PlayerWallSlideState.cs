using UnityEngine;

public class PlayerWallSlideState : EntityState
{
    public PlayerWallSlideState(Player player, StateMachine stateMachine, string animBoolName = "") : base(player, stateMachine, animBoolName)
    {
    }

    public override void Update()
    {
        base.Update();
        HandleWallSlide();

        if (input.Player.Jump.WasPressedThisFrame())
        {
            stateMachine.ChangeState(player.wallJumpState);
        }

        if (!player.wallDetected)
        {
            // If no wall detected, change to fall state
            stateMachine.ChangeState(player.fallState);
        }

        // If the player is grounded, change to idle state
        if (player.groundDetected)
        {
            stateMachine.ChangeState(player.idleState);
            player.FlipSprite();
        }
    }

    private void HandleWallSlide()
    {
        if (player.moveInput.y < 0)
        {
            // If player is pressing down, slide down wall faster
            player.SetVelocity(player.moveInput.x, player.rb.linearVelocityY);
        }
        else
        {
            // Slow slide
            player.SetVelocity(player.moveInput.x, player.rb.linearVelocityY * player.wallSlideSlowMultiplier);
        }
    }

}
