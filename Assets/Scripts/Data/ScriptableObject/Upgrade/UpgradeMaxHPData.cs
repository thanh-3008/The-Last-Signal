using UnityEngine;
[CreateAssetMenu(fileName = "NewUpgradeData", menuName = "GameData/UpgradeData/UpgradeMaxHPData")]
public class UpgradeMaxHP : UpgradeData
{
    public float maxHP;
    public override void ChangeUpgradeUltimate()
    {
        throw new System.NotImplementedException();
    }

    public override void UnlockUpgrade()
    {
        player.data.maxHealth += maxHP;
    }

    public override void UpgradeLevel()
    {
        player.data.maxHealth += maxHP;
    }
}
