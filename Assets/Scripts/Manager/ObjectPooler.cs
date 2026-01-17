using System.Collections.Generic;
using UnityEngine;
using static ObjectPooler;

public class ObjectPooler : MonoBehaviour
{
    [System.Serializable]
    public class Pool
    {
        public string tag;
        public GameObject prefab;
        public int size;
    }
    public static ObjectPooler Instance;
    public List<Pool> pools;
    public Dictionary<string, Queue<GameObject>> poolDictionary;

    private void Awake()
    {
        if(Instance==null)
        {
            Instance = this;
        }
        poolDictionary = new Dictionary<string, Queue<GameObject>>();

        foreach (Pool pool in pools)
        {
            Queue<GameObject> objectPool = new Queue<GameObject>();
            for(int i =0;i<pool.size;i++)
            {
                GameObject obj = Instantiate(pool.prefab);
                obj.transform.SetParent(this.transform);
                obj.SetActive(false);
                objectPool.Enqueue(obj);
            }
            poolDictionary.Add(pool.tag, objectPool);
        }
    }
    public GameObject SpawnFromPool(string tag,Vector3 position,Quaternion rotation)
    {
        if(!poolDictionary.ContainsKey(tag))
        {
            Debug.Log("Pool với tag " + tag + "không tồn tại");
        }
        GameObject objectToSpawn;
        if (poolDictionary[tag].Count>0 && !poolDictionary[tag].Peek().activeSelf)
        {
            objectToSpawn = poolDictionary[tag].Dequeue();
        }
        else
        {
            Pool pool = pools.Find(p => p.tag == tag);
            objectToSpawn = Instantiate(pool.prefab);
            objectToSpawn.transform.SetParent(this.transform);
        }
        objectToSpawn.SetActive(true);
        objectToSpawn.transform.position = position;
        objectToSpawn.transform.rotation = rotation;

        return objectToSpawn;
    }

    public void ReturnToPool(string tag,GameObject obj)
    {
        if (poolDictionary[tag].Contains(obj)) return;
        obj.SetActive(false);
        poolDictionary[tag].Enqueue(obj);
    }
}
