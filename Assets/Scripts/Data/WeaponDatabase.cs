using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "WeaponDatabase" ,menuName = "GameData/WeaponDatabase")]
public class WeaponDatabase : ScriptableObject
{
    public List<WeaponData> weaponDatabase;

    public WeaponData GetWeapon(int index)
    {
        if (index < 0 || index >= weaponDatabase.Count) return null;
        return weaponDatabase[index];
    }
}
