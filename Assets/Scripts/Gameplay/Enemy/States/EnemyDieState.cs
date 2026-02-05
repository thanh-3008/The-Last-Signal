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

        if (dieTimer <= 0 && counter == 0)
        {
            counter = 1;

            // 1. Lưu vị trí chết trước
            Vector2 deathPosition = enemy.transform.position;

            // 2. Spawn các cục EXP
            foreach (var drop in enemy.data.expDrops)
            {
                for (int i = 0; i < drop.amount; i++)
                {
                    Vector2 spawnPos = deathPosition + Random.insideUnitCircle * 0.5f;
                    string tag = drop.gemPrefab.GetComponent<ExperienceGem>().tagGem;
                    ObjectPooler.Instance.SpawnFromPool(tag, spawnPos, Quaternion.identity);
                }
            }

            // 3. Trả quái về pool sau cùng
            ObjectPooler.Instance.ReturnToPool(enemy.data.enemyTag, enemy.gameObject);
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }   
}
