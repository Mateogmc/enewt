using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour
{
    public Weapon weapon;
    public GameObject cannon;
    public LayerMask ignoreLayer;

    public float aimDirection;
    public float aimSpeed;
    public float fireForce;
    public int maxBounces;
    public int maxBullets;
    public float fireCooldown;
    protected float lastFired;

    protected void Start()
    {
        lastFired = Time.time - fireCooldown;
    }

    protected void Fire()
    {
        RaycastHit2D hit = Physics2D.Raycast(new Vector2(transform.position.x, transform.position.y), new Vector2(Mathf.Cos(aimDirection * Mathf.Deg2Rad), Mathf.Sin(aimDirection * Mathf.Deg2Rad)), 0.5f, ~ignoreLayer);
        if (Time.time >= lastFired + fireCooldown)
        {
            if (hit.collider == null || hit.collider.tag != "Wall")
            {
                weapon.Fire(maxBounces, fireForce, maxBullets);
                lastFired = Time.time;
            }
        }
    }

    protected void Fire(bool noCooldown)
    {
        RaycastHit2D hit = Physics2D.Raycast(new Vector2(transform.position.x, transform.position.y), new Vector2(Mathf.Cos(aimDirection * Mathf.Deg2Rad), Mathf.Sin(aimDirection * Mathf.Deg2Rad)), 0.5f, ~ignoreLayer);
        if (noCooldown || (Time.time >= lastFired + fireCooldown))
        {
            if (hit.collider == null || hit.collider.tag != "Wall")
            {
                weapon.Fire(maxBounces, fireForce, maxBullets);
                lastFired = Time.time;
            }
        }
    }

    protected void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Bullet")
        {
            Destroy(gameObject);
        }
    }
}
