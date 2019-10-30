using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class StandStill : MovementBehaviour
{
    public override void Move(Transform target)
    {
        //TODO Andere animatie indien mogelijk?
        enemy.Animator.Play(enemy.animations.idleAnimation);

        Vector3 velocity = target.position - transform.position;
        if (velocity.normalized.x <= 0)
        {
            GetComponent<SpriteRenderer>().flipX = true;
        }
        else
        {
            GetComponent<SpriteRenderer>().flipX = false;
        }
    }

    public override List<Type> OnDestroy(){ return null; }
}
