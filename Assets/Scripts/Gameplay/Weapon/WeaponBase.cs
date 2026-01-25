using UnityEngine;

public class WeaponBase : MonoBehaviour
{
    [SerializeField]
    private WeaponData weaponData;
    public WeaponData data => weaponData;
    protected NearestEnemyFinder nearestEnemyFinder;
    protected Vector2 direction;
    protected PlayerController player;
    private float timeDestroy = 0f;
    protected bool isCritical = false;

    // Ensure components are loaded early in Awake
    protected virtual void Awake()
    {
        LoadComponents();
    }

    protected virtual void Start()
    {
        // Kept for derived classes to override. Core LoadComponents moved to Awake.
    }

    protected virtual void Update()
    {
        ReturnObjectToPool();
    }

    protected virtual void OnEnable()
    {
        // Ensure references are present when object is enabled (pooled objects)
        LoadComponents();

        if (player != null)
        {
            // Xác định hướng bay ngay lập tức khi đạn vừa được lấy ra khỏi Pool
            FindEnemyNearestDirection(transform);
        }
    }

    private void ReturnObjectToPool()
    {
        timeDestroy += Time.deltaTime;
        if (timeDestroy >= 10f)
        {
            ObjectPooler.Instance.ReturnToPool(weaponData.weaponTag, gameObject);
            timeDestroy = 0f;
        }
    }

    protected virtual Vector2 FindEnemyNearestDirection(Transform transform)
    {
        if (nearestEnemyFinder == null)
        {
            direction = Random.insideUnitCircle.normalized;
            return direction;
        }

        GameObject targetObj = nearestEnemyFinder.FindNearestEnemy(transform);
        if (targetObj == null)
        {
            direction = Random.insideUnitCircle.normalized;
        }
        else
        {
            direction = (targetObj.transform.position - transform.position).normalized;
        }
        return direction;
    }

    protected virtual void LoadComponents()
    {
        // Get player from PlayerManager instead of FindWithTag
        if (PlayerManager.Instance != null && PlayerManager.Instance.HasPlayer())
        {
            player = PlayerManager.Instance.Player;
            // Lấy Finder từ Player
            nearestEnemyFinder = player.GetComponent<NearestEnemyFinder>();
        }
    }

    protected virtual float GetDameRaw()
    {
        if (player == null || player.data == null)
        {
            Debug.LogWarning("WeaponBase: Player or PlayerData is null!");
            return 0f;
        }

        isCritical = false;

        float damage = player.data.dameBase * data.damageMultiplier;

        int randomValue = Random.Range(0, 101);
        if (randomValue < player.data.critChance)
        {
            isCritical = true;
            damage = (damage * player.data.critDame) / 100;
            Debug.Log("Crit dame cua nhan vat la" + player.data.critDame);
            return damage;
        }
        else
        {
            Debug.Log("dame cua nhan vat la" + damage);
            return damage;
        }
    }
}