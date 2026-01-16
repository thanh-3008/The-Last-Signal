using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "CharacterDatabase", menuName = "GameData/CharacterDatabase")]
public class CharacterDatabase : ScriptableObject
{
    public List<PlayerData> playerDatas;

    public PlayerData GetCharacter(int index)
    {
        if (index < 0 || index >= playerDatas.Count) return null;
        return playerDatas[index];
    }
}
