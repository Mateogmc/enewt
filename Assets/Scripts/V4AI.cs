using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class V4AI : Turret
{
    public GameObject predictedTarget;
    public Vector3 targetPoint;

    private void Update()
    {
        if (!Options.paused)
        {
            if (player != null)
            {
                CheckDirection();
                PredictPosition();
                Move();
                CheckFire(cannon.transform.position, new Vector2(Mathf.Cos(aimDirection * Mathf.Deg2Rad), Mathf.Sin(aimDirection * Mathf.Deg2Rad)), 0);
            }
            else
            {
                player = GameObject.FindGameObjectWithTag("Player");
            }
        }
    }

    void PredictPosition()
    {
        float rotation = player.GetComponent<PlayerController>().rotation;
        targetPoint = player.transform.position + (new Vector3(Mathf.Cos(rotation * Mathf.Deg2Rad), Mathf.Sin(rotation * Mathf.Deg2Rad), 0) * player.GetComponent<PlayerController>().currentSpeed);
    }

    void CheckFire(Vector2 pos, Vector2 angle, int currentBounces)
    {
        //Debug.DrawRay(pos, angle * 5, Color.red);
        RaycastHit2D hit = Physics2D.Raycast(pos, angle, 100f, ~ignoreLayer);
        GameObject target = Instantiate(predictedTarget, targetPoint, Quaternion.identity);
        if (hit == true)
        {
            if (hit.collider.tag == "Player" || (hit.collider.tag == "Prediction" && currentBounces > 0))
            {
                Fire();
            }
            else if (hit.collider.tag == "Wall")
            {
                if (currentBounces < maxBounces)
                {
                    CheckFire(hit.point, Vector2.Reflect(angle, hit.normal), ++currentBounces);
                }
            }
        }
        Destroy(target);
    }
}
