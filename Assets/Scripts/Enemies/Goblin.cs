using UnityEngine;

public class Goblin : Enemy
{
    public Goblin()
    {
        // Default constructor logic if needed
        health = 25;
        moveSpeed = 10.0f;
        entityName = "Goblin";
    }

    override protected void Update()
    {
        Move();
        HurtFlashWhenHit(new Color(62f / 255f, 185f / 255f, 74f / 255f, 1f)); // Custom color for Goblin
        if (Input.GetKeyDown(KeyCode.F))
            Attack(); // Example condition to trigger attack
    }

    override protected void Attack()
    {
        // Custom attack logic for Goblin
        Debug.Log(entityName + " slashes with a dagger!");
        StealMoney();

        // Example of dealing damage to the player
        // Collider2D[] hitPlayers = Physics2D.OverlapCircleAll(attackPoint.position, attackRadius, whatIsEnemy);
        // foreach (Collider2D player in hitPlayers)
        // {
        //     player.GetComponent<Player>().TakeDamage(attackDamage);
        // }
    }

    override public void TakeDamage(int damage)
    {
        base.TakeDamage(damage);
        // Additional logic specific to Goblin when it takes damage
        Debug.Log(entityName + " is hurt and growls!");

        // Example of a special effect or sound
        // PlayHurtSound();
    }

    private void StealMoney()
    {
        // Logic for stealing money from the player
        Debug.Log(entityName + " steals some coins!");
        // Example: Player.Instance.StealCoins(10);
    }

}
