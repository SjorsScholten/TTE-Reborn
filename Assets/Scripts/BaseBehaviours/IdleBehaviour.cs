using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class IdleBehaviour : MonoBehaviour {

    [HideInInspector] public EnemyController enemy;

    public abstract void Idle();
}
