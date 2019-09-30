using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StandStillIdle : IdleBehaviour {
    public override void Idle() {
        enemy.Animator.Play(enemy.animations.idleAnimation);
    }
}
