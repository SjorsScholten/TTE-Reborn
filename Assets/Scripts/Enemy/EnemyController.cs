using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour {

    [Header("Idle Behaviour")]
    [SerializeField] private IdleBehaviour idle;

    [Header("Movement Behaviour")]
    [SerializeField] private MovementBehaviour movement;

    [Header("Attack Behaviour")]
    [SerializeField] private AttackBehaviour attack;

    [Header("Animations")]
    [SerializeField] private EnemyAnimations animations;

    [Header("Aggro Behaviour")]
    [SerializeField] private AggroBehaviour aggro;

    [Header("Stun Settings")]
    [SerializeField] private float stunTimer;

    public Animator Animator { get; private set; }
    public EntityState EnemyState { get; private set; }

    private void Awake() {
        Animator = GetComponent<Animator>();
        EnemyState = EntityState.Idle;

        idle.enemy = this;
        aggro.enemy = this;
    }

    private void Update() {
        switch (EnemyState) {
            case EntityState.Idle:
                Idle();
                break;
            case EntityState.Aggro:
                Aggro();
                break;
            case EntityState.Attack:
                Attack();
                break;
            case EntityState.Stun:
                //Nothing, being stunned is called from an event
                break;
            default:
                EnemyState = EntityState.Idle;
                break;
        }
    }

    private void Idle() {
        idle.Idle(animations.idleAnimation);
        Transform target = aggro.LookForTarget();
        if (target != null) {
            EnemyState = EntityState.Aggro;
        }
    }

    private void Aggro() {
        //Movement and checking for deaggro (aggro.TargetStillInRange)
    }

    private void Attack() {
        //Actually attack the player and stop moving while attacking
    }

    public void Stun() {
        StartCoroutine(StunRoutine());
    }

    private IEnumerator StunRoutine() {
        //TODO: Knockback based on damager position.
        //Might be better to do that in Destroyable script.

        Animator.Play(animations.hurtAnimation);
        EnemyState = EntityState.Stun;
        yield return new WaitForSecondsRealtime(stunTimer);
        EnemyState = EntityState.Idle;
    }
}
