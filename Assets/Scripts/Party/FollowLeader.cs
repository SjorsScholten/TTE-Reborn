using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowLeader : MonoBehaviour {

    public Transform target;

    [SerializeField] private float distanceRadius;

    private float _maxVelocity;
    private Vector3 _velocity;
    private Vector3 otherVelocity;

    void Start() {
        _maxVelocity = GetComponent<MultidirectionalTransformMovement>().MoveSpeed;
    }

    void Update() {
        _velocity = (target.position - transform.position).normalized * _maxVelocity;

        if (Vector2.Distance(target.position, transform.position) > distanceRadius) {
            _velocity = Vector3.SmoothDamp(_velocity, Vector3.zero, ref otherVelocity, 0.7f);
            transform.position += _velocity * Time.deltaTime;
        } 
    }

}
