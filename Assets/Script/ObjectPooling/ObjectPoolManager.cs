using System.Collections.Generic;
using UnityEngine;

public class ObjectPoolManager : MonoBehaviour
{
    public static ObjectPoolManager Instance;

    private Dictionary<GameObject, Pool> poolDictionary = new Dictionary<GameObject, Pool>();

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }
    
    [System.Serializable]
    public class Pool
    {
        public Queue<GameObject> inactiveObjects = new Queue<GameObject>();
        public Transform parent;
    }

    public GameObject Spawn(GameObject prefab, Vector3 position, Quaternion rotation)
    {
        if (!poolDictionary.ContainsKey(prefab)) CreatePool(prefab);

        Pool pool = poolDictionary[prefab];
        GameObject obj;
        PoolableObject poolable = null;

        if (pool.inactiveObjects.Count > 0) obj = pool.inactiveObjects.Dequeue();
        else
        {
            obj = Instantiate(prefab, pool.parent);
            obj.TryGetComponent(out poolable);
            if (poolable != null) poolable.SetPrefab(prefab);
        }

        obj.transform.position = position;
        obj.transform.rotation = rotation;
        obj.SetActive(true);
        if(poolable == null) obj.TryGetComponent(out poolable);
        poolable.SpawnInit();

        return obj;
    }

    public void Despawn(GameObject obj, GameObject originalPrefab)
    {
        if (!poolDictionary.ContainsKey(originalPrefab))
        {
            Destroy(obj);
            return;
        }

        obj.SetActive(false);
        poolDictionary[originalPrefab].inactiveObjects.Enqueue(obj);
    }

    private void CreatePool(GameObject prefab)
    {
        Pool newPool = new Pool();
        
        GameObject parentObj = new GameObject("Pool_" + prefab.name);
        parentObj.transform.SetParent(this.transform);
        
        newPool.parent = parentObj.transform;
        poolDictionary.Add(prefab, newPool);
    }
}