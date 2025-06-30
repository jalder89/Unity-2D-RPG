using UnityEngine;

public class PlayerFallState : PlayerAiredState
{
    private float originalGravityScale;
    public PlayerFallState(Player player, StateMachine stateMachine, string animBoolName = "") : base(player, stateMachine, animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        originalGravityScale = player.rb.gravityScale;
        player.rb.gravityScale = originalGravityScale * 1.5f; // Increase gravity for falling
    }

    public override void Update()
    {
        base.Update();

        if (player.groundDetected)
        {
            stateMachine.ChangeState(player.idleState);
        }

        if (player.wallDetected)
        {
            stateMachine.ChangeState(player.wallSlideState);
        }
    }

    public override void Exit()
    {
        base.Exit();
        player.rb.gravityScale = originalGravityScale; // Restore original gravity when exiting fall state
    }
}
