using UnityEngine;

public class SimpleLever : MonoBehaviour, IInteractable {

    [SerializeField]
    private GameObject[] activateables;
    
    public void Interact() {
        Switch();
    }

    private void Switch() {
        foreach (var activateable in activateables) {
            activateable.GetComponent<IActivateable>().Activate();
        }
    }
}
