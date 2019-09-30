using Ink.Runtime;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using System.Linq;
using UnityEngine.InputSystem;

public class DisplayDialog : MonoBehaviour
{
    //public InputMaster controls;
    public TextAsset inkJSON;

    [Header("Settings")]
    [Range(0.01f, 0.1f)] [SerializeField]
    private float displaySpeed;
    [SerializeField]
    private int maxLetters;

    [Header("Text Object")]
    [SerializeField]
    private TextMeshProUGUI dialogText;

    private Story ink;
    private string display;
    private bool lineCompleted;

    private void Awake() {
        //controls.Cutscenes.SetCallbacks(this);
    }

    private void OnEnable() {
        //controls.Enable();
    }

    private void OnDisable() {
        //controls.Disable();
    }

    private void Start() {
        InitiateDialog();
    }

    public void InitiateDialog() {
        ink = new Story(inkJSON.text);

        NextLine();
    }

    private void NextLine() {
        if (!ink.canContinue) {
            return;
        }

        ink.Continue();

        var inkSplit = ink.currentText.Split(null);
        inkSplit = inkSplit.Take(inkSplit.Count() - 1).ToArray();

        dialogText.text = "";

        display = CreateLine(inkSplit);

        StartCoroutine(DisplayText());
    }

    private IEnumerator DisplayText() {
        lineCompleted = false;

        while (dialogText.text.Length < display.Length) {
            dialogText.text += display[dialogText.text.Length];
            yield return new WaitForSecondsRealtime(displaySpeed);
        }

        lineCompleted = true;
    }

    private string CreateLine(string[] splitLine) {
        var finalLine = "";
        var count = 0;
        var words = splitLine;

        foreach (var word in words) {
            count += word.Length + 1;

            if (count > maxLetters) {
                count = word.Length + 1;
                finalLine += '\n';
            }

            finalLine += word + ' ';
        }

        return finalLine;
    }

    public void OnConfirm(InputAction.CallbackContext context) {
        if (lineCompleted) {
            NextLine();
        }
    }
}
