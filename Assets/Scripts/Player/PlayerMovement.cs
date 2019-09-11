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

    private Vector2 direction;
    private Vector2 lastDirection;

    [Serializable] public class OnMovePlayer : UnityEvent<Vector2> { }
    public OnMovePlayer OnMovePlayerEvent;

    public Vector2 Direction {
        get { return direction; }
    }

    private void Awake() {
        controls.Movement.SetCallbacks(this);
        lastDirection = Vector2.down;
    }

    private void Update() {
        DoMove();

        Animate();
    }

    private void DoMove() {
        OnMovePlayerEvent.Invoke(direction);
    }

    public void ForceMove(Vector2 direction) {
        this.direction = direction;
    }

    private void Animate() {
        if (direction == Vector2.zero) { //Idle
            animator.SetBool("Idle", true);
            animator.SetFloat("Horizontal", lastDirection.x);
            animator.SetFloat("Vertical", lastDirection.y);
        } else { //Walk
            animator.SetBool("Idle", false);
            animator.SetFloat("Horizontal", direction.x);
            animator.SetFloat("Vertical", direction.y);
            lastDirection = direction; //Save last direction the player has faced.
        }
    }

    public void OnMove(InputAction.CallbackContext context) {
        if (isActive) direction = context.ReadValue<Vector2>();
    }

    private void OnEnable() {
        controls.Enable();
    }

    private void OnDisable() {
        controls.Disable();
    }
}