using UnityEngine;

public class PlayerBasicAttackState : EntityState
{
    private float attackVelocityTimer;
    private const int FirstComboIndex = 1; // The first attack in the combo sequence, used in animator
    private int comboIndex = 1;
    private int comboLimit = 3;

    private float lastTimeAttacked;

    public PlayerBasicAttackState(Player player, StateMachine stateMachine, string animBoolName = "") : base(player, stateMachine, animBoolName)
    {
        if (comboLimit != player.attackVelocity.Length)
        {
            Debug.LogWarning("Combo limit does not match the length of attack velocities. Adjusting combo limit to match.");
            comboLimit = player.attackVelocity.Length; // Ensure combo limit matches the length of attack velocities
        }
    }

    public override void Enter()
    {
        base.Enter();

        ResetComboIndexIfNeeded();
        anim.SetInteger("basicAttackIndex", comboIndex);
        ApplyAttackVelocity();
    }

    private void ResetComboIndexIfNeeded()
    {
        if (comboIndex > comboLimit || Time.time > lastTimeAttacked + player.comboResetTime)
        {
            comboIndex = FirstComboIndex; // Reset to the first attack in the combo
        }
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

    public override void Exit()
    {
        base.Exit();

        comboIndex++;
        lastTimeAttacked = Time.time;
    }

    private void HandleAttackVelocity()
    {
        attackVelocityTimer -= Time.deltaTime;

        if (attackVelocityTimer <= 0f)
        {
            player.SetVelocity(0, player.rb.linearVelocityY);
        }
    }

    private void ApplyAttackVelocity()
    {
        attackVelocityTimer = player.attackVelocityDuration;
        player.SetVelocity(player.attackVelocity[comboIndex - 1].x * player.facingDirection, player.attackVelocity[comboIndex - 1].y);
    }
}
