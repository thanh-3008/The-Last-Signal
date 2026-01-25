using UnityEngine;
using UnityEngine.Localization;
using UnityEngine.UI;
public abstract class UpgradeData : ScriptableObject
{
    public LocalizedString nameUpgrade;
    public LocalizedString desUpgrade;
    public Image imgUpgrade;
    protected PlayerController player;
    public UpgradeData UltimateUpgrade;
    public enum UpgradeType
    {
        StatModifier,   // Tăng chỉ số: Sức mạnh, tốc độ...
        Ability,        // Kỹ năng mới: Con quay, lớp giáp...
        Consumable,     // Tiêu thụ/Hỗ trợ: Hồi máu, nhận vàng...
        UltimateUpgrade // Kỹ năng tối thượng khi kỹ năng Ability max level
    }
    public UpgradeType type;
    public abstract void UnlockUpgrade();
    public abstract void UpgradeLevel();
    public  abstract void ChangeUpgradeUltimate();
    protected void GetPlayerController()
    {
        player = PlayerManager.Instance.GetPlayer();

        if (player == null)
        {
            Debug.LogError("UpgradeSpeedData: Không tìm th?y PlayerController!");
            return;
        }

        if (player.data == null)
        {
            Debug.LogError("UpgradeSpeedData: PlayerData là null!");
            return;
        }
    }
}
