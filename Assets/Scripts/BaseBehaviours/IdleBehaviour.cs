using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleBehaviour : MonoBehaviour {

    [HideInInspector] public EnemyController enemy;

    public virtual void Idle(string animation) {
        enemy.Animator.Play(animation);
    }

}
