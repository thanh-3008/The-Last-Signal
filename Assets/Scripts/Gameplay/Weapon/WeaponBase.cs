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
        GameObject playerObj = GameObject.FindWithTag("Player");
        if (playerObj != null)
        {
            player = playerObj.GetComponent<PlayerController>();
            // Lấy Finder từ Player thay vì tự LoadComponent trên viên đạn
            nearestEnemyFinder = playerObj.GetComponent<NearestEnemyFinder>();
        }
    }
}