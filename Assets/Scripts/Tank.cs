using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tank : MonoBehaviour
{
    public Weapon weapon;
    public GameObject cannon;
    Rigidbody2D rb;

    float currentSpeed;
    public float acceleration;
    public float maxSpeed;
    float drag = 3f;
    float rotation;
    float rotationSpeed = 100f;
    float aimDirection;
    public float aimSpeed;
    public float fireForce;
    public int maxBounces;

    private void Update()
    {
        Move();
    }

    void Move()
    {
        transform.Translate(new Vector3(currentSpeed, 0, 0) * Time.deltaTime);
        transform.eulerAngles = Vector3.forward * rotation;
        cannon.transform.position = transform.position + (2 * new Vector3(Mathf.Cos(aimDirection * Mathf.Deg2Rad), Mathf.Sin(aimDirection * Mathf.Deg2Rad), 0) / 5);
        cannon.transform.eulerAngles = Vector3.forward * aimDirection;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Wall" || collision.gameObject.tag == "Tank" || collision.gameObject.tag == "Player")
        {
            currentSpeed = -(currentSpeed / 2);
        }
        else if (collision.gameObject.tag == "Wall")
        {
            rb.velocity = Vector2.zero;
        }
        else if (collision.gameObject.tag == "Bullet")
        {
            Destroy(gameObject);
        }
    }
}
