using UnityEngine;

public class PlayerAnimationTriggers : MonoBehaviour
{
    private Player player;

    public void Awake()
    {
        player = GetComponentInParent<Player>();
    }

    public void CurrentStateTrigger()
    {
        player.CallAnimationTrigger();
    }
}
