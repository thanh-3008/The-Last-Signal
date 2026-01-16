using UnityEngine;

public class SpawnPlayerManager : MonoBehaviour
{
    [SerializeField] private CharacterDatabase characters;

    private void Awake()
    {
        SpawnPlayer();
    }
    private void SpawnPlayer()
    {
        if(characters!=null&&characters.playerDatas.Count==0)
        {
            Debug.Log("chua co character databse");
            return;
        }

        int currentPlayerIndex = PlayerPrefs.GetInt("index", 0);

        currentPlayerIndex = Mathf.Clamp(currentPlayerIndex, 0, characters.playerDatas.Count-1);

        PlayerData player = characters.playerDatas[currentPlayerIndex];

        if(player.prefabCharacter!=null)
        {
            GameObject playerPrefab = Instantiate(player.prefabCharacter, Vector3.zero, Quaternion.identity); 
        }
        else { Debug.Log("Chưa gắn prefab"); }
    }
}
