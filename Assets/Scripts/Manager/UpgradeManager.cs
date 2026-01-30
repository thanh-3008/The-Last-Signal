using System.Collections.Generic;
using UnityEngine;
using System.Linq; // Thêm cái này để dùng LINQ cho gọn

public class UpgradeManager : MonoBehaviour
{
    public static UpgradeManager Instance { get; private set; }

    public List<UpgradeData> allUpgrades;
    public UpgradeCardUI upgradeUI; // Kéo thả Script UI vào đây
    public GameObject upgradePanel;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }

    // Hàm logic chính để lấy và hiển thị 3 upgrade
    public void ShowUpgradeSelection()
    {
        if (allUpgrades.Count < 3)
        {
            Debug.LogError("Không đủ Upgrade trong danh sách!");
            return;
        }

        List<UpgradeData> randomUpgrades = allUpgrades.OrderBy(x => Random.value).Take(3).ToList();

        // Hiển thị Panel và gửi dữ liệu sang UI
        upgradePanel.SetActive(true);
        upgradeUI.SetUp(randomUpgrades);

        // Dừng thời gian game nếu cần
        Time.timeScale = 0;
    }

    public void ApplyUpgrade(UpgradeData upgrade)
    {
        // Logic khi người dùng chọn 1 card
        Debug.Log("Đã chọn: " + upgrade.nameUpgrade.GetLocalizedString());

        // Thực hiện logic cộng chỉ số hoặc thêm skill ở đây...

        // Đóng panel và tiếp tục game
        upgradePanel.SetActive(false);
        Time.timeScale = 1;
    }
}