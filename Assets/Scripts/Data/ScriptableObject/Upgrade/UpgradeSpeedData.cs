    using UnityEngine;
    [CreateAssetMenu(fileName = "NewUpgradeData",menuName = "GameData/UpgradeData/UpgradeSpeedData")]
    public class UpgradeSpeedData : UpgradeData
    {
        public float speed;

        public override void ChangeUpgradeUltimate()
        {
            throw new System.NotImplementedException();
        }

        public override void UnlockUpgrade()
        {
            player.data.moveSpeed += speed;
        }

        public override void UpgradeLevel()
        {
            player.data.moveSpeed += speed;
        }
    }
