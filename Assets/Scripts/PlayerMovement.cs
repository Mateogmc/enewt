using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    JSONReader reader;
    public Weapon weapon;
    public GameObject cannon;
    JSONReader.Player controls;
    Rigidbody2D rb;
    public int playerId;

    public float currentSpeed = 0f;
    public float acceleration = 4f;
    public float maxSpeed = 2f;
    public float drag = 3f;
    public float rotation = 0f;
    public float rotationSpeed = 150f;
    public float aimDirection = 0f;
    public float aimSpeed = 100f;
    public float fireForce = 4f;
    public int maxBounces = 2;
    public int maxBullets = 3;
    public float fireCooldown = 0.3f;
    public float lastFired = 0f;


    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        reader = new JSONReader();
        controls = new JSONReader.Player();
        controls = reader.GetControls(playerId);
    }

    void Update()
    {
        InputCheck();
        Move();
    }

    void InputCheck()
    {
        if ((Input.GetKey((KeyCode) controls.keyboard.rotLeft) && !Input.GetKey((KeyCode) controls.keyboard.rotRight)) || Input.GetAxis(controls.gamepad.horizontal) < -0.3f)
        {
            rotation += rotationSpeed * Time.deltaTime;
            if (rotation > 360f)
            {
                rotation -= 360f;
            }
        } else if (Input.GetKey((KeyCode) controls.keyboard.rotRight) || Input.GetAxis(controls.gamepad.horizontal) > 0.3f)
        {
            rotation -= rotationSpeed * Time.deltaTime;
            if (rotation < 0f)
            {
                rotation += 360f;
            }
        }
        if ((Input.GetKey((KeyCode) controls.keyboard.aimLeft) || Input.GetKey((KeyCode)controls.gamepad.aimLeft)) && (!(Input.GetKey((KeyCode) controls.keyboard.aimRight) && !Input.GetKey((KeyCode)controls.gamepad.aimRight))))
        {
            aimDirection += aimSpeed * Time.deltaTime;
            if (aimDirection > 360f)
            {
                aimDirection -= 360f;
            }
        } else if (Input.GetKey((KeyCode)controls.keyboard.aimRight) || Input.GetKey((KeyCode)controls.gamepad.aimRight))
        {
            aimDirection -= aimSpeed * Time.deltaTime;
            if (aimDirection < 0f)
            {
                aimDirection += 360f;
            }
        }
        if ((Input.GetKey((KeyCode)controls.keyboard.forward) || Input.GetAxis(controls.gamepad.forward) > 0.3f) && !(Input.GetKey((KeyCode)controls.keyboard.backwards) || Input.GetAxis(controls.gamepad.backwards) > 0.3f))
        {
            currentSpeed += acceleration * Time.deltaTime;

            if (currentSpeed > maxSpeed)
            {
                currentSpeed = maxSpeed;
            }
        }
        else if (Input.GetKey((KeyCode)controls.keyboard.backwards) || Input.GetAxis(controls.gamepad.backwards) > 0.3f)
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
        if (Input.GetKeyDown((KeyCode)controls.keyboard.fire) || Input.GetKey((KeyCode)controls.gamepad.fire))
        {
            Fire();
        }
    }

    void Fire()
    {
        if (Time.time >= lastFired + fireCooldown)
        {
            weapon.Fire(maxBounces, fireForce, maxBullets);
            lastFired = Time.time;
        }
    }

    void Move()
    {
        transform.Translate(new Vector3(currentSpeed, 0, 0) * Time.deltaTime);
        transform.eulerAngles = Vector3.forward * rotation;
        cannon.transform.position = transform.position + (new Vector3(Mathf.Cos(aimDirection * Mathf.Deg2Rad), Mathf.Sin(aimDirection * Mathf.Deg2Rad), 0) / 2);
        cannon.transform.eulerAngles = Vector3.forward * aimDirection;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Wall" || collision.gameObject.tag == "Tank" || collision.gameObject.tag == "Player")
        {
            currentSpeed = -(currentSpeed / 2);
            rb.velocity = Vector2.zero;

        } else if (collision.gameObject.tag == "Bullet")
        {
            //Destroy(gameObject);
            rb.velocity = Vector2.zero;
        } else if (collision.gameObject.tag == "Water")
        {
            currentSpeed = 0;
            rb.velocity = Vector2.zero;
        }
    }
}
