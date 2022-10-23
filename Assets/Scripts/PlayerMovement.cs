using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public Weapon weapon;
    public GameObject cannon;
    Rigidbody2D rb;

    public float currentSpeed = 0f;
    float acceleration = 2f;
    float maxSpeed = 2f;
    float drag = 3f;
    public float rotation = 0f;
    float rotationSpeed = 100f;
    float aimDirection = 0f;
    float aimSpeed = 100f;
    float fireForce = 4f;
    int maxBounces = 2;
    int maxBullets = 3;


    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        InputCheck();
        Move();
    }

    void InputCheck()
    {
        if (Input.GetKey(KeyCode.LeftArrow) && !Input.GetKey(KeyCode.RightArrow))
        {
            rotation += rotationSpeed * Time.deltaTime;
            if (rotation > 360f)
            {
                rotation -= 360f;
            }
        } else if (Input.GetKey(KeyCode.RightArrow))
        {
            rotation -= rotationSpeed * Time.deltaTime;
            if (rotation < 0f)
            {
                rotation += 360f;
            }
        }
        if (Input.GetKey(KeyCode.Z) && !Input.GetKey(KeyCode.X))
        {
            aimDirection += aimSpeed * Time.deltaTime;
            if (aimDirection > 360f)
            {
                aimDirection -= 360f;
            }
        } else if (Input.GetKey(KeyCode.X))
        {
            aimDirection -= aimSpeed * Time.deltaTime;
            if (aimDirection < 0f)
            {
                aimDirection += 360f;
            }
        }
        if (Input.GetKey(KeyCode.UpArrow) && !Input.GetKey(KeyCode.DownArrow))
        {
            currentSpeed += acceleration * Time.deltaTime;

            if (currentSpeed > maxSpeed)
            {
                currentSpeed = maxSpeed;
            }
        }
        else if (Input.GetKey(KeyCode.DownArrow))
        {
            currentSpeed -= acceleration * Time.deltaTime;

            if (currentSpeed < -maxSpeed)
            {
                currentSpeed = -maxSpeed;
            }
        }
        else
        {
            if (currentSpeed > 0.001f)
            {
                currentSpeed -= (drag * Time.deltaTime);
            } else if (currentSpeed < -0.001f)
            {
                currentSpeed += (drag * Time.deltaTime);
            } else
            {
                currentSpeed = 0f;
            }
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            weapon.Fire(maxBounces, fireForce, maxBullets);
        }
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
        } else if (collision.gameObject.tag == "Wall")
        {
            rb.velocity = Vector2.zero;
        } else if (collision.gameObject.tag == "Bullet")
        {
            //Destroy(gameObject);
            rb.velocity = Vector2.zero;
        }
    }
}
