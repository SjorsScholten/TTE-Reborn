using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room : MonoBehaviour {
    // Start is called before the first frame update
    void Start() {
        
    }

    // Update is called once per frame
    void Update() {

    }

    private void OnDrawGizmos() {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireCube(transform.position, new Vector3(GetComponent<SpriteRenderer>().sprite.bounds.size.x * transform.localScale.x, GetComponent<SpriteRenderer>().sprite.bounds.size.y * transform.localScale.y));
    }
}
