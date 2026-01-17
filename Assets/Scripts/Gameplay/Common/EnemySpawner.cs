using NUnit;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public Transform player;
    public float spawnRadius = 10f;
    public float timeSpawn=2f;
    private void Start()
    {
        GameObject playerObj = GameObject.FindWithTag("Player");
        player = playerObj.transform;
    }
    private void Update()
    {
        timeSpawn -= Time.deltaTime;
        if(timeSpawn<=0)
        {
            SpawnEnemy();
            timeSpawn = 2f;
        }
    }

    public void SpawnEnemy()
    {
        float angle = Random.Range(0f, 360f);
        Vector2 spawnPos = new Vector2(
            Mathf.Cos(angle * Mathf.Deg2Rad),
            Mathf.Sin(angle * Mathf.Deg2Rad)
        ) * spawnRadius;

        Vector3 finalPos = player.position + (Vector3)spawnPos;
        GameObject enemyObj =ObjectPooler.Instance.SpawnFromPool("Mushroom", finalPos, Quaternion.identity);
        enemyObj.GetComponent<EnemyController>().currentHealth = enemyObj.GetComponent<EnemyController>().data.maxHealth;
    }
    public void OnDrawGizmosSelected()
    {
        if (player == null) return;
        Gizmos.DrawWireSphere(player.position, spawnRadius);
    }
}
