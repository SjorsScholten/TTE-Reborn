using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour {

    [Header("Idle Settings")]
    [SerializeField] private IdleBehaviour idle;

    [Header("Movement Settings")]
    [SerializeField] private IdleBehaviour movement;

    [Header("Attack Settings")]
    [SerializeField] private IdleBehaviour attack;

    [Header("Animations")]
    [SerializeField] private EnemyAnimations animations;

    private void Update() {
        idle.Idle(animations.idleAnimation);
    }

}
