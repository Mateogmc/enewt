using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class V1AI: Turret
{
    private void Update()
    {
        if (!Options.paused)
        {
            if (player != null)
            {
                CheckDirection();
                Move();
                CheckFire();
            }
            else
            {
                player = GameObject.FindGameObjectWithTag("Player");
            }
        }
    }

    void CheckFire()
    {
        RaycastHit2D hit = Physics2D.Raycast(cannon.transform.position, new Vector2(Mathf.Cos((clockwise ? aimDirection + 2 : aimDirection - 2) * Mathf.Deg2Rad), Mathf.Sin((clockwise ? aimDirection + 2 : aimDirection - 2) * Mathf.Deg2Rad)), 100f, ~ignoreLayer);
        if (hit.collider.tag == "Player")
        {
            Fire();
        }
    }
}
