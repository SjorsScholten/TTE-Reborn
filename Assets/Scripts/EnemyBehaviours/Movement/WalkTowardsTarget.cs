using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class WalkTowardsTarget : MovementBehaviour {
    public override void Move(Transform target) {
        enemy.animator.Play(enemy.animations.walkAnimation);

        Vector3 velocity = target.position - transform.position;
        mtm.Move(velocity.normalized);

        if (velocity.normalized.x <= 0) {
            transform.localScale = new Vector3(-1f, 1f, 1f);
        } else {
            transform.localScale = new Vector3(1f, 1f, 1f);
        }
    }

    public override List<Type> OnDestroy() { return null; }
}
