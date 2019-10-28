using Pathfinding;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Seeker))]
public class PathfindingAI : MonoBehaviour {
    [SerializeField] private float nextWaypointDistance = 1.5f;
    [SerializeField] private float resetPath = 0.5f;

    private Path path;
    private int currentWaypoint = 0;
    private bool reachedEnd = false;
    private TimedVariable pathRefreshTimer = new TimedVariable();
    private Seeker seeker;
    private Rigidbody2D rb2d;
    private Transform target;

    public Vector2 Direction { get; private set; }

    public Transform Target {
        get {
            return target;
        }
        set {
            target = value;
            RefreshPath();
        }
    }

    private void Awake() {
        seeker = GetComponent<Seeker>();
        rb2d = GetComponent<Rigidbody2D>();
    }

    private void OnEnable() {
        pathRefreshTimer.time = resetPath;
        pathRefreshTimer.triggerAction = RefreshPath;
    }

    private void Update() {
        if (target) {
            pathRefreshTimer.StepFrame();
        } 
        
        if (path != null) {
            if (currentWaypoint >= path.vectorPath.Count) {
                reachedEnd = true;
                return;
            } else {
                reachedEnd = false;
            }

            Direction = (Vector2)path.vectorPath[currentWaypoint] - rb2d.position;

            float distance = Vector2.Distance(rb2d.position, path.vectorPath[currentWaypoint]);
            if (distance < nextWaypointDistance) {
                currentWaypoint++;
            }
        }
    }

    private void RefreshPath() {
        if (target && seeker.IsDone()) {           
            seeker.StartPath(rb2d.position, target.position, OnPathComplete);
            pathRefreshTimer.Reset();
        }
    }

    private void OnPathComplete(Path p) {
        if (!p.error) {
            path = p;
            currentWaypoint = 0;
        }
    }
}
