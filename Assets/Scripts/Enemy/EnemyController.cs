using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour {

    [Header("Idle Settings")]
    [SerializeField] private IdleBehaviour idle;

    [Header("Movement Settings")]
    [SerializeField] private MovementBehaviour movement;

    [Header("Attack Settings")]
    [SerializeField] private AttackBehaviour attack;

    [Header("Animations")]
    [SerializeField] private EnemyAnimations animations;

    [Header("Aggro Settings")]
    [SerializeField] private AggroBehaviour aggro;

    [Header("Stun Settings")]
    [SerializeField] private float stunTimer;

    public Animator Animator { get; private set; }
    public EntityState EnemyState { get; private set; }

    private void Awake() {
        Animator = GetComponent<Animator>();
        EnemyState = EntityState.Idle;

        idle.enemy = this;
    }

    private void Update() {
        switch (EnemyState) {
            case EntityState.Idle:
                idle.Idle(animations.idleAnimation);
                break;
            case EntityState.Move:
                break;
            case EntityState.Attack:
                break;
            case EntityState.Stun:
                break;
            default:
                break;
        }
    }

    public void Hurt() {
        StartCoroutine(StunRoutine());
    }

    private IEnumerator StunRoutine() {
        Animator.Play(animations.hurtAnimation);
        EnemyState = EntityState.Stun;
        yield return new WaitForSecondsRealtime(stunTimer);
        EnemyState = EntityState.Idle;
    }
}
