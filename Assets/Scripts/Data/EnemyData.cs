using UnityEngine;

[CreateAssetMenu (fileName = "NewEnemyData",menuName = "GameData/EnemyData")]
public class EnemyData : EntityData
{
    public float attackRange;

    public float attackCooldown;

    public GameObject prefabEnemy;
}
