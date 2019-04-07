using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimplePickUp : MonoBehaviour, IInteractable {
    
    [SerializeField] 
    private GameObject item;
    
    public void Interact() {
        if (AddItemToInventory()) {
            Destroy(gameObject);
        }
    }

    private bool AddItemToInventory() {
        //todo: add item to inventory
        return false;
    }
}
