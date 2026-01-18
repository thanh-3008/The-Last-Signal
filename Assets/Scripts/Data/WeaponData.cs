using UnityEngine;
using UnityEngine.Localization;
using UnityEngine.UI;

[CreateAssetMenu (fileName = "NewWeaponData",menuName = "GameData/Weapon Data")]
public class WeaponData : ScriptableObject
{
    public LocalizedString weaponName;

    public string weaponTag;

    public Image weaponImage;

    public WeaponQuality weaponQuality;

    public GameObject prefabBullet;

    public float damageMultiplier;

    public LocalizedString weaponDes;

    public float fireRate;

    public float moveSpeed;
}
