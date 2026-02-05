using UnityEngine;

public class PlayerLevelSystem : MonoBehaviour
{
    public static PlayerLevelSystem Instance { get; private set; }
    public PlayerData playerData; // Gán data vào đây

    public int currentLevel = 1;
    public float currentExp = 0;
    public float expToNextLevel = 100;

    private void Awake() => Instance = this;

    private void Start()
    {
        playerData = PlayerManager.Instance.GetPlayer().data;
    }

    public void AddExperience(float amount)
    {
        // Cộng exp có tính đến chỉ số ExpGain (ví dụ: 1.1 = +10%)
        currentExp += amount * (1 + playerData.expGain / 100f);

        // Kiểm tra lên cấp (dùng while để xử lý lên nhiều cấp cùng lúc)
        while (currentExp >= expToNextLevel)
        {
            LevelUp();
        }
    }

    private void LevelUp()
    {
        currentExp -= expToNextLevel;
        currentLevel++;

        // Công thức tăng EXP cần thiết: Cấp càng cao càng cần nhiều EXP
        // Ví dụ: Mỗi cấp tăng thêm 20% yêu cầu
        expToNextLevel = Mathf.Round(expToNextLevel * 1.2f);

        Debug.Log($"Level Up! Hiện tại: {currentLevel}");

        // Gọi Menu nâng cấp
        UpgradeManager.Instance.ShowUpgradeSelection();
    }
}
