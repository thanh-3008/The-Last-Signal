using System.Collections.Generic;
using UnityEngine;

public class ObjectPooler : MonoBehaviour
{
    public static ObjectPooler Instance;

    private Dictionary<string, Queue<GameObject>> poolDictionary = new();
    private Dictionary<string, GameObject> prefabRepo = new();

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
    }

    // Tạo pool cho 1 tag
    public void InitializePool(string tag, GameObject prefab, int size)
    {
        if (poolDictionary.ContainsKey(tag)) return;

        poolDictionary[tag] = new Queue<GameObject>();
        prefabRepo[tag] = prefab;

        for (int i = 0; i < size; i++)
        {
            CreateNewObject(tag);
        }
    }

    private GameObject CreateNewObject(string tag)
    {
        GameObject obj = Instantiate(prefabRepo[tag], transform);
        obj.SetActive(false);
        poolDictionary[tag].Enqueue(obj);
        return obj;
    }

    public GameObject SpawnFromPool(string tag, Vector3 position, Quaternion rotation)
    {
        if (!poolDictionary.ContainsKey(tag))
        {
            Debug.LogError($"Pool với tag {tag} chưa được khởi tạo");
            return null;
        }

        GameObject obj =
            poolDictionary[tag].Count > 0
            ? poolDictionary[tag].Dequeue()
            : CreateNewObject(tag);

        obj.transform.SetPositionAndRotation(position, rotation);
        obj.SetActive(true);

        return obj;
    }

    public void ReturnToPool(string tag, GameObject obj)
    {
        if (!poolDictionary.ContainsKey(tag)) Debug.LogError("Khong ton tai tag de tra ");
        else
        {
            obj.SetActive(false);
            poolDictionary[tag].Enqueue(obj);
        }
    }
}
