using UnityEngine;

public class ExperienceGem : MonoBehaviour
{
    public enum GemType { Blue = 10, Purple = 50, Yellow = 200, Red = 500 }
    public GemType type;
    public GameObject prefabGem;
    private Transform targetPlayer;
    private bool isMoving = false;
    public string tagGem;
    [SerializeField] private float speed = 10f;

    private void OnEnable()
    {
        isMoving = false;
        targetPlayer = null;
    }

    public void StartFollowing(Transform player)
    {
        targetPlayer = player;
        isMoving = true;
    }

    void Update()
    {
        if (!isMoving || targetPlayer == null) return;

        // Dùng thêm gia tốc để viên đá bay ngày càng nhanh sẽ đẹp hơn
        transform.position = Vector3.MoveTowards(transform.position, targetPlayer.position, speed * Time.deltaTime);

        if (Vector2.Distance(transform.position, targetPlayer.position) < 0.2f)
        {
            // Reset trước khi trả về pool để tránh lỗi logic khung hình cuối
            isMoving = false;
            PlayerLevelSystem.Instance.AddExperience((int)type);
            ObjectPooler.Instance.ReturnToPool(tagGem, gameObject);
        }
    }
}
