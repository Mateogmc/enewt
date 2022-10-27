using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class V4AI : MonoBehaviour
{
    public Weapon weapon;
    public GameObject cannon;
    public GameObject player = null;
    public GameObject predictedTarget;
    public Vector3 targetPoint;
    Vector2 initialPosition;
    Rigidbody2D rb;

    float aimDirection;
    public float aimSpeed;
    public float fireForce;
    public int maxBounces;
    public int maxBullets;
    public float fireCooldown;
    float lastFired;
    float minAngle;
    float maxAngle;
    float oldAngle = 0f;
    float currentAngle;
    bool clockwise;

    private void Start()
    {
        initialPosition = transform.position;
        lastFired = Time.time - fireCooldown;
    }

    private void Update()
    {
        if (player != null)
        {
            CheckDirection();
            PredictPosition();
            Move();
            Fire(cannon.transform.position, new Vector2(Mathf.Cos(aimDirection * Mathf.Deg2Rad), Mathf.Sin(aimDirection * Mathf.Deg2Rad)), 0);
        } else
        {
            player = GameObject.FindGameObjectWithTag("Player");
        }
    }

    void CheckDirection()
    {
        currentAngle = Mathf.Atan2(player.transform.position.y - transform.position.y, player.transform.position.x - transform.position.x) * Mathf.Rad2Deg;
        minAngle = currentAngle - 100f;
        maxAngle = currentAngle + 100f;

        /*Debug.DrawRay(transform.position, new Vector3(Mathf.Cos(aimDirection * Mathf.Deg2Rad), Mathf.Sin(aimDirection * Mathf.Deg2Rad), 0) * 6, Color.black);
        Debug.DrawRay(transform.position, new Vector3(Mathf.Cos(currentAngle * Mathf.Deg2Rad), Mathf.Sin(currentAngle * Mathf.Deg2Rad), 0) * 6, Color.green);
        Debug.DrawRay(transform.position, new Vector3(Mathf.Cos(minAngle * Mathf.Deg2Rad), Mathf.Sin(minAngle * Mathf.Deg2Rad), 0) * 6, Color.magenta);
        Debug.DrawRay(transform.position, new Vector3(Mathf.Cos(maxAngle * Mathf.Deg2Rad), Mathf.Sin(maxAngle * Mathf.Deg2Rad), 0) * 6, Color.yellow);*/

    }

    void PredictPosition()
    {
        float rotation = player.GetComponent<PlayerMovement>().rotation;
        targetPoint = player.transform.position + (new Vector3(Mathf.Cos(rotation * Mathf.Deg2Rad), Mathf.Sin(rotation * Mathf.Deg2Rad), 0) * player.GetComponent<PlayerMovement>().currentSpeed);
    }

    void Move()
    {
        if (oldAngle > 175 && currentAngle < -175)
        {
            aimDirection -= 360f;
        }
        else if (oldAngle < -175 && currentAngle > 175)
        {
            aimDirection += 360f;
        }
        if (aimDirection < minAngle)
        {
            aimDirection += aimSpeed * Time.deltaTime;
            clockwise = false;
        }
        else if (aimDirection > maxAngle)
        {
            aimDirection -= aimSpeed * Time.deltaTime;
            clockwise = true;
        }
        else
        {
            if (clockwise)
            {
                aimDirection -= aimSpeed * Time.deltaTime;
            }
            else
            {
                aimDirection += aimSpeed * Time.deltaTime;
            }
        }
        //Debug.Log(minAngle + ", " + maxAngle + ", " + aimDirection);
        oldAngle = currentAngle;
        cannon.transform.position = transform.position + (new Vector3(Mathf.Cos(aimDirection * Mathf.Deg2Rad), Mathf.Sin(aimDirection * Mathf.Deg2Rad), 0) / 2);
        cannon.transform.eulerAngles = Vector3.forward * aimDirection;

        transform.position = initialPosition;
    }

    void Fire(Vector2 pos, Vector2 angle, int currentBounces)
    {
        //Debug.DrawRay(pos, angle * 5, Color.red);
        RaycastHit2D hit = Physics2D.Raycast(pos, angle);
        GameObject target = Instantiate(predictedTarget, targetPoint, Quaternion.identity);
        if (hit == true)
        {
            if (hit.collider.tag == "Player" || (hit.collider.tag == "Prediction" && currentBounces > 0))
            {
                if (Time.time >= lastFired + fireCooldown)
                {
                    weapon.Fire(maxBounces, fireForce, maxBullets);
                    lastFired = Time.time;
                }
            }
            else if (hit.collider.tag == "Wall")
            {
                if (currentBounces < maxBounces)
                {
                    Fire(hit.point, Vector2.Reflect(angle, hit.normal), ++currentBounces);
                }
            }
        }
        Destroy(target);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Bullet")
        {
            Destroy(gameObject);
        }
        else
        {
            rb.velocity = Vector2.zero;
        }
    }
}
