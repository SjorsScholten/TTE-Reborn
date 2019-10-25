using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MultidirectionalTransformMovement : MonoBehaviour, ICharacterMovement {
    [SerializeField] private float moveSpeed;

    private float speedSmoothTime = 0.1f;
    private float speedSmoothVelocity;
    private float currentSpeed;
    private Transform myTransform;
    private Rigidbody2D rb2d;

    public float MoveSpeed {
        get { return moveSpeed; }
    }

    private void Awake() {
        rb2d = GetComponent<Rigidbody2D>();
        myTransform = GetComponent<Transform>();
    }

    public void Move(Vector2 direction) {
        var targetSpeed = moveSpeed * direction.magnitude;
        currentSpeed = Mathf.SmoothDamp(currentSpeed, targetSpeed, ref speedSmoothVelocity, speedSmoothTime);
        Vector3 velocity = direction * currentSpeed;
        rb2d.MovePosition(myTransform.position + velocity * Time.deltaTime);
    }
}