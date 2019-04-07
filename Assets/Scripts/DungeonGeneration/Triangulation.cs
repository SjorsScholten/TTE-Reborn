using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Triangulation : MonoBehaviour {
    /**
     * X_i = points needed to connect
     * X_o = origin/seed of the web
     * X_j = closest X_i to X_o
     * X_k = X_i that makes the smallest circum-circle
     * C = the circum-circles center
     */

    private Vector2[] points;
    private Vector2 origin;
    
    private void Triangulate(Vector2[] roomArray) {
        throw new System.NotImplementedException ();
    }

    private void Flip() {
        throw new System.NotImplementedException ();
    }
    
}
