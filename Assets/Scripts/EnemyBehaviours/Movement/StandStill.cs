using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class StandStill : MovementBehaviour
{
    public override void Move(Transform target)
    {
        //TODO Andere animatie indien mogelijk?
        enemy.animator.Play(enemy.animations.idleAnimation);

        Vector3 velocity = target.position - transform.position;
        if (velocity.normalized.x <= 0) {
            transform.localScale = new Vector3(-1f, 1f, 1f);
        } else {
            transform.localScale = new Vector3(1f, 1f, 1f);
        }
    }

    public override List<Type> OnDestroy(){ return null; }
}
