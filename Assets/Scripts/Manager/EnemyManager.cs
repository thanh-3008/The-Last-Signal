using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    public static EnemyManager Instance { get; private set; }

    //danh sách các enemy đang sống
    public List<EnemyController> activeEnemies = new List<EnemyController>();

    private void Awake()
    {
        if(Instance==null)
        {
            Instance = this;
        }
    }

    //Hàm sẽ gọi khi enemy enable
    public void RegisterEnemy(EnemyController enemy)
    {
        if (!activeEnemies.Contains(enemy)) { activeEnemies.Add(enemy); }
    }

    //Hàm sẽ gọi khi enemy disable hoặc die
    public void UnregisterEnemy(EnemyController enemy)
    {
        if (activeEnemies.Contains(enemy)) { activeEnemies.Remove(enemy); }
    }
    public GameObject GetNearestEnemy(Vector3 posision, List<GameObject> ignoredEnemies)
    {
        GameObject nearst = null;

        float minDistance = Mathf.Infinity;
        // Tìm enemy có khoảng cách gần vị trí hiện tại nhất
        foreach (EnemyController enemy in activeEnemies) 
        {
            //Bỏ qua các enemy đã chết hoặc đang tắt
            if (enemy == null || !enemy.gameObject.activeInHierarchy) continue;
            //Bỏ qua các enemy trong danh sách bỏ qua
            if (ignoredEnemies.Contains(enemy.gameObject)) continue;

            float distance = Vector2.Distance(enemy.transform.position, posision);

            if (distance < minDistance)
            {
                minDistance = distance;
                nearst = enemy.gameObject;
            }
        }
        return nearst;
    }
}
