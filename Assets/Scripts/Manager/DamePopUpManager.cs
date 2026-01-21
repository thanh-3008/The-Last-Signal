using UnityEngine;

public class DamagePopupManager : MonoBehaviour
{
    // Singleton Instance
    public static DamagePopupManager Instance { get; private set; }

    [Header("References")]
    [SerializeField] private Transform pfDamagePopup; // Kéo Prefab vào đây

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
    }

    // Hàm gọi từ bất cứ đâu
    public void Create(Vector3 position, float damageAmount, bool isCriticalHit)
    {
        // Tạo Popup tại vị trí truyền vào
        // Quaternion.identity nghĩa là giữ nguyên góc xoay mặc định (không xoay)
        Transform damagePopupTransform = Instantiate(pfDamagePopup, position, Quaternion.identity);

        // Gọi hàm Setup
        DamagePopup damagePopup = damagePopupTransform.GetComponent<DamagePopup>();
        damagePopup.Setup(damageAmount, isCriticalHit);
    }
}