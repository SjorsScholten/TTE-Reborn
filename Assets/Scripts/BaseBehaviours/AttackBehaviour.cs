using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AttackBehaviour : MonoBehaviour {

    [HideInInspector] public EnemyController enemy;

    [SerializeField] protected float cooldown = 1;

    protected bool isOnCooldown;

    protected TimedVariable timerCooldown = new TimedVariable();

    public bool IsAttacking { get; protected set; }

    private void OnEnable() {
        timerCooldown.time = cooldown;
        timerCooldown.triggerAction = () => {
            timerCooldown.Reset();
            isOnCooldown = false;
        };
        IsAttacking = false;
        isOnCooldown = false;
    }

    private void Update() {
        if (isOnCooldown) timerCooldown.StepFrame();
    }

    public abstract IEnumerator Routine();
    public abstract bool AllowedToAttack();
}
