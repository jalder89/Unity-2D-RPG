using UnityEngine;

public class Cooldown_Example : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;

    [SerializeField] private float hurtFlashDuration = 0.1f;

    public float currentTimeInGame;
    public float lastTimeWasDamaged;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        HurtFlashWhenHit();
    }

    private void HurtFlashWhenHit()
    {
        currentTimeInGame = Time.time;

        if (currentTimeInGame > lastTimeWasDamaged + hurtFlashDuration)
        {
            if (spriteRenderer.color != Color.red)
            {
                TurnRed();
            }
        }
    }

    public void TakeDamage()
    {
        spriteRenderer.color = Color.yellow;
        lastTimeWasDamaged = Time.time;
        Debug.Log(gameObject.name + " took some damage");
    }

    private void TurnRed()
    {
        spriteRenderer.color = Color.red;
    }
}
