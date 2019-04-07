using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Experimental.Input;

public class Player : MonoBehaviour {

    public InputMaster controls;

    [Serializable] public class OnMovePlayer : UnityEvent<Vector2> { }
    public OnMovePlayer OnMovePlayerEvent;

    private void Awake() {
        controls.Movement.Move.performed += MovePlayer;
    }

    private void MovePlayer(InputAction.CallbackContext obj) {
        var direction = obj.ReadValue<Vector2>();
        OnMovePlayerEvent.Invoke(direction);
    }

    private void OnEnable() {
        controls.Enable();
    }

    private void OnDisable() {
        controls.Disable();
    }
}
