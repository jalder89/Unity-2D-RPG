using Unity.VisualScripting;
using UnityEngine;

public class PlayerMoveState : PlayerGroundedState
{
    public PlayerMoveState(Player player, StateMachine stateMachine, string stateName = "") : base(player, stateMachine, stateName)
    {
    }

    public override void Update()
    {
        base.Update();
        
        // If player stops or is against a wall, change to idle state
        if (player.moveInput.x == 0 || player.wallDetected)
        {
            stateMachine.ChangeState(player.idleState);
        }

        player.SetVelocity(player.moveInput.x * player.moveSpeed, rb.linearVelocityY);
    }

    public override void Exit()
    {
        base.Exit();
        Debug.Log("Time for a break!");
    }

    
}
