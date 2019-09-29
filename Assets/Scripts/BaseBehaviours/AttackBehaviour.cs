﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AttackBehaviour : MonoBehaviour {

    [HideInInspector] public EnemyController enemy;

    [SerializeField] protected float cooldown;

    protected bool isOnCooldown;

    protected TimedVariable timerCooldown = new TimedVariable();

    public bool IsAttacking { get; protected set; }

    private void Awake() {
        timerCooldown.time = cooldown;
        timerCooldown.triggerAction = () => {
            timerCooldown.Reset();
            isOnCooldown = false;
            Debug.Log("Fired Timer Cooldown Reset");
        };
    }

    private void Update() {
        if (isOnCooldown) timerCooldown.StepFrame();
    }

    public abstract IEnumerator Routine();
    public abstract bool AllowedToAttack();
}