using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[RequireComponent(typeof(Animator), typeof(SpriteRenderer))]
public class EnemyController : MonoBehaviour {

    [Header("Idle Behaviour")]
    public IdleBehaviour idle;

    [Header("Movement Behaviour")]
    public MovementBehaviour movement;

    [Header("Attack Behaviour")]
    public AttackBehaviour attack;

    [Header("Aggro Behaviour")]
    public AggroBehaviour aggro;

    [Header("Animations")]
    public EnemyAnimations animations;    

    public Animator Animator { get; private set; }
    public EntityState EnemyState { get; private set; }
    public Transform Target { get; private set; }
    public AnimationClip Clip { get; private set; }
    public bool Attacking { get { return attack.IsAttacking; } }

    private void Awake() {
        Animator = GetComponent<Animator>();
        
        Clip = Animator.runtimeAnimatorController.animationClips.Single(x => animations.attackAnimation == x.name);
     
        idle.enemy = this;
        aggro.enemy = this;
        movement.enemy = this;
        attack.enemy = this;
    }

    private void OnEnable() {
        EnemyState = EntityState.Idle;
    }

    private void FixedUpdate() {
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
        }
    }

    public void ForceAggro() {
        EnemyState = EntityState.Aggro;
    }

    private void Idle() {
        idle.Idle();
        Target = aggro.LookForTarget();
        if (Target != null) {
            EnemyState = EntityState.Aggro;
        }
    }

    private void Aggro() {
        if (Target != null) {
            movement.Move(Target);

            if (!aggro.TargetStillInRange(Target)) {
                EnemyState = EntityState.Idle;
                return;
            }

            if (attack.AllowedToAttack()) {
                EnemyState = EntityState.Attack;
                return;
            }
        }
        else {
            EnemyState = EntityState.Idle;
        }
    }

    private void Attack() {
        if (!Attacking) {
            StartCoroutine(attack.Routine());
        }
    }

    public void Stun(float stunTimer) {
        StartCoroutine(StunRoutine(stunTimer));
    }

    private IEnumerator StunRoutine(float stunTimer) {
        Animator.Play(animations.hurtAnimation);
        EnemyState = EntityState.Stun;
        yield return new WaitForSecondsRealtime(stunTimer);
        EnemyState = EntityState.Aggro;
    }
}
