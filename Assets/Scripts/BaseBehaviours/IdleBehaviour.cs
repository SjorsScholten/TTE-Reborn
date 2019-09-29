using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleBehaviour : MonoBehaviour {

    protected Animator animator;

    void Awake() {
        animator = GetComponent<Animator>();
    }

    public virtual void Idle(string animation) {
        animator.Play(animation);
    }

}
