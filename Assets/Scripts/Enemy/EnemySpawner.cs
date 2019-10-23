using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour {

    public EnemyPool enemyObject;

    private ObjectPooler pooler;
    private GameObject spawned;

    private void Awake() {
        pooler = ObjectPooler.Instance;
    }

    private void OnEnable() {
        spawned = pooler.SpawnFromPool(enemyObject.tag, transform.parent, transform.position, transform.rotation);
    }

    private void OnDisable() {
        if (spawned) {
            pooler.Despawn(spawned);
        }
    }
}
