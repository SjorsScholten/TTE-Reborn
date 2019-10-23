using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviourSingleton<CameraFollow> {

    [Header("Following")]
    [Range(0, 2)]
    [SerializeField] private float damp;
    [SerializeField] private Transform follow;

    [Header("Bounds")]
    public Room roomBounds;
    [SerializeField] private Vector2 boundsMin;
    [SerializeField] private Vector2 boundsMax;

    private Vector3 velocity = Vector3.zero;
    private Camera camera;
    private Vector3 destination;
    private float verticalExtent;
    private float horizontalExtent;

    public float Damp { get { return damp; } set { damp = value; } }
    public bool IsFollowing { get; set; }

    private void Start() {
        camera = GetComponent<Camera>();
        verticalExtent = camera.orthographicSize;
        horizontalExtent = camera.aspect * verticalExtent;
        IsFollowing = true;
    }

    void Update() {
        if (follow != null && IsFollowing) {
            destination = GetDestination();

            transform.position = Vector3.SmoothDamp(transform.position, new Vector3(destination.x, destination.y, transform.position.z), ref velocity, damp);
        }
    }

    public Vector3 GetDestination() {
        destination = follow.position;
        destination.z = transform.position.z;

        Bounds bounds = roomBounds.GetComponent<SpriteRenderer>().bounds;

        Vector2 minBounds = new Vector2(bounds.min.x + horizontalExtent, bounds.min.y + verticalExtent);
        Vector2 maxBounds = new Vector2(bounds.max.x - horizontalExtent, bounds.max.y - verticalExtent);

        destination.x = Mathf.Clamp(destination.x, minBounds.x, maxBounds.x);

        destination.y = Mathf.Clamp(destination.y, minBounds.y, maxBounds.y);

        return destination;
    }
}