using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu (fileName = "NewEnemyData",menuName = "GameData/EnemyData")]
public class EnemyData : EntityData
{
    public float attackRange;

    public float attackCooldown;

    public GameObject prefabEnemy;

    public string enemyTag;

    [System.Serializable]
    public class ExpDropConfig
    {
        public GameObject gemPrefab; // Kéo Prefab viên đá vào đây
        public int amount;           // Số lượng rơi ra
    }

    // Trong PlayerData hoặc EnemyData của bạn:
    [Header("Drops")]
    public List<ExpDropConfig> expDrops;

    public float coinDrop;
}
