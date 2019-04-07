using UnityEngine;

public class SimpleDestroyer : MonoBehaviour, IInteractable {
    [SerializeField] 
    private GameObject[] destroyables;


    public void Interact() {
        DestroyObjects();
    }

    private void DestroyObjects() {
        foreach (var destroyable in destroyables) {
            destroyable.GetComponent<IDestroyable>().Destroy();
        }
    }
}
