using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class PlayerAttack : MonoBehaviour, PlayerControls.ICombatActions {

    private PlayerControls input;

    [SerializeField] private Animator weakAttackAnimator;
    [SerializeField] private Transform attackContainer;

    void Awake() {
        input = new PlayerControls();
        input.Combat.SetCallbacks(this);
    }

    public void RotateAttacks(Vector2 direction) {
        Direction side = Tools.ConvertVector2ToDirection(direction);
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
        weakAttackAnimator.SetBool("Attack", false);
        
    }

    private void OnEnable() {
        input.Combat.Enable();
    }

    private void OnDisable() {
        input.Combat.Disable();
    }
}
