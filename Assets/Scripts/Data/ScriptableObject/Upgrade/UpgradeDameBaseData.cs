
using UnityEngine;
[CreateAssetMenu(fileName = "NewUpgradeData",menuName = "GameData/UpgradeData/UpgradeDameBaseData")]
public class UpgradeDameBaseData : UpgradeData
{
    public float dame;
    public override void ChangeUpgradeUltimate()
    {
        player.data.dameBase += dame;
    }

    public override void UnlockUpgrade()
    {
        player.data.dameBase += dame;
    }

    public override void UpgradeLevel()
    {
        throw new System.NotImplementedException();
    }

}
