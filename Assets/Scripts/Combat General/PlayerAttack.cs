using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Experimental.Input;
using UnityEngine.UI;

public class PlayerAttack : MonoBehaviour, ICombatActions {

    [SerializeField] private InputMaster controls;

    [SerializeField] private Animator weakAttackAnimator;
    [SerializeField] private Transform attackContainer;

    [SerializeField] private Image cooldownImage;

    private float animationLength;
    private float cooldownTimer;
    private bool cooldown = false;

    void Awake() {
        controls.Combat.SetCallbacks(this);
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

            cooldownImage.fillAmount = cooldownTimer / animationLength;
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
        animationLength = weakAttackAnimator.GetCurrentAnimatorClipInfo(0)[0].clip.length;
        cooldownTimer = animationLength;
        cooldown = true;
        weakAttackAnimator.SetBool("Attack", false);
    }

    private void OnEnable() {
        controls.Combat.Enable();
    }

    private void OnDisable() {
        controls.Combat.Disable();
    }
}
