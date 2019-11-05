using Pathfinding;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[RequireComponent(typeof(PathfindingAI))]
public class WalkTowardsPath : MovementBehaviour {

    public override void Move(Transform target) {
        enemy.animator.Play(enemy.animations.walkAnimation);

        Vector2 velocity = enemy.pathfinding.Direction;
        mtm.Move(velocity.normalized);

        if (velocity.normalized.x <= .1f) {
            transform.localScale = new Vector3(-1f, 1f, 1f);
        } else if (velocity.normalized.x >= -.1f) {
            transform.localScale = new Vector3(1f, 1f, 1f);
        }
    }

    public override List<Type> OnDestroy()
    {
        return new List<Type>()
        {
            typeof(PathfindingAI),
            typeof(Seeker)
        };
    }
}
