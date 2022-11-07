using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;
using System.Threading.Tasks;

public class V2AI : MonoBehaviour
{
    public Weapon weapon;
    public GameObject cannon;
    public GameObject player;
    public LayerMask ignoreLayer;
    GameObject gameObj;
    Transform target;

    Rigidbody2D rb;
    public AIPath aiPath;
    Seeker seeker;

    Path path;
    int currentWaypoint = 0;
    float nextWayPointDistance = 0.8f;
    bool reachedEndOfPath = false;

    public float currentSpeed = 0f;
    public float acceleration;
    public float maxSpeed;
    public float drag;
    public float rotation;
    public float rotationSpeed;
    float lastRotation;
    float aimDirection = 0f;
    public float aimSpeed;
    public float fireForce;
    public int maxBounces;
    public int maxBullets;
    public float fireCooldown;
    float lastFired;
    int fireDelay = 2000;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        seeker = GetComponent<Seeker>();
        gameObj = gameObject;
        lastFired = Time.time - fireCooldown;

        player = GameObject.FindGameObjectWithTag("Player");
        InvokeRepeating("UpdatePath", 0f, 0.2f);
    }

    void UpdatePath()
    {
        if (player != null)
        {
            target = player.transform;
            seeker.StartPath(rb.position, target.position, OnPathComplete);
        }
    }

    void Update()
    {
        if (player != null)
        {
            if (path == null)
            {
                return;
            }

            if (currentWaypoint >= path.vectorPath.Count)
            {
                reachedEndOfPath = true;
                return;
            }
            else
            {
                reachedEndOfPath = false;
            }
            Aim();
            Move();
            Fire();
        } else
        {
            player = GameObject.FindGameObjectWithTag("Player");
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

    void Move()
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
        } else if (lastRotation > 330 && angle < 30) {
            rotation -= 360;
        }
        if (rotation < angle - 3)
        {
            rotation += rotationSpeed * Time.deltaTime;
        } else if (rotation > angle + 3)
        {
            rotation -= rotationSpeed * Time.deltaTime;
        } else
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
        } else if (rotation < 0)
        {
            rotation = 0;
        }

        lastRotation = angle;

        transform.Translate(new Vector3(currentSpeed, 0, 0) * Time.deltaTime);
        transform.eulerAngles = Vector3.forward * rotation;
        cannon.transform.position = transform.position + (new Vector3(Mathf.Cos(aimDirection * Mathf.Deg2Rad), Mathf.Sin(aimDirection * Mathf.Deg2Rad), 0) / 2);
        cannon.transform.eulerAngles = Vector3.forward * aimDirection;
    }

    void Aim()
    {
        aimDirection = Mathf.Atan2(player.transform.position.y - transform.position.y, player.transform.position.x - transform.position.x) * Mathf.Rad2Deg;
    }

    async void Fire()
    {
        RaycastHit2D hit = Physics2D.Raycast(cannon.transform.position, new Vector2(Mathf.Cos(aimDirection * Mathf.Deg2Rad), Mathf.Sin(aimDirection * Mathf.Deg2Rad)), 100f, ~ignoreLayer);
        if (hit.collider.tag == "Player")
        {
            await Task.Delay(fireDelay);
            if (Time.time >= lastFired + fireCooldown)
            {
                if (gameObj != null)
                {
                    hit = Physics2D.Raycast(cannon.transform.position, new Vector2(Mathf.Cos(aimDirection * Mathf.Deg2Rad), Mathf.Sin(aimDirection * Mathf.Deg2Rad)), 100f, ~ignoreLayer);
                    if (hit.collider.tag == "Player")
                    {
                        weapon.Fire(maxBounces, fireForce, maxBullets);
                        lastFired = Time.time;
                    }
                }
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Wall" || collision.gameObject.tag == "Tank" || collision.gameObject.tag == "Player")
        {
            currentSpeed = -(currentSpeed / 2);
            rb.velocity = Vector2.zero;

        }
        else if (collision.gameObject.tag == "Bullet")
        {
            Destroy(gameObject);
        }
    }
}
