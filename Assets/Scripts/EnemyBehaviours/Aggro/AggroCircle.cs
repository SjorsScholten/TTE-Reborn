using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AggroCircle : AggroBehaviour {

    [SerializeField] private float aggroRadius;
    [SerializeField] private float deAggroRadius;
    [SerializeField] [Tooltip("Line of Sight")] private bool requiresLOS;
    [SerializeField] private LayerMask layerMask;
    [SerializeField] private GameObject alertIcon;

    public override Transform LookForTarget() {
        var targets = Physics2D.OverlapCircleAll(transform.position, aggroRadius, layerMask);

        if (targets.Length == 0)
        {
            if (Physics2D.OverlapCircleAll(transform.position, deAggroRadius, layerMask).Length > 0)
            {
                alertIcon.SetActive(true);
            }
            else
            {
                alertIcon.SetActive(false);
            }
        }
        else
        {
            alertIcon.SetActive(false);
        }

        Transform nearestTarget = null;
        foreach (var t in targets) {
            //if (requiresLOS&& !HasLOS(t.transform)) continue;
            if (nearestTarget != null) {
                if (Vector3.Distance(nearestTarget.position, transform.position) >
                    Vector3.Distance(t.transform.position, transform.position)) {
                    nearestTarget = t.transform;
                }
            }
            else {
                nearestTarget = t.transform;
            }
        }

        return nearestTarget;
    }

    public override bool TargetStillInRange(Transform target) {
        //if (!requiresLOS && !HasLOS(target)) return false;
        return Vector3.Distance(target.position, transform.position) <= deAggroRadius;
    }

    private void OnDrawGizmos() {
        float aggroDistance = aggroRadius;
        float deAggroDistance = deAggroRadius;
        Vector3 lastPositionAggro = transform.position + transform.right * aggroDistance;
        Vector3 lastPositionDeAggro = transform.position + transform.right * deAggroDistance;
        for (var i = 10; i <= 360; i += 10) {
            Vector3 nextPositionAggro = transform.position + Quaternion.Euler(0, 0, i) * transform.right * aggroDistance;
            Vector3 nextPositionDeAggro = transform.position + Quaternion.Euler(0, 0, i) * transform.right * deAggroDistance;
            Gizmos.color = Color.red;
            Gizmos.DrawLine(lastPositionAggro, nextPositionAggro);
            Gizmos.color = Color.magenta;
            Gizmos.DrawLine(lastPositionDeAggro, nextPositionDeAggro);
            lastPositionAggro = nextPositionAggro;
            lastPositionDeAggro = nextPositionDeAggro;
        }

    }
}
