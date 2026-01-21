using System.Runtime.InteropServices;
using UnityEngine;

public class DamagePopupManager : MonoBehaviour
{
    // Singleton Instance
    public static DamagePopupManager Instance { get; private set; }

    [Header("References")]
    [SerializeField] private GameObject pfDamagePopup; // Kéo Prefab vào đây
    [SerializeField]
    private string tag;
    [SerializeField]
    private int size;
    private void Awake()
    {
        // Khởi tạo Singleton
        if (Instance != null && Instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            Instance = this;
        }
        CreateObjectFromBool();
    }

    // Hàm gọi từ bất cứ đâu
    public void Create(Vector3 position, float damageAmount, bool isCriticalHit)
    {
        // Tạo Popup tại vị trí truyền vào
        // Quaternion.identity nghĩa là giữ nguyên góc xoay mặc định (không xoay)
        GameObject damagePopupTransform = ObjectPooler.Instance.SpawnFromPool(tag, position, Quaternion.identity);

        // Gọi hàm Setup
        DamagePopup damagePopup = damagePopupTransform.GetComponent<DamagePopup>();
        damagePopup.Setup(damageAmount, isCriticalHit);
    }

    public void CreateObjectFromBool()
    {
        if (ObjectPooler.Instance == null)
        {
            Debug.LogError("EnemySpawner: ObjectPooler.Instance is null. Ensure ObjectPooler exists in the scene.");
            return;
        }
         ObjectPooler.Instance.InitializePool(tag, pfDamagePopup, size);    
    }
}