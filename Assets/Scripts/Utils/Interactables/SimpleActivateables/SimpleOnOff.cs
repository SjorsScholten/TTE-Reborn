using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleOnOff : MonoBehaviour, IActivateable {
    private bool value = false;

    public void Activate() {
        value = !value;
    }
}
