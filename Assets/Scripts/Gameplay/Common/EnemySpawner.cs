using NUnit;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public Transform player;
    public float spawnRadius;
    public float timeSpawn;
    public float time = 2f;
    public List<EnemyData> enemies;
    public List<GameObject> listGems;

    private void Awake()
    {
        // Ensure object pools are created early
        CreateObjectFromBool();

        // Get player reference from PlayerManager
        if (PlayerManager.Instance != null && PlayerManager.Instance.HasPlayer())
        {
            player = PlayerManager.Instance.Player.transform;
        }
    }

    private void Start()
    {
        // If player not found in Awake, try again in Start (fallback)
        if (player == null && PlayerManager.Instance != null)
        {
            PlayerController pc = PlayerManager.Instance.GetPlayer();
            if (pc != null)
            {
                player = pc.transform;
            }
        }

        if (player == null)
        {
            Debug.LogWarning("EnemySpawner: Player not found in Start.");
        }
    }

    private void Update()
    {
        time -= Time.deltaTime;
        if (time <= 0)
        {
            SpawnEnemy();
            time = timeSpawn;
        }
    }

    private void CreateObjectFromBool()
    {
        if (ObjectPooler.Instance == null)
        {
            Debug.LogError("EnemySpawner: ObjectPooler.Instance is null. Ensure ObjectPooler exists in the scene.");
            return;
        }

        if (enemies == null) return;

        for (int i = 0; i < enemies.Count; i++)
        {
            if (enemies[i] != null)
            {
                ObjectPooler.Instance.InitializePool(enemies[i].enemyTag, enemies[i].prefabEnemy, 20);
            }
        }
        for(int i = 0; i < listGems.Count; i++)
        {
            if (listGems[i] != null)
            {
                ObjectPooler.Instance.InitializePool(listGems[i].GetComponent<ExperienceGem>().tagGem, listGems[i], 20);
            }
        }
    }

    public void SpawnEnemy()
    {
        if (player == null)
        {
            Debug.LogWarning("EnemySpawner: player transform is null. Cannot spawn enemy reliably.");
            return;
        }

        if (enemies == null || enemies.Count == 0)
        {
            Debug.LogWarning("EnemySpawner: No enemies configured to spawn.");
            return;
        }

        float angle = Random.Range(0f, 360f);
        Vector2 spawnPos = new Vector2(
            Mathf.Cos(angle * Mathf.Deg2Rad),
            Mathf.Sin(angle * Mathf.Deg2Rad)
        ) * spawnRadius;

        Vector3 finalPos = player.position + (Vector3)spawnPos;
        if (ObjectPooler.Instance == null) return;

        GameObject enemyObj = ObjectPooler.Instance.SpawnFromPool(enemies[0].enemyTag, finalPos, Quaternion.identity);
        if (enemyObj != null)
        {
            EnemyController ec = enemyObj.GetComponent<EnemyController>();
            if (ec != null && ec.data != null)
            {
                ec.currentHealth = ec.data.maxHealth;
            }
        }
    }

    public void OnDrawGizmosSelected()
    {
        if (player == null) return;
        Gizmos.DrawWireSphere(player.position, spawnRadius);
    }
}
