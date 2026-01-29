using System.Collections.Generic;
using UnityEngine;

public class CHAINSPARK : WeaponBase
{
    public int bounceCount = 0;
    int bounceCountMax = 30;
    List<GameObject> hitEnemies = new List<GameObject>();

    protected override void Awake()
    {
        base.Awake();
        ResetChain();
    }

    protected override void Start()
    {
        base.Start();
        // ensure chain is reset when starting
        ResetChain();
    }

    protected override void OnEnable()
    {
        base.OnEnable();
        ResetChain();
    }

    protected override void Update()
    {
        base.Update();
        BulletMove();
    }

    private void ResetChain()
    {
        bounceCount = 0;
        hitEnemies.Clear();
    }

    protected void BulletMove()
    {
        // Di chuyển đạn theo hướng hiện tại
        Vector3 move = (Vector3)direction * data.moveSpeed * Time.deltaTime;
        transform.position += move;

        // Xoay đạn theo hướng di chuyển
        if (direction != Vector2.zero)
        {
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0, 0, angle);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Monster"))
        {
            GameObject enemy = collision.gameObject;
            if (!hitEnemies.Contains(enemy))
            {
                EnemyController enemyController = enemy.GetComponent<EnemyController>();
                if (enemyController != null && player != null)
                {
                    enemyController.TakeDamage(GetDameRaw());
                    int finalDamage = Mathf.RoundToInt(enemyController.GetFinalDamage(GetDameRaw()));
                    DamagePopupManager.Instance.Create(enemy.transform.position, finalDamage, isCritical);              
                }

                hitEnemies.Add(enemy);
                if (bounceCount < bounceCountMax)
                {
                    //nếu còn lượt nảy thì nảy tới mục tiêu tiếp theo
                    BounceToNextEnemy(enemy.transform);
                }
                else
                {
                    //Hết số lần nảy
                    ObjectPooler.Instance.ReturnToPool(data.weaponTag, gameObject);
                }
            }
            else return;
        }
    }
    private void BounceToNextEnemy(Transform currentTarget)
    {
        bounceCount++;

        GameObject nextEnemy = EnemyManager.Instance.GetNearestEnemy(currentTarget.position, hitEnemies);
        if (nextEnemy != null && !hitEnemies.Contains(nextEnemy))
        {
            // Cập nhật lại hướng bay mới về phía kẻ địch tiếp theo
            direction = (nextEnemy.transform.position - currentTarget.position).normalized;
        }
        else
        {
            // Không tìm thấy mục tiêu mới hoặc mục tiêu đã trúng rồi
            Debug.Log("Muc tieu da trung dan nay hoac ko co muc tieu");
            ObjectPooler.Instance.ReturnToPool(data.weaponTag, gameObject);
        }
    }
}
