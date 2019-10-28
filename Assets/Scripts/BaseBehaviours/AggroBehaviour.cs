using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AggroBehaviour : MonoBehaviour {

    [HideInInspector] public EnemyController enemy;

    public LayerMask layerMask;
    public GameObject alertIcon;

    public abstract Transform LookForTarget();
    public abstract bool TargetStillInRange(Transform target);

    private void OnEnable()
    {
        foreach (Transform child in transform)
        {
            if (child.name.Equals("AlertIcon"))
            {
                this.alertIcon = child.gameObject;
                return;
            }
        }
    }
}
