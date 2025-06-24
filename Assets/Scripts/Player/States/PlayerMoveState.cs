using UnityEngine;

public class PlayerMoveState : EntityState
{
    private bool isFacingRight = true;

    public PlayerMoveState(Player player, StateMachine stateMachine, string stateName = "") : base(player, stateMachine, stateName)
    {
    }

    public override void Update()
    {
        base.Update();
        FlipSprite();
        if (player.moveInput.x == 0)
        {
            stateMachine.ChangeState(player.idleState);
        }
    }

    public override void Exit()
    {
        base.Exit();
        Debug.Log("Time for a break!");
    }

    private void FlipSprite()
    {
        if (player.moveInput.x > 0 && !isFacingRight)
        {
            isFacingRight = !isFacingRight;
            player.transform.Rotate(new Vector3(0, 180, 0));
        }
        else if (player.moveInput.x < 0 && isFacingRight)
        {
            isFacingRight = !isFacingRight;
            player.transform.Rotate(new Vector3(0, 180, 0));
        }
    }
}
