using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleDestroyable : MonoBehaviour, IDestroyable{

    public GameObject objectToDestroy;

    public void Destroy() {
        Destroy(gameObject);
    }

    public void DespawnFromPool() {
        if (objectToDestroy) ObjectPooler.Instance.Despawn(objectToDestroy);
    }
}
