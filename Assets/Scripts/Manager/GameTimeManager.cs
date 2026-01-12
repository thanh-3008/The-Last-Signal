using UnityEngine;

public class GameTimeManager : MonoBehaviour
{
    // Tạo Singleton để dễ gọi từ các script khác (Player, UI)
    public static GameTimeManager Instance { get; private set; }

    [Header("Debug Status")]
    [SerializeField] private bool isPlayerDead = false;
    [SerializeField] private bool isGamePaused = false;

    private void Awake()
    {
        // Setup Singleton
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // Hàm gọi khi nhân vật Chết/Hồi sinh
    public void SetPlayerDead(bool isDead)
    {
        isPlayerDead = isDead;
        CheckTimeScale();
    }

    // Hàm gọi khi Bật/Tắt Pause menu
    public void SetGamePaused(bool isPaused)
    {
        isGamePaused = isPaused;
        CheckTimeScale();
    }

    // Hàm logic trung tâm để quyết định TimeScale
    private void CheckTimeScale()
    {
        // Nếu (Nhân vật chết) HOẶC (Game đang Pause) -> Dừng game
        if (isPlayerDead || isGamePaused)
        {
            Time.timeScale = 0f;
        }
        else
        {
            // Chỉ khi sống VÀ không pause -> Game mới chạy
            Time.timeScale = 1f;
        }
    }
}