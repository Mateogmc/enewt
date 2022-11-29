using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : Tank
{
    JSONReader reader;
    JSONReader.Player controls;
    public int playerId;


    void Start()
    {
        reader = new JSONReader();
        controls = new JSONReader.Player();
        controls = reader.GetControls(playerId);
        base.Start();
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

            if (currentSpeed > currentMaxSpeed)
            {
                currentSpeed = currentMaxSpeed;
            }
        }
        else if (Input.GetKey((KeyCode)controls.keyboard.backwards) || Input.GetAxis(controls.gamepad.backwards) > 0.3f)
        {
            currentSpeed -= acceleration * Time.deltaTime;

            if (currentSpeed < -currentMaxSpeed)
            {
                currentSpeed = -currentMaxSpeed;
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
            CheckFire();
        }
    }

    void CheckFire()
    {
        RaycastHit2D hit = Physics2D.Raycast(new Vector2(transform.position.x, transform.position.y), new Vector2(Mathf.Cos(aimDirection * Mathf.Deg2Rad), Mathf.Sin(aimDirection * Mathf.Deg2Rad)), 0.5f, ~ignoreLayer);
        Debug.DrawRay(new Vector2(transform.position.x, transform.position.y), new Vector2(Mathf.Cos(aimDirection * Mathf.Deg2Rad), Mathf.Sin(aimDirection * Mathf.Deg2Rad)), Color.red, 0.5f);
        if (hit.collider == null || hit.collider.tag != "Wall")
        {
            Fire();
        }
    }
}
