using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public Weapon weapon;
    public GameObject cannon;
    Rigidbody2D rb;

    public float currentSpeed = 0f;
    public float acceleration = 4f;
    public float maxSpeed = 2f;
    public float drag = 3f;
    public float rotation = 0f;
    public float rotationSpeed = 150f;
    float aimDirection = 0f;
    public float aimSpeed = 100f;
    public float fireForce = 4f;
    public int maxBounces = 2;
    public int maxBullets = 3;
    public float fireCooldown = 0.3f;
    public float lastFired = 0f;


    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        InputCheck();
        Move();
    }

    void InputCheck()
    {
        if ((Input.GetKey(KeyCode.LeftArrow) && !Input.GetKey(KeyCode.RightArrow)) || Input.GetAxis("Horizontal") > -0.1f)
        {
            rotation += rotationSpeed * Time.deltaTime;
            if (rotation > 360f)
            {
                rotation -= 360f;
            }
        } else if (Input.GetKey(KeyCode.RightArrow) || Input.GetAxis("Horizontal") < 0.1f)
        {
            rotation -= rotationSpeed * Time.deltaTime;
            if (rotation < 0f)
            {
                rotation += 360f;
            }
        }
        if ((Input.GetKey(KeyCode.Z) || Input.GetKey(KeyCode.Joystick1Button10)) && !(Input.GetKey(KeyCode.X) || Input.GetKey(KeyCode.Joystick1Button10)))
        {
            aimDirection += aimSpeed * Time.deltaTime;
            if (aimDirection > 360f)
            {
                aimDirection -= 360f;
            }
        } else if (Input.GetKey(KeyCode.X) || Input.GetKey(KeyCode.Joystick1Button10))
        {
            aimDirection -= aimSpeed * Time.deltaTime;
            if (aimDirection < 0f)
            {
                aimDirection += 360f;
            }
        }
        if ((Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.Joystick1Button1)) && !Input.GetKey(KeyCode.DownArrow))
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
        if (Input.GetKeyDown(KeyCode.Space) || Input.GetKey(KeyCode.Joystick1Button6))
        {
            Fire();
        }
        if (Input.GetKey(KeyCode.Joystick1Button0))
        {
            Debug.Log(0);
        }
        if (Input.GetKey(KeyCode.Joystick1Button1))
        {
            Debug.Log(1);
        }
        if (Input.GetKey(KeyCode.Joystick1Button2))
        {
            Debug.Log(2);
        }
        if (Input.GetKey(KeyCode.Joystick1Button3))
        {
            Debug.Log(3);
        }
        if (Input.GetKey(KeyCode.Joystick1Button4))
        {
            Debug.Log(4);
        }
        if (Input.GetKey(KeyCode.Joystick1Button5))
        {
            Debug.Log(5);
        }
        if (Input.GetKey(KeyCode.Joystick1Button6))
        {
            Debug.Log(6);
        }
        if (Input.GetKey(KeyCode.Joystick1Button7))
        {
            Debug.Log(7);
        }
        if (Input.GetKey(KeyCode.Joystick1Button8))
        {
            Debug.Log(8);
        }
        if (Input.GetKey(KeyCode.Joystick1Button9))
        {
            Debug.Log(9);
        }
        if (Input.GetKey(KeyCode.Joystick1Button10))
        {
            Debug.Log(10);
        }
        if (Input.GetKey(KeyCode.Joystick1Button11))
        {
            Debug.Log(11);
        }
        if (Input.GetKey(KeyCode.Joystick1Button12))
        {
            Debug.Log(12);
        }
        if (Input.GetKey(KeyCode.Joystick1Button13))
        {
            Debug.Log(13);
        }
        if (Input.GetKey(KeyCode.Joystick1Button14))
        {
            Debug.Log(14);
        }
        if (Input.GetKey(KeyCode.Joystick1Button15))
        {
            Debug.Log(15);
        }
        if (Input.GetKey(KeyCode.Joystick1Button16))
        {
            Debug.Log(16);
        }
        if (Input.GetKey(KeyCode.Joystick1Button17))
        {
            Debug.Log(17);
        }
        if (Input.GetKey(KeyCode.Joystick1Button18))
        {
            Debug.Log(18);
        }
        if (Input.GetKey(KeyCode.Joystick1Button19))
        {
            Debug.Log(19);
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
            Destroy(gameObject);
            //rb.velocity = Vector2.zero;
        }
    }
}
