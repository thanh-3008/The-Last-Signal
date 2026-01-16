using UnityEngine;

public class EnemyAttackState : BaseStateEnemy
{
    public EnemyAttackState(FiniteStateMachine finiteStateMachine, EnemyController enemy, Animator animator) : base(finiteStateMachine, enemy, animator)
    {
    }

    public bool isFinishAttack;
    public override void Enter()
    {
        isFinishAttack = false;
        enemy.rb.linearVelocity = Vector2.zero;
        animator.CrossFade("attack", 0.1f);
        Debug.Log("Enemy attack");
    }

    public override void LogicUpdate()
    {
        if(isFinishAttack)
        {
            finiteStateMachine.ChangeState(enemy.enemyIdleState);
        }
    }

    public override void PhysicsUpdate()
    {
        
    }

    public override void Exit()
    {

    }
}
