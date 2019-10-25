﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AggroBehaviour : MonoBehaviour {

    [HideInInspector] public EnemyController enemy;

    public LayerMask layerMask;

    public abstract Transform LookForTarget();
    public abstract bool TargetStillInRange(Transform target);
}
