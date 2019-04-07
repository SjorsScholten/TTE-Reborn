using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Experimental.Input;

public class PlayerMovement : MonoBehaviour, IMovementActions {

    public InputMaster controls;

    private Vector2 direction;

    [Serializable] public class OnMovePlayer : UnityEvent<Vector2> { }
    public OnMovePlayer OnMovePlayerEvent;

    private void Awake() {
        controls.Movement.SetCallbacks(this);
    }

    private void Update() {
        DoMove();
    }

    private void DoMove() {
        OnMovePlayerEvent.Invoke(direction);
    }

    public void OnMove(InputAction.CallbackContext context) {
        direction = context.ReadValue<Vector2>();
    }

    private void OnEnable() {
        controls.Enable();
    }

    private void OnDisable() {
        controls.Disable();
    }
}
