using UnityEngine;

public class PlayerBasicAttackState : EntityState
{
    private float attackVelocityTimer;

    public PlayerBasicAttackState(Player player, StateMachine stateMachine, string animBoolName = "") : base(player, stateMachine, animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();

        GenerateAttackVelocity();
    }

    public override void Update()
    {
        base.Update();

        HandleAttackVelocity();
        // Todo: Add attack logic here (e.g., hit detection, damage application)

        if (triggerCalled)
        {
            stateMachine.ChangeState(player.idleState);
        }
    }

    private void HandleAttackVelocity()
    {
        attackVelocityTimer -= Time.deltaTime;

        if (attackVelocityTimer <= 0f)
        {
            player.SetVelocity(0, player.rb.linearVelocityY);
        }
    }

    private void GenerateAttackVelocity()
    {
        attackVelocityTimer = player.attackVeloictyDuration;
        player.SetVelocity(player.attackVelocity.x * player.facingDirection, player.attackVelocity.y);
    }
}
