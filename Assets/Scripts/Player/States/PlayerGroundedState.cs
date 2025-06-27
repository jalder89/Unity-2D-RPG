using UnityEngine;

public class PlayerGroundedState : EntityState
{
    public PlayerGroundedState(Player player, StateMachine stateMachine, string animBoolName = "") : base(player, stateMachine, animBoolName)
    {
    }

    public override void Update()
    {
        base.Update();

        if (input.Player.Jump.WasPerformedThisFrame())
        {
            Debug.Log("Jumping from Grounded State!");
            stateMachine.ChangeState(player.jumpState);
        }
    }

    
}
