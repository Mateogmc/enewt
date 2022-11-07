 using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    Rigidbody2D rb;
    Vector2 lastVelocity;
    public LayerMask water;

    public int maxBounces;
    int currentBounces = 0;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        Physics2D.IgnoreLayerCollision(4, 7, true);
    }

    private void Update()
    {
        lastVelocity = rb.velocity;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Water")
        {
            return;
        }
        else if (collision.gameObject.tag != "Wall" || currentBounces >= maxBounces)
        {
            Destroy(gameObject);
        }  
        else if (collision.gameObject.tag == "Wall")
        {
            float x = collision.contacts[0].normal.x;
            float y = collision.contacts[0].normal.y;
            Vector2 newVelocity = new Vector2(x != 0 ? -lastVelocity.x : lastVelocity.x , y != 0 ? -lastVelocity.y : lastVelocity.y);

            rb.velocity = newVelocity;
            currentBounces++;
        }
    }
}
