using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tank : Unit
{
    public Rigidbody2D rb;

    public float currentSpeed;
    public float acceleration;
    public float maxSpeed;
    public float currentMaxSpeed;
    protected float drag = 3f;
    public float rotation;
    protected float rotationSpeed = 100f;

    protected void Start()
    {
        currentMaxSpeed = maxSpeed;
        rb = GetComponent<Rigidbody2D>();
        base.Start();
    }

    protected void Move()
    {
        if (currentSpeed > currentMaxSpeed)
        {
            currentSpeed -= drag * Time.deltaTime;
        } else if (currentSpeed < -currentMaxSpeed) {
            currentSpeed += drag * Time.deltaTime;
        }
        transform.Translate(new Vector3(currentSpeed, 0, 0) * Time.deltaTime);
        transform.eulerAngles = Vector3.forward * rotation;
        cannon.transform.position = transform.position + (2 * new Vector3(Mathf.Cos(aimDirection * Mathf.Deg2Rad), Mathf.Sin(aimDirection * Mathf.Deg2Rad), 0) / 3);
        cannon.transform.eulerAngles = Vector3.forward * aimDirection;
    }

    protected void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Wall" || collision.gameObject.tag == "Tank" || collision.gameObject.tag == "Player")
        {
            currentSpeed = -(currentSpeed / 2);
            rb.velocity = Vector2.zero;

        }
        else if (collision.gameObject.tag == "Void")
        {
            currentSpeed = 0;
            rb.velocity = Vector2.zero;
        } 
        else
        {
            base.OnCollisionEnter2D(collision);
        }
    }

    protected void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Offroad")
        {
            currentMaxSpeed = maxSpeed - ((float) (collision.gameObject.layer - 8) / 3);
        }
    }

    protected void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Offroad")
        {
            currentMaxSpeed = maxSpeed;
        }
    }
}
