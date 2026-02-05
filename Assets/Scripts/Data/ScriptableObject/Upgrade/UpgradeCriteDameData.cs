using UnityEngine;
[CreateAssetMenu(fileName = "NewUpgradeData", menuName = "GameData/UpgradeData/UpgradeCritDameData")]
public class UpgradeCriteDameData : UpgradeData
{
    public float critDame;
    public override void ChangeUpgradeUltimate()
    {
        throw new System.NotImplementedException();
    }

    public override void UnlockUpgrade()
    {
        GetPlayerController();
        player.data.critDame += critDame;
    }

    public override void UpgradeLevel()
    {
        GetPlayerController();
        player.data.critDame += critDame;
    }
}
