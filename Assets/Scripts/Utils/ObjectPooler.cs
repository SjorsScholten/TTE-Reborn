using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPooler : MonoBehaviourSingleton<ObjectPooler> {

    public Dictionary<string, Queue<GameObject>> poolDictionary;
    public List<Pool> pools;

    void Start() {
        poolDictionary = new Dictionary<string, Queue<GameObject>>();

        foreach (Pool pool in pools) {
            Queue <GameObject> objects = new Queue<GameObject>();

            for (int i = 0; i < pool.size; i++) {
                GameObject obj = Instantiate(pool.prefab, transform);
                obj.SetActive(false);
                objects.Enqueue(obj);
            }

            poolDictionary.Add(pool.tag, objects);
        }
    }

    public GameObject SpawnFromPool(string tag, Transform parent, Vector3 position, Quaternion rotation) {
        GameObject obj = poolDictionary[tag].Dequeue();

        obj.transform.SetParent(parent);
        obj.SetActive(true);
        obj.transform.position = position;
        obj.transform.rotation = rotation;

        poolDictionary[tag].Enqueue(obj);

        return obj;
    }

}

[System.Serializable]
public class Pool {
    public string tag;
    public GameObject prefab;
    public int size;
}
