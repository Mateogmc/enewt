using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    JSONReader reader;
    public Weapon weapon;
    public GameObject cannon;
    JSONReader.Player controls;
    Rigidbody2D rb;
    public int playerId;
    public LayerMask ignoreLayer;

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
        if (!Options.paused)
        {
            InputCheck();
            Move();
        }
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
        } else if (Input.GetKey((KeyCode) controls.keyboard.rotRight) || Input.GetAxis(controls.gamepad.horizontal) > 0.5f)
        {
            rotation -= rotationSpeed * Time.deltaTime;
            if (rotation < 0f)
            {
                rotation += 360f;
            }
        }
        if ((Input.GetKey((KeyCode) controls.keyboard.aimLeft) || Input.GetAxis(controls.gamepad.aim) < -0.5f) && !(Input.GetKey((KeyCode) controls.keyboard.aimRight)))
        {
            aimDirection += aimSpeed * Time.deltaTime;
            if (aimDirection > 360f)
            {
                aimDirection -= 360f;
            }
        } else if (Input.GetKey((KeyCode)controls.keyboard.aimRight) || Input.GetAxis(controls.gamepad.aim) > 0.5f)
        {
            aimDirection -= aimSpeed * Time.deltaTime;
            if (aimDirection < 0f)
            {
                aimDirection += 360f;
            }
        }
        if ((Input.GetKey((KeyCode)controls.keyboard.forward) || Input.GetAxis(controls.gamepad.forward) > 0.5f) && !(Input.GetKey((KeyCode)controls.keyboard.backwards) || Input.GetAxis(controls.gamepad.backwards) > 0.3f))
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
        RaycastHit2D hit = Physics2D.Raycast(new Vector2(transform.position.x, transform.position.y), new Vector2(Mathf.Cos(aimDirection * Mathf.Deg2Rad), Mathf.Sin(aimDirection * Mathf.Deg2Rad)), 0.5f, ~ignoreLayer);
        Debug.DrawRay(new Vector2(transform.position.x, transform.position.y), new Vector2(Mathf.Cos(aimDirection * Mathf.Deg2Rad), Mathf.Sin(aimDirection * Mathf.Deg2Rad)), Color.red, 0.5f);
        if (Time.time >= lastFired + fireCooldown)
        {
            if (hit.collider == null)
            {
                weapon.Fire(maxBounces, fireForce, maxBullets);
                lastFired = Time.time;
            }
            else if (hit.collider.tag != "Wall")
            {
                weapon.Fire(maxBounces, fireForce, maxBullets);
                lastFired = Time.time;
            }
        }
    }

    void Move()
    {
        transform.Translate(new Vector3(currentSpeed, 0, 0) * Time.deltaTime);
        transform.eulerAngles = Vector3.forward * rotation;
        cannon.transform.position = transform.position + 2 * (new Vector3(Mathf.Cos(aimDirection * Mathf.Deg2Rad), Mathf.Sin(aimDirection * Mathf.Deg2Rad), 0) / 3);
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
        } else if (collision.gameObject.tag == "Void")
        {
            currentSpeed = 0;
            rb.velocity = Vector2.zero;
        }
    }
}
