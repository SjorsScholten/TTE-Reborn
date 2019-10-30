using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class PlayerAttack : MonoBehaviour, PlayerControls.ICombatActions {

    private PlayerControls input;

    [SerializeField] private Animator weakAttackAnimator;
    [SerializeField] private Transform attackContainer;

    private float animationLength;
    private float cooldownTimer;
    private bool cooldown = false;

    public bool IsAttacking { get { return cooldown; } }

    void Awake() {
        input = new PlayerControls();
        input.Combat.SetCallbacks(this);
    }

    void Update()
    {
        if (cooldown)
        {
            cooldownTimer -= Time.deltaTime;
            if (cooldownTimer <= 0)
            {
                cooldown = false;
                cooldownTimer = 0;
            }
        }
    }

    public void RotateAttacks(Vector2 direction) {
        Direction side = Tools.ConvertVectorToDirection(direction);
        float rotation = 0;

        switch (side) {
            case Direction.North:
                rotation = 180;
                break;
            case Direction.East:
                rotation = 90;
                break;
            case Direction.South:
                rotation = 0;
                break;
            case Direction.West:
                rotation = 270;
                break;
        }
        attackContainer.localRotation = Quaternion.Euler(0, 0, rotation);
    }

    public void OnWeakAttack(InputAction.CallbackContext context) {
        StartCoroutine(WeakAttackRoutine());
    }

    IEnumerator WeakAttackRoutine() {
        weakAttackAnimator.SetBool("Attack", true);
        yield return new WaitForEndOfFrame();
        if (cooldown == false) {
            animationLength = weakAttackAnimator.GetCurrentAnimatorClipInfo(0)[0].clip.length;
            cooldownTimer = animationLength;
            cooldown = true;
        }
        weakAttackAnimator.SetBool("Attack", false);
    }

    private void OnEnable() {
        input.Combat.Enable();
    }

    private void OnDisable() {
        input.Combat.Disable();
    }
}
