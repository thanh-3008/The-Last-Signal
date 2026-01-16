using UnityEngine;
using UnityEngine.Localization;
using UnityEngine.UI;

[CreateAssetMenu (fileName = "NewWeaponData",menuName = "GameData/Weapon Data")]
public class WeaponData : ScriptableObject
{
    public LocalizedString weaponName;

    public Image weaponImage;

    public WeaponQuality weaponQuality;

    public RuntimeAnimatorController weaponAnimator;

    public float DamedamageMultiplier;

    public LocalizedString weaponDes;

    public float attackSpeed;

    public float moveSpeed;
}
