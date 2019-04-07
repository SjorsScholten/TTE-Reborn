using UnityEngine;
using UnityEngine.Events;

public class Interactable : MonoBehaviour, IInteractable {
    public UnityEvent onInteraction;
    
    public void Interact() {
        onInteraction.Invoke();
    }
}
