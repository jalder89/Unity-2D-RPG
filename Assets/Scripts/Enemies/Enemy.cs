using UnityEngine;

public class Enemy : Entity
{
    override protected float moveSpeed 
    {
        get { return enemySpeed; }
        set { enemySpeed = value; }
    }

    override protected string entityName
    {
        get { return enemyName; }
        set { enemyName = value; }
    }

    override protected int health
    {
        get { return enemyHealth; }
        set { enemyHealth = value; }
    }

    [Header("Enemy Details")]
    [SerializeField] private string enemyName = "Enemy";
    [SerializeField] private int enemyHealth = 75;
    [SerializeField] private float enemySpeed = 2.0f;
    [SerializeField] protected float lastTimeWasDamaged;

    [Header("Attack Details")]
    [SerializeField] private float attackRadius;
    [SerializeField] private Transform attackPoint;
    [SerializeField] private LayerMask whatIsEnemy;
    [SerializeField] private int attackDamage = 10;
    [SerializeField] private float hurtFlashDuration = 0.1f;

    public Enemy()
    {
        // Default constructor logic if needed
        health = 100;
        moveSpeed = 3.0f;
    }

    virtual protected void Update()
    {
        Move();
        HurtFlashWhenHit();

        if (Input.GetKeyDown(KeyCode.F))
            Attack(); // Example condition to trigger attack
    }

    protected void Move()
    {
        // Logic for moving the enemy
        Debug.Log(enemyName + " is moving at speed: " + moveSpeed);
    }

    override protected void Attack()
    {
        // Logic for attacking
        Debug.Log(enemyName + " attacks!");
    }

    override public void TakeDamage(int damage)
    {
        spriteRenderer.color = Color.yellow;
        lastTimeWasDamaged = Time.time;
        health -= damage;

        Debug.Log(enemyName + " took " + damage + " damage. Remaining health: " + health);
        Debug.Log(gameObject.name + " took some damage");

        if (health <= 0)
        {
            Die();
        }
    }

    protected void HurtFlashWhenHit(Color? baseColor = null)
    {
        Color colorToUse = baseColor ?? Color.red;
        currentTimeInGame = Time.time;

        if (currentTimeInGame > lastTimeWasDamaged + hurtFlashDuration)
        {
            if (spriteRenderer.color != colorToUse)
            {
                TurnColor(colorToUse);
            }
        }
    }

    protected void TurnColor(Color color)
    {
        spriteRenderer.color = color;
    }

    override protected void Die()
    {
        Debug.Log(enemyName + " has died.");
        // Logic for enemy death, e.g., play animation, drop loot, etc.
        Destroy(gameObject); // Remove the enemy from the scene
    }
}
