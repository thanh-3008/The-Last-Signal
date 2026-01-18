using NUnit.Framework;
using UnityEngine;

public class EnemyDieState : BaseStateEnemy
{
    public EnemyDieState(FiniteStateMachine finiteStateMachine, EnemyController enemy, Animator animator) : base(finiteStateMachine, enemy, animator)
    {
    }

    public override void Enter()
    {
        enemy.rb.linearVelocity = Vector2.zero;
        animator.Play("die");
        ObjectPooler.Instance.ReturnToPool(enemy.data.enemyTag,enemy.gameObject);
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }

}
