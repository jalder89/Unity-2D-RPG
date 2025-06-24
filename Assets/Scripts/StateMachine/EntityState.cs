using UnityEngine;

public abstract class EntityState
{
    protected Player player;
    protected StateMachine stateMachine;
    protected string stateName;

    public EntityState(Player player, StateMachine stateMachine, string stateName = "")
    {
        this.player = player;
        this.stateMachine = stateMachine;
        this.stateName = stateName;
    }

    public virtual void Enter()
    {
        // Code to execute when entering the state
        Debug.Log($"Entering state: {stateName}");
    }

    public virtual void Update()
    {
        // Code to execute during the state update
        Debug.Log($"Updating state: {stateName}");
    }

    public virtual void Exit()
    {
        // Code to execute when exiting the state
        Debug.Log($"Exiting state: {stateName}");
    }
}
