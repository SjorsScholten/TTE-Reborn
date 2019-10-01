using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class ForceReloadScene : MonoBehaviour, PlayerControls.IDebugActions {

    private PlayerControls input;

    private void Awake() {
        input = new PlayerControls();
        input.Debug.SetCallbacks(this);
    }

    private void OnEnable() {
        input.Debug.Enable();
    }

    private void OnDisable() {
        input.Debug.Disable();
    }

    public void OnReloadScene(InputAction.CallbackContext context) {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
