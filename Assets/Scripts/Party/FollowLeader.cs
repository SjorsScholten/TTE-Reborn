using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowLeader : MonoBehaviour {

    public Transform target;
    public List<Transform> partyMembers;

    [SerializeField] private float distanceRadius;

    private Vector3 otherVelocity;
    private Vector3 lastDirection;

    void Start() {
    }

    void Update() {
        foreach (Transform member in partyMembers) {
            if (member == target) continue;

            var maxVelocity = member.GetComponent<MultidirectionalTransformMovement>().MoveSpeed;
            var animator = member.GetComponent<Animator>();
            var velocity = (target.position - member.position).normalized * maxVelocity;

            if (Vector2.Distance(target.position, member.position) > distanceRadius) {
                member.position += velocity * Time.deltaTime;

                animator.SetBool("Idle", false);
                animator.SetFloat("Horizontal", velocity.x);
                animator.SetFloat("Vertical", velocity.y);
                lastDirection = velocity;
            } else {
                animator.SetBool("Idle", true);
            }
        }
    }

    private Vector3 CalculateSeperation(Transform agent, float speed) {
        return Vector3.zero;
    }

}