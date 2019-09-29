using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StandStillIdle : IdleBehaviour {
    public override void Idle(string animation) {
        enemy.Animator.Play(animation);
    }
}
