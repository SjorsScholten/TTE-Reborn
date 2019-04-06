using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TriggerReporter : MonoBehaviour {

    [SerializeField]
    private bool onlyTriggerOnce;
    private bool hasTriggered;

    [SerializeField]
    private bool disableAfter;

    [Serializable]
    public class OnTriggerEnter : UnityEvent<Collider2D> { }
    public OnTriggerEnter onTriggerEnterEvent;

    [Serializable]
    public class OnTriggerExit : UnityEvent<Collider2D> { }
    public OnTriggerExit onTriggerExitEvent;

    [SerializeField]
    private bool ignoreEnemies;

    void Start() {

    }

    private void OnTriggerEnter2D(Collider2D other) {
        if (ignoreEnemies && other.CompareTag(Tags.Enemies.ToString())) {
            return;
        }

        if (onlyTriggerOnce && hasTriggered) {
            return;
        }

        hasTriggered = true;

        if (onTriggerEnterEvent != null) {
            onTriggerEnterEvent.Invoke(other);
        }

        if (disableAfter) {
            gameObject.SetActive(false);
        }
    }

    private void OnTriggerExit2D(Collider2D other) {
        if (ignoreEnemies && other.CompareTag(Tags.Enemies.ToString())) {
            return;
        }

        if (onlyTriggerOnce && hasTriggered) {
            return;
        }


        if (onTriggerExitEvent != null) {
            onTriggerExitEvent.Invoke(other);
        }
    }
}
