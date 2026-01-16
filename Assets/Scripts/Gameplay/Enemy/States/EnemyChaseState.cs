using NUnit.Framework;
using System.Net;
using UnityEngine;

public class EnemyChaseState : BaseStateEnemy
{
    public EnemyChaseState(FiniteStateMachine finiteStateMachine, EnemyController enemy, Animator animator) : base(finiteStateMachine, enemy, animator)
    {
    }
    public PlayerController player;

    public override void Enter()
    {
        Debug.Log("Trang thai truy duoi");
        animator.CrossFade("run", 0.1f);
    }

    public override void LogicUpdate()
    {
        if (enemy.playerController == null)
        {
            finiteStateMachine.ChangeState(enemy.enemyIdleState);
            return;
        }
        float distance = Vector2.Distance(enemy.playerController.transform.position, enemy.transform.position);
        if(distance< enemy.data.attackRange)
        {
            Debug.Log("Doi sang trang thai tan cong");
            finiteStateMachine.ChangeState(enemy.enemyAttackState);
        }
    }

    public override void PhysicsUpdate()
    {
        if (enemy.playerController == null) return;
        Vector2 direction = (enemy.playerController.transform.position - enemy.transform.position).normalized;
        enemy.rb.linearVelocity = enemy.data.moveSpeed * direction;
        if (direction.x < 0) enemy.transform.localScale = new Vector3(-5, 5, 1);
        else if (direction.x > 0) enemy.transform.localScale = new Vector3(5,5,1);
    }
    public override void Exit()
    {
    }
}
