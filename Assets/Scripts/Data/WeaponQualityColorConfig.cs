using UnityEngine;

[CreateAssetMenu(
    fileName = "WeaponQualityColorConfig",
    menuName = "GameData/Weapon Quality Colors"
)]
public class WeaponQualityColorConfig : ScriptableObject
{
    public Color commonColor = Color.white;
    public Color rareColor = Color.blue;
    public Color epicColor = new Color(0.6f, 0f, 1f); // tím
    public Color legendaryColor = new Color(1f, 0.5f, 0f); // cam vàng

    public Color GetColor(WeaponQuality quality)
    {
        return quality switch
        {
            WeaponQuality.Common => commonColor,
            WeaponQuality.Rare => rareColor,
            WeaponQuality.Epic => epicColor,
            WeaponQuality.Legendary => legendaryColor,
            _ => Color.white
        };
    }
}
