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

        if (player.rb.linearVelocityY < 0)
        {
            stateMachine.ChangeState(player.fallState);
        }
    }
}
