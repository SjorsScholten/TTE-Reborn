using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AggroCircle : AggroBehaviour {

    [SerializeField] private float aggroRadius;
    [SerializeField] private float deAggroRadius;
    [SerializeField] private bool requiresLOS;

    public override void LookForTarget() {
        throw new System.NotImplementedException();
    }

    public override void TargetStillInRange() {
        throw new System.NotImplementedException();
    }

    private void OnDrawGizmos() {
        float aggroDistance = aggroRadius;
        float deAggroDistance = deAggroRadius;
        Vector3 lastPositionAggro = transform.position + transform.right * aggroDistance;
        Vector3 lastPositionDeAggro = transform.position + transform.right * aggroDistance;
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
