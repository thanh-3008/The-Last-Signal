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
        player.data.critDame += critDame;
    }

    public override void UpgradeLevel()
    {
        player.data.critDame += critDame;
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
