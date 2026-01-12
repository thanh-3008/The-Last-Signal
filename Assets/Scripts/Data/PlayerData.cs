using UnityEngine;
using UnityEngine.Localization;

[CreateAssetMenu (fileName = "NewPlayerData",menuName = "GameData/Player Data")]
public class PlayerData : EntityData
{
    [Header("Player Info")] 
    public Sprite avatarCharacter;

    public LocalizedString descriptionCharacter;

    public LocalizedString talentCharacter;

    public LocalizedString classCharacter;

    [Header("Player Specific Stats")]

    public float critChance;

    public float critDame;

    public float expGain;

    public float goldGain;

    public float PickupRange;

}
