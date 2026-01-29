using UnityEngine;
[CreateAssetMenu(fileName = "NewUpgradeData", menuName = "GameData/UpgradeData/UpgradeCoinGainData")]
public class UpgradeCoinGainData : UpgradeData
{
    public float coinGain;
    public override void ChangeUpgradeUltimate()
    {
        throw new System.NotImplementedException();
    }

    public override void UnlockUpgrade()
    {
        player.data.coinGain += coinGain;
    }

    public override void UpgradeLevel()
    {
        player.data.coinGain += coinGain;
    }

}
