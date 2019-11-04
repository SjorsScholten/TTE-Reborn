using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class WalkTowardsTarget : MovementBehaviour {
    public override void Move(Transform target) {
        enemy.Animator.Play(enemy.animations.walkAnimation);

        Vector3 velocity = target.position - transform.position;
        mtm.Move(velocity.normalized);

        if (velocity.normalized.x <= 0) {
            GetComponent<SpriteRenderer>().flipX = true;
        }
        else {
            GetComponent<SpriteRenderer>().flipX = false;
        }
    }

    public override List<Type> OnDestroy() { return null; }
}
