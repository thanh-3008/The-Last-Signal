using UnityEngine;

public class PickupRange : MonoBehaviour
{
    // Cập nhật kích thước vòng nhặt đồ dựa trên PlayerData
    private void Start()
    {
        UpdateRange();
    }

    public void UpdateRange()
    {
        CircleCollider2D col = GetComponent<CircleCollider2D>();

        // Truy cập thẳng qua Instance của PlayerLevelSystem
        if (col != null && PlayerLevelSystem.Instance != null && PlayerLevelSystem.Instance.playerData != null)
        {
            col.radius = PlayerLevelSystem.Instance.playerData.pickupRange;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent<ExperienceGem>(out ExperienceGem gem))
        {
            // Bảo viên gem bay về phía Player (cha của PickupRange)
            gem.StartFollowing(transform.parent);
        }
    }
}