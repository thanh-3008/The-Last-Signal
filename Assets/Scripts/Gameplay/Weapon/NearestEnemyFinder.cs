using UnityEngine;

public class NearestEnemyFinder : MonoBehaviour
{
    protected GameObject FindNearestEnemy(Transform currentTransform)
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Monster");

        if (enemies.Length == 0) return null;

        float minDistance = Mathf.Infinity;
        GameObject nearest = null;
        foreach (GameObject enemy in enemies)
        {
            if (enemy == null) continue;
            float distance = Vector2.Distance(currentTransform.position, enemy.transform.position);
            if(distance<minDistance)
            {
                minDistance = distance;
                nearest = enemy;
            }
        }
        return nearest;       
    }
}
