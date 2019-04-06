using System;
using UnityEngine;

public class SimpleDialogue : MonoBehaviour, IInteractable {

    [SerializeField] [TextArea]
    private String dialogue;

    public void Interact() {
        OpenDialogue();
    }

    private void OpenDialogue() {
        //todo: show dialogue on screen
    }
}
