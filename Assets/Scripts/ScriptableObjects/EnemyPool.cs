using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewEnemyPool", menuName = "Enemy/Pool")]
public class EnemyPool : ScriptableObject {
    public string tag;
    public GameObject prefab;
}
