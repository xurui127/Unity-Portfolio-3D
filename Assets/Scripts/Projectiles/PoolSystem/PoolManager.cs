using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PoolManager : MonoBehaviour
{
   [SerializeField] Pool[] playerProjectilePools;
  Dictionary<GameObject, Pool> dictionary;


    void Initialize(Pool[] pools)
    {
        foreach (var pool in pools)
        {
#if UNITY_EDITOR
            if (dictionary.ContainsKey(pool.Prefab))
            {
                Debug.LogError("Same prefab in Dictionary: " + pool.Prefab.name);
                continue;
            }
#endif
            dictionary.Add(pool.Prefab, pool);
            Transform poolParent = new GameObject("Projectile Pool: " + pool.Prefab.name).transform;
            poolParent.parent = transform;
            pool.Initialize(poolParent);
        }
    }

#if UNITY_EDITOR
    private void OnDestroy()
    {
        CheckPoolSize(playerProjectilePools);
    }
#endif
    void CheckPoolSize(Pool[] pools)
    {
        foreach (var pool in pools)
        {
            if (pool.RunTimeSize > pool.Size)
            {
                Debug.LogWarning( string.Format(" Pool: {0}'s  runtime size is Bigger than intial size {2}!",
                                  pool.Prefab.name,
                                  pool.RunTimeSize,
                                  pool.Size));
            }
        }
    }
    public  GameObject ReleasePrefab(GameObject prefab)
    {
#if UNITY_EDITOR
        if (!dictionary.ContainsKey(prefab))
        {
            Debug.LogError("Not Found  prefab in Dictionary: " + prefab.name);
            return null;
        }
#endif
        return dictionary[prefab].PrepareObject(); ;
    }
    public GameObject ReleasePrefab(GameObject prefab,Vector3 position)
    {
#if UNITY_EDITOR
        if (!dictionary.ContainsKey(prefab))
        {
            Debug.LogError("Not Found  prefab in Dictionary: " + prefab.name);
            return null;
        }
#endif
        return dictionary[prefab].PrepareObject(position); ;
    } 
    
    public GameObject ReleasePrefab(GameObject prefab,Vector3 position,Quaternion rotation)
    {
#if UNITY_EDITOR
        if (!dictionary.ContainsKey(prefab))
        {
            Debug.LogError("Not Found  prefab in Dictionary: " + prefab.name);
            return null;
        }
#endif
        return dictionary[prefab].PrepareObject(position,rotation); 
    }
    // Start is called before the first frame update
    void Start()
    {
        dictionary = new Dictionary<GameObject, Pool>();
        Initialize(playerProjectilePools);
      
    }

    // Update is called once per frame
    void Update()
    {

    }
}
