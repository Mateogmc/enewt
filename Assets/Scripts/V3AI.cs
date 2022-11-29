using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;
using System.Threading.Tasks;

public class V3AI : MovingEnemy
{
    void Update()
    {
        if (!Options.paused)
        {
            if (player != null)
            {
                if (path == null)
                {
                    return;
                }

                if (currentWaypoint >= path.vectorPath.Count)
                {
                    reachedEndOfPath = true;
                    return;
                }
                else
                {
                    reachedEndOfPath = false;
                }
                Aim();
                Move();
                CheckFire();
            }
            else
            {
                player = GameObject.FindGameObjectWithTag("Player");
            }
        }
    }

    async void CheckFire()
    {
        RaycastHit2D hit = Physics2D.Raycast(cannon.transform.position, new Vector2(Mathf.Cos(aimDirection * Mathf.Deg2Rad), Mathf.Sin(aimDirection * Mathf.Deg2Rad)), 100f, ~ignoreLayer);
        if (hit.collider.tag == "Player")
        {
            await Task.Delay(fireDelay);
            if (Time.time >= lastFired + fireCooldown)
            {
                if (gameObj != null)
                {
                    hit = Physics2D.Raycast(cannon.transform.position, new Vector2(Mathf.Cos(aimDirection * Mathf.Deg2Rad), Mathf.Sin(aimDirection * Mathf.Deg2Rad)), 100f, ~ignoreLayer);
                    if (hit.collider.tag == "Player")
                    {
                        for (int i = 0; i < maxBullets; i++)
                        {
                            hit = Physics2D.Raycast(cannon.transform.position, new Vector2(Mathf.Cos(aimDirection * Mathf.Deg2Rad), Mathf.Sin(aimDirection * Mathf.Deg2Rad)), 100f, ~ignoreLayer);
                            if (gameObj != null && player != null && hit.collider.tag == "Player")
                            {
                                Fire(true);
                                await Task.Delay(200);
                            }
                        }
                    }
                }
            }
        }
    }
}
