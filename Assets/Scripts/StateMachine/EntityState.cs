using UnityEngine;

public abstract class EntityState
{
    protected Player player;
    protected StateMachine stateMachine;
    protected string animBoolName;
    protected Animator anim;

    public EntityState(Player player, StateMachine stateMachine, string animBoolName = "")
    {
        this.player = player;
        this.stateMachine = stateMachine;
        this.animBoolName = animBoolName;
        anim = player.anim;
    }

    public virtual void Enter()
    {
        // Code to execute when entering the state
        Debug.Log($"Entering state: {animBoolName}");
        anim.SetBool(animBoolName, true);
    }

    public virtual void Update()
    {
        // Code to execute during the state update
        Debug.Log($"Updating state: {animBoolName}");
    }

    public virtual void Exit()
    {
        // Code to execute when exiting the state
        Debug.Log($"Exiting state: {animBoolName}");
        anim.SetBool(animBoolName, false);
    }
}
