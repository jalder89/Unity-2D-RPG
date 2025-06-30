using UnityEngine;

public class PlayerBasicAttackState : EntityState
{
    private float attackVelocityTimer;
    private float lastTimeAttacked;

    private bool comboAttackQueued;
    private int attackDirection;
    private int comboIndex = 1;
    private int comboLimit = 3;
    private const int FirstComboIndex = 1; // The first attack in the combo sequence, used in animator


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

        comboAttackQueued = false;
        ResetComboIndexIfNeeded();
        
        // Define the attack direction based on player input
        attackDirection = player.moveInput.x != 0 ? (int)player.moveInput.x : player.facingDirection;

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
        if (input.Player.Attack.WasPressedThisFrame())
        {
            QueueNextComboAttack();
        }
        if (triggerCalled)
        {
            HandleStateExit();
        }
    }

    public override void Exit()
    {
        base.Exit();

        comboIndex++;
        lastTimeAttacked = Time.time;
    }

    private void HandleStateExit()
    {
         if (comboAttackQueued)
        {
            anim.SetBool(animBoolName, false);
            player.EnterAttackStateWithDelay();
        }
        else
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

    private void QueueNextComboAttack()
    {
        if (comboIndex < comboLimit)
        {
            comboAttackQueued = true;
        }
    }

    private void ApplyAttackVelocity()
    {
        attackVelocityTimer = player.attackVelocityDuration;
        player.SetVelocity(player.attackVelocity[comboIndex - 1].x * attackDirection, player.attackVelocity[comboIndex - 1].y);
    }
}
