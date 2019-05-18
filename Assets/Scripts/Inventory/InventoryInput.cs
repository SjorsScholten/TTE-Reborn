using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Input;

public class InventoryInput : MonoBehaviour, IInventoryActions {
    public InputMaster controls;

    [SerializeField]
    [Tooltip("Inventory tabs")]
    private List<GameObject> Tabs;

    private float direction;
    private int index = 0;

    void Awake() {
        controls.Inventory.SetCallbacks(this);
    }

    /// <summary>
    /// On Input, Read input value and change Tab.
    /// </summary>
    /// <param name="context"></param>
    public void OnTabs(InputAction.CallbackContext context) {
        direction = context.ReadValue<float>();
        Debug.Log(direction);
        ChangeTab();
    }

    /// <summary>
    /// Changes Tab depending on the input
    /// </summary>
    private void ChangeTab() {
        if (direction < 0) {
            SetIndex(-1);
        }
        else {
            SetIndex(1);
        }
        Tabs[index].transform.SetAsLastSibling();
    }

    /// <summary>
    /// Set new index of Tab list.
    /// </summary>
    /// <param name="next"></param>
    private void SetIndex(int next) {
        index += next;
        if (index < 0) {
            index = Tabs.Count - 1;
        }
        else if (index == Tabs.Count) {
            index = 0;
        }
    }

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
