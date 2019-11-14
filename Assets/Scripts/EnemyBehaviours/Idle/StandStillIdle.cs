using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StandStillIdle : IdleBehaviour {
    public override void Idle() {
        enemy.animator.Play(enemy.animations.idleAnimation);
    }
}
