using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AggroBox : AggroBehaviour
{

    [SerializeField] private float aggroRadius = 5;
    [SerializeField] private float deAggroRadius = 8;
    [SerializeField] [Tooltip("Line of Sight")] private bool requiresLOS;

    public override Transform LookForTarget()
    {
        var targets = Physics2D.OverlapBoxAll(transform.position, new Vector2(aggroRadius, aggroRadius), 0, layerMask);

        if (targets.Length == 0)
        {
            Collider2D[] alertTargets = Physics2D.OverlapBoxAll(transform.position, new Vector2(deAggroRadius, deAggroRadius), 0, layerMask);
            if (alertTargets.Length > 0)
            {
                Debug.Log(alertTargets[0]);
                ActivateAlert(true);
            }
            else
            {
                ActivateAlert(false);
            }
        }
        else
        {
            if (alertIcon != null)
            {
                alertIcon.SetActive(false);
            }
        }

        Transform nearestTarget = null;
        foreach (var t in targets)
        {
            //if (requiresLOS&& !HasLOS(t.transform)) continue;
            if (nearestTarget != null)
            {
                if (Vector3.Distance(nearestTarget.position, transform.position) >
                    Vector3.Distance(t.transform.position, transform.position))
                {
                    nearestTarget = t.transform;
                }
            }
            else
            {
                nearestTarget = t.transform;
            }
        }

        return nearestTarget;
    }

    public override bool TargetStillInRange(Transform target)
    {
        //if (!requiresLOS && !HasLOS(target)) return false;
        return Vector3.Distance(target.position, transform.position) <= deAggroRadius;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(this.transform.position, new Vector3(aggroRadius, aggroRadius));
        Gizmos.color = Color.magenta;
        Gizmos.DrawWireCube(this.transform.position, new Vector3(deAggroRadius, deAggroRadius));
    }

    private void ActivateAlert(bool activate)
    {
        if (alertIcon != null)
        {
            alertIcon.SetActive(activate);
        }
        else
        {
            Debug.Log("No Alert Icon has been assisgned to " + this.gameObject.name, this.gameObject);
        }
    }
}
