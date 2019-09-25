using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Input;

public class PlayerAttack : MonoBehaviour, ICombatActions {

    [SerializeField] private InputMaster controls;

    [SerializeField] private Animator weakAttackAnimator;
    [SerializeField] private Transform attackContainer;

    void Awake() {
        controls.Combat.SetCallbacks(this);
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
        controls.Combat.Enable();
    }

    private void OnDisable() {
        controls.Combat.Disable();
    }
}
