using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Destroyable : MonoBehaviour {

    [SerializeField] private BaseStats stats;
    public BaseStats Stats { get { return stats; } }

    [SerializeField] [EnumFlag] private DamageSource immuneToSource;

    [Serializable] public class OnDestroyed : UnityEvent<Transform> { }
    public OnDestroyed OnDestroyedEvent;

    [Serializable] public class OnDamaged : UnityEvent { }
    public OnDamaged OnDamagedEvent;

    public bool Damage(int baseDamage, Transform origin, DamageSource source) {
        if (IsImmune(source)) return false;
        int defense = DefenseOrResistance(source);

        int damage = baseDamage - defense;
        
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
