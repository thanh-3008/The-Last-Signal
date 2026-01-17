using UnityEngine;

public class EnemyIdleState : BaseStateEnemy
{
    public EnemyIdleState(FiniteStateMachine finiteStateMachine, EnemyController enemy, Animator animator) : base(finiteStateMachine, enemy, animator)
    {
    }
    public float cooldownTime;
    public override void Enter()
    {
        cooldownTime = enemy.data.attackCooldown;
        enemy.rb.linearVelocity = Vector2.zero;
        animator.CrossFade("idle", 0.1f);
    }

    public override void LogicUpdate()
    {
        cooldownTime -= Time.deltaTime;
        if(cooldownTime<=0)
        {
            if(enemy.playerController!=null && enemy.playerController != null)
            finiteStateMachine.ChangeState(enemy.enemyChaseState);
        }
    }

    public override void PhysicsUpdate()
    {
    }
    public override void Exit()
    {
    }
}
