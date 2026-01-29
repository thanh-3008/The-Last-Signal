using UnityEngine;
[CreateAssetMenu(fileName = "NewUpgradeData", menuName = "GameData/UpgradeData/UpgradeCritChanceData")]
public class UpgradeCritChance : UpgradeData
{
    public float critChance;
    public override void ChangeUpgradeUltimate()
    {
        throw new System.NotImplementedException();
    }

    public override void UnlockUpgrade()
    {
        player.data.critChance += critChance;
    }

    public override void UpgradeLevel()
    {
        player.data.critChance += critChance;
    }

}
