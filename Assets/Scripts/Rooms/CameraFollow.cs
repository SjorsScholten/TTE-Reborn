using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour {

    [Header("Following")]
    [Range(0, 2)]
    public float damp;
    public Transform follow;

    [Header("Bounds")]
    public Room roomBounds;
    public Vector2 boundsMin;
    public Vector2 boundsMax;

    private Vector3 velocity = Vector3.zero;
    private Camera camera;
    private Vector3 destination;
    private float verticalExtent;
    private float horizontalExtent;

    private void Start() {
        camera = GetComponent<Camera>();
        verticalExtent = camera.orthographicSize;
        horizontalExtent = camera.aspect * verticalExtent;
    }

    void Update() {
        if (follow) {
            destination = follow.position;

            Bounds bounds = roomBounds.GetComponent<SpriteRenderer>().bounds;

            Vector2 minBounds = new Vector2(bounds.min.x + horizontalExtent, bounds.min.y + verticalExtent);
            Vector2 maxBounds = new Vector2(bounds.max.x - horizontalExtent, bounds.max.y - verticalExtent);

            destination.x = Mathf.Clamp(destination.x, minBounds.x, maxBounds.x);

            destination.y = Mathf.Clamp(destination.y, minBounds.y, maxBounds.y);

            transform.position = Vector3.SmoothDamp(transform.position, new Vector3(destination.x, destination.y, transform.position.z), ref velocity, damp);
        }
    }
}