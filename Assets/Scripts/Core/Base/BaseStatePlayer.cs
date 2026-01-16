using NUnit.Framework;
using UnityEngine;
using MyGame.Core.Interface;
public class BaseStatePlayer : IState
{
    protected PlayerController player;
    protected FiniteStateMachine playerStateMachine;
    protected Animator animator;

    public BaseStatePlayer(PlayerController player, FiniteStateMachine playerStateMachine, Animator animator)
    {
        this.player = player;
        this.playerStateMachine = playerStateMachine;
        this.animator = animator;
    }

    public virtual void Enter() { }

    public virtual void Exit() { }

    public virtual void LogicUpdate() { }

    public virtual void PhysicsUpdate() { }
}
