using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowLeader : MonoBehaviour {

<<<<<<< Updated upstream
    public Transform target;

    [SerializeField] private float distanceRadius;

    private float _maxVelocity;
    private Vector3 _velocity;
    private Vector3 otherVelocity;

=======
    public List<Transform> partyMembers;

    [SerializeField] private float distanceRadius;

>>>>>>> Stashed changes
    void Start() {
        _maxVelocity = GetComponent<MultidirectionalTransformMovement>().MoveSpeed;
    }

    void Update() {
<<<<<<< Updated upstream
        _velocity = (target.position - transform.position).normalized * _maxVelocity;

        if (Vector2.Distance(target.position, transform.position) > distanceRadius) {
            _velocity = Vector3.SmoothDamp(_velocity, Vector3.zero, ref otherVelocity, 0.7f);
            transform.position += _velocity * Time.deltaTime;
        } 
    }

}
=======
        for (int i = 1; i < partyMembers.Count; i++) {
            var member = partyMembers[i].transform;
            var target = partyMembers[i - 1].transform;

            var maxVelocity = member.GetComponent<MultidirectionalTransformMovement>().MoveSpeed;
            var animator = member.GetComponent<Animator>();
            var velocity = (target.position - member.position).normalized * maxVelocity;

            if (Vector2.Distance(target.position, member.position) > distanceRadius) {
                member.position += velocity * Time.deltaTime;
            }

            AnimateFollower(member, velocity);
        }
    }

    private void AnimateFollower(Transform member, Vector3 velocity) {
        var animator = member.GetComponent<Animator>();
        var targetAnimator = partyMembers[0].GetComponent<Animator>();

        animator.SetBool("Idle", targetAnimator.GetBool("Idle"));
        animator.SetFloat("Horizontal", velocity.x);
        animator.SetFloat("Vertical", velocity.y);
    }
}
>>>>>>> Stashed changes
