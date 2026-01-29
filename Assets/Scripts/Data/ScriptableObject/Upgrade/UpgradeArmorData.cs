
using UnityEngine;
[CreateAssetMenu(fileName ="NewUpgradeData",menuName ="GameData/UpgradeData/UpgradeArmorData")]
public class UpgradeArmorData : UpgradeData
{
    public float armor;
    public override void ChangeUpgradeUltimate()
    {

    }

    public override void UnlockUpgrade()
    {
        player.data.armor += armor;
    }

    public override void UpgradeLevel()
    {
        player.data.armor += armor;
    }

}
