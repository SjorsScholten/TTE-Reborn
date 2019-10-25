using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackCircle : AttackBehaviour {

    [SerializeField] private float radius = 2;

    public override bool AllowedToAttack() {
        return !isOnCooldown &&
            Vector3.Distance(enemy.Target.position, transform.position) <= radius;
    }

    public override IEnumerator Routine() {       
        IsAttacking = true;
        enemy.Animator.Play(enemy.animations.attackAnimation);   
        yield return new WaitForSecondsRealtime(enemy.Clip.length);
        isOnCooldown = true;
        IsAttacking = false;
        enemy.ForceAggro();
    }

    private void OnDrawGizmos() {
        Vector3 lastPosition = transform.position + transform.right * radius;
        for (var i = 10; i <= 360; i += 10) {
            Vector3 nextPosition = transform.position + Quaternion.Euler(0, 0, i) * transform.right * radius;
            Gizmos.color = Color.white;
            Gizmos.DrawLine(lastPosition, nextPosition);
            lastPosition = nextPosition;
        }

    }
}
