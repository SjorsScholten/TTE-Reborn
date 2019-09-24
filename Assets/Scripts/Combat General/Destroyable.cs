using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Destroyable : MonoBehaviour {

    #region parameters
    [SerializeField] private BaseStats stats;
    public BaseStats Stats { get { return stats; } }

    [SerializeField] [EnumFlag] private DamageSource immuneToSource;

    [SerializeField] private int health;
    
    [Serializable] public class OnDestroyed : UnityEvent<Transform> { }
    [Header("Events")]
    public OnDestroyed OnDestroyedEvent;

    [Serializable] public class OnDamaged : UnityEvent { }
    public OnDamaged OnDamagedEvent;
    #endregion

    private void Awake()
    {
        this.health = stats.vitality;
    }

    public bool Damage(int baseDamage, Transform origin, DamageSource source) {
        if (IsImmune(source)) return false;
        int defense = DefenseOrResistance(source);

        int damage = baseDamage - defense;

        health -= damage;

        if (health <= 0)
        {
            Destroy(this.gameObject);
        }

        //TODO: Health Calculation and Updates
        //Apply Knockback?
        //Damage Effects
        //Check If Dead

        OnDamagedEvent.Invoke();

        return true;
    }

    private void DestroyDestroyable() {
        OnDestroyedEvent.Invoke(transform);
    }

    private bool IsImmune(DamageSource source) {
        return immuneToSource.HasFlag(source);
    }

    private int DefenseOrResistance(DamageSource source) {
        if ((source & DamageSource.Magic) != 0) return stats.resistance;
        else return stats.defense;
    }
}
