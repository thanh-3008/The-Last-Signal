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
    private void Awake()
    {
        CreateObjectFromBool();
    }
    private void Start()
    {
        GameObject playerObj = GameObject.FindWithTag("Player");
        player = playerObj.transform;
    }
    private void Update()
    {
        time -= Time.deltaTime;
        if(time<=0)
        {
            SpawnEnemy();
            time = timeSpawn;
        }
    }
    private void CreateObjectFromBool()
    {
        for(int i=0;i<enemies.Count;i++)
        {
            ObjectPooler.Instance.InitializePool(enemies[i].enemyTag, enemies[i].prefabEnemy, 20);
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
        GameObject enemyObj = ObjectPooler.Instance.SpawnFromPool(enemies[0].enemyTag, finalPos, Quaternion.identity);
        enemyObj.GetComponent<EnemyController>().currentHealth = enemyObj.GetComponent<EnemyController>().data.maxHealth;
    }
    public void OnDrawGizmosSelected()
    {
        if (player == null) return;
        Gizmos.DrawWireSphere(player.position, spawnRadius);
    }
}
