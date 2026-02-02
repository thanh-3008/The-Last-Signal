using System.Collections;
using UnityEngine;

public class GameTimeManager : MonoBehaviour
{
    public static GameTimeManager Instance { get; private set; }

    [Header("Debug Status")]
    [SerializeField] private bool isPlayerDead = false;
    [SerializeField] private bool isGamePaused = false;

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    // 1. Khi Player Chết: Chờ 1s rồi mới dừng
    public void SetPlayerDead(bool isDead)
    {
        isPlayerDead = isDead;
        if (isPlayerDead)
        {
            StartCoroutine(DelayTimeScale(0f, 1f)); // Delay 1s rồi set TimeScale = 0
        }
        else
        {
            // Nếu hồi sinh, thường ta muốn game chạy lại ngay hoặc có delay tùy bạn
            Time.timeScale = 1f;
        }
    }

    // 2. Khi Pause/Resume
    public void SetGamePaused(bool isPaused)
    {
        isGamePaused = isPaused;
        StopAllCoroutines(); // Dừng các lệnh delay trước đó để tránh xung đột

        if (isGamePaused)
        {
            // Pause ngay lập tức
            Time.timeScale = 0f;
        }
        else
        {
            // Nếu không chết thì mới cho phép Resume kèm delay
            if (!isPlayerDead)
            {
                StartCoroutine(DelayTimeScale(1f, 1f)); // Delay 1s rồi set TimeScale = 1
            }
        }
    }

    // Hàm bổ trợ xử lý delay
    private IEnumerator DelayTimeScale(float targetScale, float delay)
    {
        // Sử dụng WaitForSecondsRealtime vì khi Time.timeScale = 0, 
        // WaitForSeconds thường sẽ bị đứng im.
        yield return new WaitForSecondsRealtime(delay);

        // Kiểm tra lại điều kiện một lần nữa trước khi thực hiện để tránh lỗi logic
        Time.timeScale = targetScale;
    }
}