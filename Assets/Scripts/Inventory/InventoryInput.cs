using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Input;

public class InventoryInput : InputBase, IInventoryActions {

    [SerializeField]
    [Tooltip("Inventory tabs")]
    private List<GameObject> tabs;

    //Pages script of the current tab
    private Pages tabPages;

    private Vector2 direction;
    private int index = 0;

    void Awake() {
        controls.Inventory.SetCallbacks(this);
        tabPages = tabs[index].GetComponent<Pages>();
    }

    /// <summary>
    /// On Input, Read input value and change Tab.
    /// </summary>
    /// <param name="context"></param>
    public void OnTabs(InputAction.CallbackContext context) {
        direction = context.ReadValue<Vector2>();
        //Debug.Log(direction);
        ChangeTab();
    }

    /// <summary>
    /// Changes Tab depending on the input
    /// </summary>
    private void ChangeTab() {
        bool changeTab = false;
        if (direction.x < 0) {
            
            changeTab = true;
            index = HelperFunctions.GetIndex(index, -1, tabs.Count);
        }
        else if (direction.x > 0) {
            changeTab = true;
            index = HelperFunctions.GetIndex(index, 1, tabs.Count);
        }
        else if (direction.y < 0) {
            tabPages.NextPage(-1);
        }
        else if (direction.y > 0) {
            tabPages.NextPage(1);
        }

        if(changeTab) {
            Debug.Log("Change tab   index=" + index);
            tabs[index].transform.SetAsLastSibling();
            tabPages = tabs[index].GetComponent<Pages>();
            tabPages.Setup();
        }
    }
}
