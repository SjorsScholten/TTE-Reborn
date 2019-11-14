using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AggroBehaviour : MonoBehaviour {

    [HideInInspector] public EnemyController enemy;

    [Tooltip("Line of Sight")] public bool requiresLOS;
    public LayerMask layerMask;
    public LayerMask LOSMask;
    public GameObject alertIcon;

    private Transform debugtarget;

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

    public bool HasLOS(Transform target, float distance)
    {
        Tools.DrawDebugRay(transform.position, target.position, Color.yellow);

        RaycastHit2D hit = Physics2D.Raycast(this.transform.position, (target.position - transform.position).normalized, distance, LOSMask);
        
        if (hit)
        {
            if (hit.transform.CompareTag(NYRA.Tag.Playable))
            {
                return true;
            }
        }
        return false;
    }
}
