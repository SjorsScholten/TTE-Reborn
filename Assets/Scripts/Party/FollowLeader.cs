using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowLeader : MonoBehaviour {

    public List<Transform> partyMembers;

    [SerializeField] private float distanceRadius;

    void Start() {
        partyMembers[0].GetComponent<PlayerMovement>().isActive = true;
    }

    void Update() {
        for (int i = 1; i < partyMembers.Count; i++) {
            var member = partyMembers[i].transform;
            var target = partyMembers[i - 1].transform;

            var maxVelocity = member.GetComponent<MultidirectionalTransformMovement>().MoveSpeed;
            var animator = member.GetComponent<Animator>();
            var velocity = (target.position - member.position).normalized * maxVelocity;

            if (Vector2.Distance(target.position, member.position) >= distanceRadius) {               
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
