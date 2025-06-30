using UnityEngine;

public class PlayerJumpState : PlayerAiredState
{
    public PlayerJumpState(Player player, StateMachine stateMachine, string animBoolName = "") : base(player, stateMachine, animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();

        Debug.Log("Jumping!");
        player.SetVelocity(rb.linearVelocityX, player.jumpForce);

    }

    public override void Update()
    {
        base.Update();

        // Ensure the player is not in jump attack state when before transitioning to fall state
        if (player.rb.linearVelocityY < 0 && stateMachine.currentState != player.jumpAttackState)
        {
            stateMachine.ChangeState(player.fallState);
        }
    }
}
