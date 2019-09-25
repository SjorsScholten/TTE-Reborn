using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Experimental.Input;
using System.Linq;

public class PlayerMovement : MonoBehaviour, IMovementActions {

    [HideInInspector] public bool isActive;

    public InputMaster controls;

    [SerializeField]
    private Animator animator;
    private Vector2 lastDirection;

    [Serializable] public class OnMovePlayer : UnityEvent<Vector2> { }
    public OnMovePlayer OnMovePlayerEvent;

    public Vector2 Direction { get; private set; }

    private PlayerAttack playerAttack;

    private void Awake() {
        controls.Movement.SetCallbacks(this);
        lastDirection = Vector2.down;
        playerAttack = GetComponent<PlayerAttack>();
    }

    private void Update() {
        DoMove();

        Animate();
    }

    private void DoMove() {
        OnMovePlayerEvent.Invoke(Direction);
        playerAttack.RotateAttacks(lastDirection);
    }

    public void ForceMove(Vector2 direction) {
        this.Direction = direction;
    }

    private void Animate() {
        if (!isActive) return;

        if (Direction == Vector2.zero) { //Idle
            animator.SetBool("Idle", true);
            animator.SetFloat("Horizontal", lastDirection.x);
            animator.SetFloat("Vertical", lastDirection.y);
        } else { //Walk
            animator.SetBool("Idle", false);
            animator.SetFloat("Horizontal", Direction.x);
            animator.SetFloat("Vertical", Direction.y);
            lastDirection = Direction; //Save last direction the player has faced.
        }
    }

    public void OnMove(InputAction.CallbackContext context) {
        if (isActive) Direction = context.ReadValue<Vector2>();
    }

    private void OnEnable() {
        controls.Movement.Enable();
    }

    private void OnDisable() {
        controls.Movement.Disable();
    }
}