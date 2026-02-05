using System.Collections;
using UnityEngine;

public class GameTimeManager : MonoBehaviour
{
    public static GameTimeManager Instance { get; private set; }

    [Header("Debug Status")]
    [SerializeField] private int pauseCount = 0; // Đếm số lượng yêu cầu dừng game
    [SerializeField] private bool isPlayerDead = false;

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    // --- GIỮ NGUYÊN TÊN HÀM ĐỂ BẠN KHÔNG PHẢI SỬA CODE NƠI KHÁC ---

    public void SetGamePaused(bool isPaused)
    {
        if (isPaused)
        {
            pauseCount++; // Thêm một yêu cầu dừng
        }
        else
        {
            // Giảm yêu cầu dừng, nhưng không để xuống dưới 0
            pauseCount = Mathf.Max(0, pauseCount - 1);
        }

        UpdateTimeScale();
    }

    public void SetPlayerDead(bool isDead)
    {
        isPlayerDead = isDead;
        if (isPlayerDead)
        {
            StartCoroutine(DelayTimeScale(0f, 1f));
        }
        else
        {
            // Nếu hồi sinh, reset đếm để game chạy ngay
            pauseCount = 0;
            UpdateTimeScale();
        }
    }

    // --- LOGIC ĐIỀU KHIỂN ---

    private void UpdateTimeScale()
    {
        StopAllCoroutines();

        // Nếu vẫn còn ít nhất 1 yêu cầu dừng (từ ESC hoặc từ Upgrade)
        if (pauseCount > 0 || isPlayerDead)
        {
            Time.timeScale = 0f;
        }
        else
        {
            // Chỉ Resume khi không còn ai yêu cầu dừng (pauseCount == 0)
            StartCoroutine(DelayTimeScale(1f, 0.5f));
        }
    }

    private IEnumerator DelayTimeScale(float targetScale, float delay)
    {
        yield return new WaitForSecondsRealtime(delay);

        // Kiểm tra lại: Nếu định Resume (target 1) mà trong lúc chờ delay 
        // lại có thằng khác yêu cầu Pause thì hủy lệnh Resume này.
        if (targetScale > 0 && (pauseCount > 0 || isPlayerDead))
            yield break;

        Time.timeScale = targetScale;
    }
}