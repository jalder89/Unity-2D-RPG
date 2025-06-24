using UnityEngine;

public class PlayerIdleState : EntityState
{
    private float xInput;
    private float yInput;

    public PlayerIdleState(Player player, StateMachine stateMachine, string stateName = "") : base(player, stateMachine, stateName)
    {
    }

    public override void Update()
    {
        base.Update();

        if (player.moveInput.x != 0)
        {
            stateMachine.ChangeState(player.moveState);
        }
    }
}
