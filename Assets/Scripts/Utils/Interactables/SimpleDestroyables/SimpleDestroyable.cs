using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleDestroyable : MonoBehaviour, IDestroyable{
    public void Destroy() {
        Destroy(gameObject);
    }
}
