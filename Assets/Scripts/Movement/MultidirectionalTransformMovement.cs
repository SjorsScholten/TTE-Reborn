using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MultidirectionalTransformMovement : MonoBehaviour, ICharacterMovement {
    private float speedSmoothTime = 0.1f;
    private float speedSmoothVelocity;
    private float currentSpeed;
    private Transform myTransform;

    private void Awake() {
        myTransform = GetComponent<Transform>();
    }

    public void Move(Vector2 direction, float speed) {
        var targetSpeed = speed * direction.magnitude;
        currentSpeed = Mathf.SmoothDamp(currentSpeed, targetSpeed, ref speedSmoothVelocity, speedSmoothTime);
        var velocity = myTransform.forward * currentSpeed;
        myTransform.position += velocity * Time.deltaTime;
    }
}
