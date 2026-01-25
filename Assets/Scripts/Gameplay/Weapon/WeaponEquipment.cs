using UnityEngine;

public class WeaponEquipment : MonoBehaviour
{
    [SerializeField]
    private WeaponDatabase weaponDatabase;

    public WeaponData currentWeapon;

    private PlayerController player;

    public float timeShoot = 0f;

    private void Awake()
    {
        // Get player from PlayerManager as early as possible
        if (PlayerManager.Instance != null)
        {
            player = PlayerManager.Instance.Player;
        }
    }

    private void Start()
    {
        // Fallback if PlayerManager not available
        if (player == null)
        {
            player = gameObject.GetComponent<PlayerController>();
        }

        if (player == null)
        {
            Debug.LogError("WeaponEquipment: PlayerController not found!");
            return;
        }

        EquipWeapon();
        if (currentWeapon != null)
        {
            ObjectPooler.Instance.InitializePool(currentWeapon.weaponTag, currentWeapon.prefabBullet, 20);
        }
    }

    private void Update()
    {
        HandleShooting();
    }

    private void HandleShooting()
    {
        if (player == null || currentWeapon == null) return;

        timeShoot += Time.deltaTime;
        if (timeShoot > currentWeapon.fireRate)
        {
            Shoot();
            timeShoot = 0f;
        }
    }

    private void Shoot()
    {
        if (player == null)
        {
            Debug.LogWarning("WeaponEquipment: Player is null, cannot shoot.");
            return;
        }

        Debug.Log("thuc hien ban dan");

        // 1. Tìm kẻ địch gần nhất từ vị trí Player để xác định hướng ban đầu
        NearestEnemyFinder finder = player.GetComponent<NearestEnemyFinder>();
        GameObject target = finder != null ? finder.FindNearestEnemy(player.transform) : null;

        Quaternion spawnRotation = Quaternion.identity;

        if (target != null)
        {
            Vector2 lookDirection = (target.transform.position - player.firePoint.transform.position).normalized;
            float angle = Mathf.Atan2(lookDirection.y, lookDirection.x) * Mathf.Rad2Deg;
            spawnRotation = Quaternion.Euler(0, 0, angle);
        }

        if (ObjectPooler.Instance != null && currentWeapon != null)
        {
            ObjectPooler.Instance.SpawnFromPool(currentWeapon.weaponTag, player.firePoint.transform.position, spawnRotation);
        }
    }

    public void EquipWeapon()
    {
        if (weaponDatabase == null)
        {
            Debug.LogError("WeaponEquipment: WeaponDatabase not assigned!");
            return;
        }

        int currentWeaponIndex = PlayerPrefs.GetInt("WeaponIndex", 0);
        WeaponData weapon = weaponDatabase.GetWeapon(currentWeaponIndex);

        if (weapon == null) return;
        if (currentWeapon == weapon) return;

        currentWeapon = weapon;
    }
}
