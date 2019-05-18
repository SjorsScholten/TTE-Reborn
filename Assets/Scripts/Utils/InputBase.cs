using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputBase : MonoBehaviour {

    public InputMaster controls;

    /// <summary>
    /// Enable controls
    /// </summary>
    private void OnEnable() {
        controls.Enable();
    }

    /// <summary>
    /// Disable controls
    /// </summary>
    private void OnDisable() {
        controls.Disable();
    }
}
