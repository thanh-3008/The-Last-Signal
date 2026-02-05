
using UnityEngine;
[CreateAssetMenu(fileName = "NewUpgradeData",menuName = "GameData/UpgradeData/UpgradeDameBaseData")]
public class UpgradeDameBaseData : UpgradeData
{
    public float dame;
    public override void ChangeUpgradeUltimate()
    {
        throw new System.NotImplementedException();
    }

    public override void UnlockUpgrade()
    {
        GetPlayerController();
        player.data.dameBase += dame;
    }

    public override void UpgradeLevel()
    {
        GetPlayerController();
        player.data.dameBase += dame;
    }

}
