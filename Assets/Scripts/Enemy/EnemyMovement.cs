using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System;

[RequireComponent(typeof(CircleCollider2D))]
public class EnemyMovement : MonoBehaviour
{
    [SerializeField]
    private LayerMask layerMask;

    [SerializeField]
    [Range(0, 3)]
    private float minDistance;

    [SerializeField]
    private Animator animator;

    [SerializeField]
    private GameObject alertIcon;

    [SerializeField]
    private float alertTime;

    [Serializable] public class OnMoveEnemy : UnityEvent<Vector2> { }

    [Header("Events")]
    public OnMoveEnemy OnMoveEnemyEvent;
    public OnMoveEnemy OnAttack;

    #region Walk Direction
    private Vector2 direction;
    private Vector2 lastDirection;
    #endregion

    #region Targets
    private List<Transform> possibleTargets;
    private Transform target;
    #endregion

    #region Detection
    private CircleCollider2D detectionZone;
    private float alertZone;
    private float alertTimer;
    private bool alert;
    private Transform alertTarget;
    #endregion

    private Vector3 debugDirection;

    private void Awake()
    {
        detectionZone = GetComponent<CircleCollider2D>();
        alertZone = detectionZone.radius - (detectionZone.radius * 0.15f);
        possibleTargets = new List<Transform>();
        lastDirection = Vector2.down;
    }

    // Update is called once per frame
    void Update()
    {
        //updates timer
        //if timer has reached the max then a target will be set
        if (alert)
        {
            alertTimer += Time.deltaTime;
            if (alertTimer >= alertTime)
            {
                target = alertTarget;
                DeactivateAlert();
            }
        }

        if (possibleTargets.Count == 0)
        {
            alertIcon.SetActive(false);
        }
        if (target != null)
        {
            alertIcon.SetActive(false);
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
        float distance = directionVector3.magnitude;

        Vector2 directionV2 = new Vector2(directionVector3.x, directionVector3.y);

        if (distance > minDistance)
        {
            RaycastHit2D hit = Physics2D.Raycast(new Vector2(origin.x, origin.y), directionV2, detectionZone.radius, layerMask.value);

            //When a playable is found walk in that direction.
            if (hit == true && hit.transform.tag == NYRA.Tag.Playable)
            {
                if (target == null && distance > alertZone)
                {
                    alertTarget = t;
                    alert = true;
                    alertIcon.SetActive(true);
                }
                else
                {
                    target = t;
                    this.direction = directionV2;
                    DoMove();
                    return true;
                }
            }
        }
        else
        {
            OnAttack.Invoke(directionV2.normalized);
            return true;
        }
        return false;

    }

    /// <summary>
    /// Move Character and update animator.
    /// </summary>
    private void DoMove()
    {
        Vector2 normalizedDir = direction.normalized;

        OnMoveEnemyEvent.Invoke(normalizedDir);
        animator.SetBool("Idle", false);
        animator.SetFloat("Horizontal", normalizedDir.x);
        animator.SetFloat("Vertical", normalizedDir.y);
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

            if (target == other.transform)
            {
                target = null;
            }
            else if (alertTarget == other.transform)
            {
                DeactivateAlert();
            }
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawRay(this.transform.position, debugDirection);
    }

    private void DeactivateAlert()
    {
        alert = false;
        alertTimer = 0;
        alertIcon.SetActive(false);
        alertTarget = null;
    }
}
