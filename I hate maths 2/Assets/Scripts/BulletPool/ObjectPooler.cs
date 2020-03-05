using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPooler : MonoBehaviour
{
    public bool expand = true;

    [SerializeField] GameObject bullet;
    [SerializeField] GameObject[] bulletType;
    public List<GameObject> pool;

    private void Start()
    {
        bullet = bulletType[0];

        expand = true;
        pool = new List<GameObject>();
    }

    public GameObject GetFromPool()
    {
        if (pool.Count > 0)
        {
            for (int i = 0; i < pool.Count; i++)
            {
                if (!pool[i].activeInHierarchy)
                {
                    return pool[i];
                }
            }
        }
        if (expand)
        {
            GameObject obj = Instantiate(bullet);
            obj.SetActive(false);
            pool.Add(obj);
            return obj;
        }

        return null;
    }
}
