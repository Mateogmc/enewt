using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class V1AI: MonoBehaviour
{
    public Weapon weapon;
    public GameObject cannon;
    public GameObject player = null;
    Rigidbody2D rb;

    float aimDirection;
    public float aimSpeed;
    public float fireForce;
    public int maxBounces;
    public int maxBullets;
    float minAngle;
    float maxAngle;
    float oldAngle = 0f;
    float currentAngle;
    bool clockwise;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    private void Update()
    {
        if (player != null)
        {
            CheckDirection();
            Move();
            Fire();
        }
    }

    void CheckDirection()
    {
        currentAngle = Mathf.Atan2(player.transform.position.y - transform.position.y, player.transform.position.x - transform.position.x) * Mathf.Rad2Deg;
        minAngle = currentAngle - 50f;
        maxAngle = currentAngle + 50f;
        
        Debug.DrawRay(transform.position, new Vector3(Mathf.Cos(aimDirection * Mathf.Deg2Rad), Mathf.Sin(aimDirection * Mathf.Deg2Rad), 0) * 6, Color.black);
        Debug.DrawRay(transform.position, new Vector3(Mathf.Cos(currentAngle * Mathf.Deg2Rad), Mathf.Sin(currentAngle * Mathf.Deg2Rad), 0) * 6, Color.green);
        Debug.DrawRay(transform.position, new Vector3(Mathf.Cos(minAngle * Mathf.Deg2Rad), Mathf.Sin(minAngle * Mathf.Deg2Rad), 0) * 6, Color.magenta);
        Debug.DrawRay(transform.position, new Vector3(Mathf.Cos(maxAngle * Mathf.Deg2Rad), Mathf.Sin(maxAngle * Mathf.Deg2Rad), 0) * 6, Color.yellow);
        
    }

    void Move()
    {
        if (oldAngle > 175 && currentAngle < -175) {
            aimDirection -= 360f;
        } else if (oldAngle < -175 && currentAngle > 175)
        {
            aimDirection += 360f;
        }
        if (aimDirection < minAngle)
        {
            aimDirection += aimSpeed * Time.deltaTime;
            clockwise = false;
        } else if (aimDirection > maxAngle)
        {
            aimDirection -= aimSpeed * Time.deltaTime;
            clockwise = true;
        } else
        {
            if (clockwise)
            {
                aimDirection -= aimSpeed * Time.deltaTime;
            } else
            {
                aimDirection += aimSpeed * Time.deltaTime;
            }
        }
        //Debug.Log(minAngle + ", " + maxAngle + ", " + aimDirection);
        oldAngle = currentAngle;
        cannon.transform.position = transform.position + (2 * new Vector3(Mathf.Cos(aimDirection * Mathf.Deg2Rad), Mathf.Sin(aimDirection * Mathf.Deg2Rad), 0) / 5);
        cannon.transform.eulerAngles = Vector3.forward * aimDirection;
    }

    void Fire()
    {
        RaycastHit2D hit = Physics2D.Raycast(cannon.transform.position, new Vector2(Mathf.Cos(aimDirection * Mathf.Deg2Rad), Mathf.Sin(aimDirection * Mathf.Deg2Rad)));
        if (hit.collider.tag == "Player")
        {
            weapon.Fire(maxBounces, fireForce, maxBullets);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Bullet")
        {
            Destroy(gameObject);
        } else
        {
            rb.velocity = Vector2.zero;
        }
    }
}