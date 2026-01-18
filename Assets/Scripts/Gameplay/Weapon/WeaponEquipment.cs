using Unity.Jobs;
using UnityEngine;

public class WeaponEquipment : MonoBehaviour
{
    [SerializeField]
    private WeaponDatabase weaponDatabase;

    public WeaponData currentWeapon;

    private PlayerController player;

    public float timeShoot=0f;
    private void Start()
    {
        player = gameObject.GetComponent<PlayerController>();
        EquipWeapon();
        ObjectPooler.Instance.InitializePool(currentWeapon.weaponTag, currentWeapon.prefabBullet, 20);
    }

    private void Update()
    {
        HandleShooting();
    }
    private void HandleShooting()
    {
        timeShoot += Time.deltaTime;
        if(timeShoot>currentWeapon.fireRate)
        {
            Shoot();
            timeShoot = 0f;
        }
    }

    private void Shoot()
    {

        Debug.Log("thuc hien ban dan");

        // 1. Tìm kẻ địch gần nhất từ vị trí Player để xác định hướng ban đầu
        GameObject target = player.GetComponent<NearestEnemyFinder>().FindNearestEnemy(player.transform);

        Quaternion spawnRotation = Quaternion.identity;

        if (target != null)
        {
            Vector2 lookDirection = (target.transform.position - player.firePoint.transform.position).normalized;
            float angle = Mathf.Atan2(lookDirection.y, lookDirection.x) * Mathf.Rad2Deg;
            spawnRotation = Quaternion.Euler(0, 0, angle);
        }

        ObjectPooler.Instance.SpawnFromPool(currentWeapon.weaponTag, player.firePoint.transform.position, spawnRotation);
   
    }

    public void EquipWeapon() 
    {
        int currentWeaponIndex = PlayerPrefs.GetInt("WeaponIndex", 0);

        if (currentWeapon == weaponDatabase.GetWeapon(currentWeaponIndex)) return;

        currentWeapon = weaponDatabase.GetWeapon(currentWeaponIndex);
    }
}
