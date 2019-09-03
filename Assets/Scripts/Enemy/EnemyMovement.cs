using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System;

public class EnemyMovement : MonoBehaviour
{
    [SerializeField]
    private LayerMask layerMask;

    [SerializeField]
    [Range(0, 3)]
    private float minDistance;

    [SerializeField]
    private Animator animator;

    [Serializable] public class OnMoveEnemy : UnityEvent<Vector2> { }

    [Header("Events")]
    public OnMoveEnemy OnMoveEnemyEvent;

    #region Walk Direction
    private Vector2 direction;
    private Vector2 lastDirection;
    #endregion

    #region Targets
    private List<Transform> possibleTargets;
    private Transform target;
    #endregion

    private Vector3 debugDirection;

    private void Awake()
    {
        possibleTargets = new List<Transform>();
        lastDirection = Vector2.down;
    }

    // Update is called once per frame
    void Update()
    {
        if (target != null)
        {
            TryTarget(target);

        }
        else
        {
            if (possibleTargets.Count > 0)
            {
                foreach (Transform t in possibleTargets)
                {
                    if (TryTarget(t))
                    {
                        return;
                    }
                }
            }

            //This code only runs if there are no targets.
            //Debug
            debugDirection = Vector3.zero;

            //Idle
            animator.SetBool("Idle", true);
            animator.SetFloat("Horizontal", lastDirection.x);
            animator.SetFloat("Vertical", lastDirection.y);
        }
    }

    private bool TryTarget(Transform t)
    {
        //Set positions
        Vector3 origin = this.transform.position;
        Vector3 directionVector3 = t.position - origin;

        //Debug
        debugDirection = directionVector3;

        //If distance > minDistance then move on
        if (directionVector3.magnitude > minDistance)
        {

            Vector2 directionV2 = new Vector2(directionVector3.x, directionVector3.y);

            RaycastHit2D hit = Physics2D.Raycast(new Vector2(origin.x, origin.y), directionV2, 10, layerMask.value);

            //When a playable is found walk in that direction.
            if (hit.transform.tag == NYRA.Tag.Playable)
            {
                this.direction = directionV2;
                DoMove();
                return true;
            }
        }
        else
        {
            return true;
        }
        return false;

    }

    /// <summary>
    /// Move Character and update animator.
    /// </summary>
    private void DoMove()
    {
        OnMoveEnemyEvent.Invoke(direction.normalized);
        animator.SetBool("Idle", false);
        animator.SetFloat("Horizontal", direction.x);
        animator.SetFloat("Vertical", direction.y);
        lastDirection = direction;
    }

    /// <summary>
    /// If Collision with Playable then set target
    /// </summary>
    /// <param name="other"></param>
    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == NYRA.Tag.Playable && !possibleTargets.Contains(other.transform))
        {
            possibleTargets.Add(other.transform);
        }
    }

    /// <summary>
    /// If Playable leaves then set target to null
    /// </summary>
    /// <param name="other"></param>
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == NYRA.Tag.Playable && possibleTargets.Contains(other.transform))
        {
            possibleTargets.Remove(other.transform);
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawRay(this.transform.position, debugDirection);
    }
}
