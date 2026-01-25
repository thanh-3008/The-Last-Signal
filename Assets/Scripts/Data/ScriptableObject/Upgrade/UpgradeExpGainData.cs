using UnityEngine;
[CreateAssetMenu(fileName = "NewUpgradeData", menuName = "GameData/UpgradeData/UpgradeExpGainData")]
public class UpgradeExpGainData : UpgradeData
{
    public float expGain;
    public override void ChangeUpgradeUltimate()
    {
        throw new System.NotImplementedException();
    }

    public override void UnlockUpgrade()
    {
        player.data.expGain += expGain;
    }

    public override void UpgradeLevel()
    {
        player.data.expGain += expGain;
    }

 
}
