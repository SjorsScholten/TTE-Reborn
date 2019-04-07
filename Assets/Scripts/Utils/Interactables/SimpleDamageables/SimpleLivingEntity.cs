using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleLivingEntity : MonoBehaviour, IDamageable {
    [SerializeField]
    private float startingHealth;
    private float currentHealth;
    private bool dead = false;

    private void Awake() {
        currentHealth = startingHealth;
    }

    public void TakeHit(float damage) {
        currentHealth -= damage;
        if (currentHealth <= 0 && !dead) {
            Die();
        }
    }

    private void Die() {
        dead = true;
    }
}
