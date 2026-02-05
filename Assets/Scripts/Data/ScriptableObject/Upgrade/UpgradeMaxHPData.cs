using UnityEngine;

[CreateAssetMenu(fileName = "NewUpgradeData", menuName = "GameData/UpgradeData/UpgradeMaxHPData")]
public class UpgradeMaxHP : UpgradeData
{
    public float maxHP;

    public override void UnlockUpgrade()
    {
        ApplyHealthUpgrade();
    }

    public override void UpgradeLevel()
    {
        ApplyHealthUpgrade();
    }

    private void ApplyHealthUpgrade()
    {
        GetPlayerController(); // Giả định hàm này gán biến 'player' trong lớp cha UpgradeData

        if (player != null)
        {
            player.data.maxHealth += maxHP;

            player.maxHealth = player.data.maxHealth;

            player.currentHealth += maxHP;
            player.currentHealth = Mathf.Clamp(player.currentHealth, 0, player.maxHealth);

            player.CallOnHealthChanged();
        }
    }

    public override void ChangeUpgradeUltimate()
    {
        // Logic cho chiêu cuối nếu có
    }
}