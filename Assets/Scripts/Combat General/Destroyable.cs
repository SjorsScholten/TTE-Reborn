using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Destroyable : MonoBehaviour {
    [Serializable] public class OnDestroyed : UnityEvent<Transform> { }
    [Serializable] public class OnDamaged : UnityEvent<int> { }
    [Serializable] public class OnKnockback : UnityEvent<float> { }

    #region parameters
    [Header("Stats")]

    [SerializeField] private BaseStats stats;
    public BaseStats Stats { get { return stats; } set { stats = value; } }

    [SerializeField] [EnumFlag] private DamageSource immuneToSource;

    [HideInInspector] public int health;

    [Header("Knockback & Stun")]

    [SerializeField] private bool receivesKnockback = true;
    [SerializeField] private float knockbackMultiplier;
    [SerializeField] private float stunTimer;

    [Header("I-Frames")]
    [SerializeField] private float iFrames;

    private TimedVariable invinsibilityTimer = new TimedVariable();
    private bool isInvinsible;

    [Header("Events")]
    public OnDestroyed OnDestroyedEvent;
    public OnDamaged OnDamagedEvent;
    public OnKnockback OnKnockbackEvent;
    #endregion

    private void Awake() {
        this.health = stats.vitality;
        invinsibilityTimer.time = iFrames + stunTimer;
        invinsibilityTimer.triggerAction += () => {
            invinsibilityTimer.Reset();
            GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 1);
            isInvinsible = false;
        };
    }

    private void Update() {
        if (isInvinsible) {
            invinsibilityTimer.StepFrame();
        }
    }

    public bool Damage(int baseDamage, Transform origin, DamageSource source) {
        if (IsImmune(source)) return false;
        if (isInvinsible) return false;

        return TakeDamage(baseDamage, origin, source);
    }

    private bool TakeDamage(int baseDamage, Transform origin, DamageSource source) {
        int defense = Tools.GetDefense(source, stats) / 2;

        int damage = baseDamage - defense;

        if (damage <= 0) damage = 1;

        health -= damage;

        OnDamagedEvent.Invoke(health);
        StartCoroutine(ApplyKnockback(origin));

        if (health <= 0) {
            DestroyDestroyable();
        }

        return true;
    }

    private Vector3 velocity;
    private IEnumerator ApplyKnockback(Transform origin) {
        OnKnockbackEvent.Invoke(stunTimer);
        isInvinsible = true;
        GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 0.4f); //DEBUG PROGRAMMER ART

        if (receivesKnockback) {
            Vector3 direction = (transform.position - origin.position).normalized;
            direction *= knockbackMultiplier;
            float time = 0;

            while (time <= stunTimer) {
                transform.position = Vector3.SmoothDamp(transform.position, transform.position + direction, ref velocity, stunTimer);
                yield return null;
                time += Time.deltaTime;
            }
        }
    }

    private void DestroyDestroyable() {
        OnDestroyedEvent.Invoke(transform);
    }

    private bool IsImmune(DamageSource source) {
        return immuneToSource.HasFlag(source);
    }
}
