using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System;

public class EnemyMovement : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField]
    private Animator animator;

    [SerializeField]
    [Tooltip("Range that the enemy will keep from it's target.")]
    [Range(0, 3)]
    private float minDistance;

    [Header("Detection")]

    [SerializeField]
    [Tooltip("Layers of the targets.")]
    private LayerMask layerMask;

    [SerializeField]
    [Tooltip("Total range of detection.")]
    private float detectionRadius;

    [SerializeField]
    [Tooltip("Takes up the outer % of the detection radius.")]
    [Range(0,0.8f)]
    private float alertRadiusPercentage;

    [SerializeField]
    [Tooltip("Time before enemy will go out of alert and aggros the player.")]
    private float alertTime;

    [SerializeField]
    [Tooltip("GameObject used to show that the enemy is on alert.")]
    private GameObject alertIcon;

    [Serializable] public class OnMoveEnemy : UnityEvent<Vector2> { }

    [Header("Events")]
    public OnMoveEnemy OnMoveEnemyEvent;
    public OnMoveEnemy OnAttack;

    #region Walk Direction
    private Vector2 direction;
    private Vector2 lastDirection;
    #endregion

    #region Targets
    private Collider2D[] possibleTargets;
    private Collider2D target;
    #endregion

    #region Detection
    private float alertZone;
    private float alertTimer;
    private bool alert;
    private Collider2D alertTarget;
    #endregion

    private Vector3 debugDirection;

    private void Awake()
    {
        alertZone = detectionRadius - (detectionRadius * alertRadiusPercentage);
        lastDirection = Vector2.down;
    }

    // Update is called once per frame
    void Update()
    {
        UpdateTargets();

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

        if (possibleTargets.Length == 0)
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
            if (possibleTargets.Length > 0)
            {
                foreach (Collider2D t in possibleTargets)
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

    private void UpdateTargets()
    {
        possibleTargets = Physics2D.OverlapCircleAll(this.transform.position, detectionRadius, layerMask);
        if (Array.IndexOf(possibleTargets, target) == -1)
        {
            target = null;
        }
        else if (Array.IndexOf(possibleTargets, alertTarget) == -1)
        {
            DeactivateAlert();
        }
    }

    private bool TryTarget(Collider2D t)
    {
        //Set positions
        Vector3 origin = this.transform.position;
        Vector3 directionVector3 = t.transform.position - origin;

        //Debug
        debugDirection = directionVector3;

        //If distance > minDistance then move on
        float distance = directionVector3.magnitude;

        Vector2 directionV2 = new Vector2(directionVector3.x, directionVector3.y);

        if (distance > minDistance)
        {
            RaycastHit2D hit = Physics2D.Raycast(new Vector2(origin.x, origin.y), directionV2, detectionRadius, layerMask.value);

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
