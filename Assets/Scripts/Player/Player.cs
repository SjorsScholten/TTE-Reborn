using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Experimental.Input;

public class Player : MonoBehaviour {

    public InputMaster controls;

    private Vector2 direction;

    [Serializable] public class OnMovePlayer : UnityEvent<Vector2> { }
    public OnMovePlayer OnMovePlayerEvent;

    private void Awake() {
        controls.Movement.Move.performed += MovePlayer;
    }

    private void Update() {
        DoMove();
    }

    private void DoMove() {
        OnMovePlayerEvent.Invoke(direction);
    }

    private void MovePlayer(InputAction.CallbackContext obj) {
        direction = obj.ReadValue<Vector2>();        

        if (direction == Vector2.zero) {
            Debug.Log("ja hoor");
        }
    }

    private void OnEnable() {
        controls.Enable();
    }

    private void OnDisable() {
        controls.Disable();
    }
}
