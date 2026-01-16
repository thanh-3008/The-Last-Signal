using MyGame.Core.Interface;
using NUnit.Framework;
using UnityEngine;

public class BaseStateEnemy : IState
{
    protected FiniteStateMachine finiteStateMachine;

    protected EnemyController enemy;

    protected Animator animator;

    public BaseStateEnemy(FiniteStateMachine finiteStateMachine, EnemyController enemy, Animator animator)
    {
        this.finiteStateMachine = finiteStateMachine;
        this.enemy = enemy;
        this.animator = animator;
    }

    public virtual void Enter()
    { }

    public virtual void Exit()
    { }

    public virtual void LogicUpdate()
    { }

    public virtual void PhysicsUpdate()
    { }
    
}
