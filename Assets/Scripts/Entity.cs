using UnityEngine;

public class Entity : MonoBehaviour
{
    protected SpriteRenderer spriteRenderer;

    public float currentTimeInGame;

    virtual protected float moveSpeed
    {
        get { return 5f; } // Default move speed, can be overridden in derived classes
        set { moveSpeed = value; }
    }

    virtual protected string entityName
    {
        get { return "Entity"; } // Default entity name, can be overridden in derived classes
        set { entityName = value; }
    }

    [Header("Entity Details")]
    virtual protected int health
    {
        get { return 100; } // Default health, can be overridden in derived classes
        set { health = value; }
    }

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        Move();

        if (Input.GetKeyDown(KeyCode.F))
            Attack(); // Example condition to trigger attack
    }

    private void Move()
    {
        // Logic for moving the entity
        Debug.Log(entityName + " is moving at speed: " + moveSpeed);
    }

    virtual protected void Attack()
    {
        // Logic for attacking
        Debug.Log(entityName + " attacks!");
    }

    virtual public void TakeDamage(int damage)
    {
        health -= damage;
        Debug.Log(entityName + " took " + damage + " damage. Remaining health: " + health);

        if (health <= 0)
        {
            Die();
        }
    }

    virtual protected void Die()
    {
        Debug.Log(entityName + " has died.");
        // Logic for entity death, e.g., play animation, drop loot, etc.
        Destroy(gameObject); // Remove the entity from the scene
    }
}
