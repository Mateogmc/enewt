using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class MovingEnemy : Tank
{
    protected GameObject player;
    protected GameObject gameObj;
    protected Transform target;

    public AIPath aipath;
    protected Seeker seeker;
    protected Path path;
    protected int currentWaypoint = 0;
    protected float nextWayPointDistance = 0.8f;
    protected bool reachedEndOfPath = false;
    public float minDistance;

    public int fireDelay;
    protected float lastRotation;

    protected void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        seeker = GetComponent<Seeker>();
        gameObj = gameObject;

        player = GameObject.FindGameObjectWithTag("Player");
        InvokeRepeating("UpdatePath", 0f, 0.2f);
        base.Start();
    }

    void UpdatePath()
    {
        if (player != null)
        {
            target = player.transform;
            seeker.StartPath(rb.position, target.position, OnPathComplete);
        }
    }

    void OnPathComplete(Path p)
    {
        if (!p.error)
        {
            path = p;
            currentWaypoint = 0;
        }
    }

    protected void Move()
    {
        float angle = Mathf.Atan2(transform.position.y - path.vectorPath[currentWaypoint].y, transform.position.x - path.vectorPath[currentWaypoint].x) * Mathf.Rad2Deg + 180;

        //Debug.DrawRay(transform.position, new Vector3(Mathf.Cos(angle * Mathf.Deg2Rad), Mathf.Sin(angle * Mathf.Deg2Rad)) * 6, Color.red);

        float distance = Vector2.Distance(rb.position, path.vectorPath[currentWaypoint]);
        if (distance < nextWayPointDistance)
        {
            currentWaypoint++;
        }
        //Debug.Log(rotation + " " + angle);

        if (lastRotation < 30 && angle > 330)
        {
            rotation += 360;
        }
        else if (lastRotation > 330 && angle < 30)
        {
            rotation -= 360;
        }
        if (rotation < angle - 3)
        {
            rotation += rotationSpeed * Time.deltaTime;
        }
        else if (rotation > angle + 3)
        {
            rotation -= rotationSpeed * Time.deltaTime;
        }
        else
        {
            currentSpeed += acceleration * Time.deltaTime;
            if (currentSpeed > maxSpeed)
            {
                currentSpeed = maxSpeed;
            }
        }

        if (rotation > 360)
        {
            rotation = 360;
        }
        else if (rotation < 0)
        {
            rotation = 0;
        }

        lastRotation = angle;
        base.Move();
    }

    protected void Aim()
    {
        aimDirection = Mathf.Atan2(player.transform.position.y - transform.position.y, player.transform.position.x - transform.position.x) * Mathf.Rad2Deg;
    }
}
