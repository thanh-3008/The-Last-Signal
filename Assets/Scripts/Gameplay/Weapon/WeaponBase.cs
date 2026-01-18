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
    protected virtual void Start()
    {
        GetComponent();
    }

    protected virtual void Update()
    {
        ReturnObjectToPool();
    }

    protected virtual void OnEnable()
    {
        // Gọi GetComponent để đảm bảo đã có tham chiếu tới player và finder
        GetComponent();

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
    protected virtual void GetComponent() 
    {
        GameObject playerObj = GameObject.FindWithTag("Player");
        if (playerObj != null)
        {
            player = playerObj.GetComponent<PlayerController>();
            // Lấy Finder từ Player thay vì tự GetComponent trên viên đạn
            nearestEnemyFinder = playerObj.GetComponent<NearestEnemyFinder>();
        }
    }
}