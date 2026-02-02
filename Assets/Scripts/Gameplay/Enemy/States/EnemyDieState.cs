using System.Collections;
using UnityEngine;

public class EnemyDieState : BaseStateEnemy
{
    private float dieTimer;
    private int counter;
    public EnemyDieState(FiniteStateMachine finiteStateMachine, EnemyController enemy, Animator animator) : base(finiteStateMachine, enemy, animator)
    {
    }

    public override void Enter()
    {
        
        enemy.rb.linearVelocity = Vector2.zero;
        animator.Play("die");
        dieTimer = 1f;
        counter = 0;
        
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicUpdate()
    {
        dieTimer -= Time.deltaTime;

        if (dieTimer <= 0 && counter==0)
        {
            ObjectPooler.Instance.ReturnToPool(enemy.data.enemyTag, enemy.gameObject);
            counter = 1;
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }   
}
