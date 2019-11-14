using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;
using System.Linq;

public class PlayerMovement : MonoBehaviour, PlayerControls.IMovementActions {

    [HideInInspector] public bool isActive;

    [SerializeField]
    private Animator animator;
    private Vector2 lastDirection;

    [Serializable] public class OnMovePlayer : UnityEvent<Vector2> { }
    public OnMovePlayer OnMovePlayerEvent;

    public Vector2 Direction { get; private set; }

    private PlayerAttack playerAttack;
    private PlayerControls input;
    private Destroyable destroyable;

    private void Awake() {
        input = new PlayerControls();
        input.Movement.SetCallbacks(this);
        lastDirection = Vector2.down;
        playerAttack = GetComponent<PlayerAttack>();
        destroyable = GetComponent<Destroyable>();
    }

    private void FixedUpdate() {
        DoMove();

        Animate();
    }

    private void DoMove() {
        if (playerAttack.IsAttacking || destroyable.IsStunned) return;

        OnMovePlayerEvent.Invoke(Direction);
        playerAttack.RotateAttacks(lastDirection);
    }

    public void ForceMove(Vector2 direction) {
        this.Direction = direction;
    }

    private void Animate() {
        if (!isActive) return;

        if (Direction == Vector2.zero || playerAttack.IsAttacking) { //Idle 
            //|| Remove playerAttack.IsAttacking here when attack animation is actually on the player sprites.
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
        input.Movement.Enable();
    }

    private void OnDisable() {
        input.Movement.Disable();
    }
}