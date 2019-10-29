using Pathfinding;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PathfindingAI))]
public class WalkTowardsPath : MovementBehaviour {

    public override void Move(Transform target) {
        enemy.Animator.Play(enemy.animations.walkAnimation);

        Vector2 velocity = enemy.pathfinding.Direction;
        mtm.Move(velocity.normalized);

        if (velocity.normalized.x <= 0.01f) {
            GetComponent<SpriteRenderer>().flipX = true;
        } else if (velocity.normalized.x >= -0.01f) {
            GetComponent<SpriteRenderer>().flipX = false;
        }
    }

}
