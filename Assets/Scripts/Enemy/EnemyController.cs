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
                idle.Idle(animations.idleAnimation);
                break;
            case EntityState.Aggro:
                //Movement
                break;
            case EntityState.Attack:
                //Attack, No Movement
                break;
            case EntityState.Stun:
                //Nothing, Enemy is stunned
                break;
            default:
                EnemyState = EntityState.Idle;
                break;
        }
    }

    public void Hurt() {
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
