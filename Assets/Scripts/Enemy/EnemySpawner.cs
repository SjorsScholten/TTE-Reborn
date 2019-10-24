using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class EnemySpawner : MonoBehaviour {

    public EnemyPool enemyObject;

    private ObjectPooler pooler;
    private GameObject spawned;

    private void Awake() {
        pooler = ObjectPooler.Instance;
        GetComponent<SpriteRenderer>().enabled = false;
    }

    private void OnEnable() {
        spawned = pooler.SpawnFromPool(enemyObject.tag, transform.parent, transform.position, transform.rotation);
    }

    private void OnDisable() {
        if (spawned) {
            pooler.Despawn(spawned);
        }
    }

#if UNITY_EDITOR
    private void OnDrawGizmos() {
        GUIStyle style = new GUIStyle();
        style.normal.textColor = Color.red;
        style.fontSize = 15;
        style.fontStyle = FontStyle.Bold;

        Handles.Label(transform.position, enemyObject.name, style);

    }
#endif
}
