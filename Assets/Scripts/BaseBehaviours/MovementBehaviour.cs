using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public abstract class MovementBehaviour : MonoBehaviour {

    [HideInInspector] public EnemyController enemy;

    protected MultidirectionalTransformMovement mtm;

    private void OnEnable() {
        mtm = GetComponent<MultidirectionalTransformMovement>();
    }

    public abstract void Move(Transform target);

    public abstract List<Type> OnDestroy();
}
