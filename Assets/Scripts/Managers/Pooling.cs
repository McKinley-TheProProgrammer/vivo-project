using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum DataStructureType
{
    STACK,
    QUEUE,
    LIST
}
public class Pooling : MonoBehaviour
{
    [System.Serializable]
    public class Pool
    {
        public string objTag;
        public GameObject prefab;
        public int size;
    }

    private Dictionary<string, Queue<GameObject>> poolingDictionaty = new Dictionary<string, Queue<GameObject>>();

    [SerializeField] List<Pool> listOfPools = new List<Pool>();
    
    [SerializeField] private DataStructureType dataStructureType;

    private static Pooling instance;
    public static Pooling Instance => instance;
    private void Awake()
    {
        foreach (var pool in listOfPools)
        {
            Queue<GameObject> listOfObjs = new Queue<GameObject>();
            
            for (int i = 0; i < pool.size; i++)
            {
                GameObject obj = Instantiate(pool.prefab);
                obj.SetActive(false);
                listOfObjs.Enqueue(obj);
            }
            
            poolingDictionaty.Add(pool.objTag,listOfObjs);
        }

        if (instance != null)
        {
            instance = this;
        }
    }

    public GameObject SpawnFromPool(string poolTag,Vector3 pos, Quaternion rot)
    {
        if (!poolingDictionaty.ContainsKey(poolTag))
        {
            Debug.LogError("Pool Tag: " + poolTag + " not found");
            return null;
        }

        GameObject pooledObj = poolingDictionaty[poolTag].Dequeue();

        pooledObj.SetActive(true);
        pooledObj.transform.position = pos;
        pooledObj.transform.rotation = rot;

        IPool _iPool = pooledObj.GetComponent<IPool>();
        
        _iPool?.OnSpawnedObject();
        
        poolingDictionaty[poolTag].Enqueue(pooledObj);
        
        return pooledObj;
    }
}
