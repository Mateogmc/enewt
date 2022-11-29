using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : Unit
{
    public GameObject player = null;

    public float amplitude;
    protected float minAngle;
    protected float maxAngle;
    protected float oldAngle = 0f;
    protected float currentAngle;
    protected bool clockwise;

    protected void CheckDirection()
    {
        currentAngle = Mathf.Atan2(player.transform.position.y - transform.position.y, player.transform.position.x - transform.position.x) * Mathf.Rad2Deg;
        minAngle = currentAngle - amplitude;
        maxAngle = currentAngle + amplitude;

        /*Debug.DrawRay(transform.position, new Vector3(Mathf.Cos(aimDirection * Mathf.Deg2Rad), Mathf.Sin(aimDirection * Mathf.Deg2Rad), 0) * 6, Color.black);
        Debug.DrawRay(transform.position, new Vector3(Mathf.Cos(currentAngle * Mathf.Deg2Rad), Mathf.Sin(currentAngle * Mathf.Deg2Rad), 0) * 6, Color.green);
        Debug.DrawRay(transform.position, new Vector3(Mathf.Cos(minAngle * Mathf.Deg2Rad), Mathf.Sin(minAngle * Mathf.Deg2Rad), 0) * 6, Color.magenta);
        Debug.DrawRay(transform.position, new Vector3(Mathf.Cos(maxAngle * Mathf.Deg2Rad), Mathf.Sin(maxAngle * Mathf.Deg2Rad), 0) * 6, Color.yellow);*/

    }

    protected void Move()
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
        cannon.transform.position = transform.position + 2 * (new Vector3(Mathf.Cos(aimDirection * Mathf.Deg2Rad), Mathf.Sin(aimDirection * Mathf.Deg2Rad), 0) / 3);
        cannon.transform.eulerAngles = Vector3.forward * aimDirection;
    }
}
