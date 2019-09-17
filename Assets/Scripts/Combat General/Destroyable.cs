using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destroyable : MonoBehaviour {

    [SerializeField] private BaseStats stats;
    public BaseStats Stats { get { return stats; } }

    [SerializeField] [EnumFlag] private DamageSource immuneToSource;

    public bool Damage(int baseDamage, Transform origin, DamageSource source) {
        if (IsImmune(source)) return false;
        int defense = DefenseOrResistance(source);

        int damage = baseDamage - defense;

        return true;
    }

    private bool IsImmune(DamageSource source) {
        return immuneToSource.HasFlag(source);
    }

    private int DefenseOrResistance(DamageSource source) {
        if ((source & DamageSource.Magic) != 0) return stats.resistance;
        else return stats.defense;
    }

}
