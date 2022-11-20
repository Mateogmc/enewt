 using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    Rigidbody2D rb;
    Vector2 lastVelocity;
    float magnitude;
    public LayerMask hole;

    public int maxBounces;
    int currentBounces = 0;

    SoundManager soundManager;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        Physics2D.IgnoreLayerCollision(8, 7, true);
        Physics2D.IgnoreLayerCollision(4, 7, true);
        magnitude = rb.velocity.magnitude;
        soundManager = GameObject.FindGameObjectWithTag("Sound").GetComponent<SoundManager>();
    }

    private void Update()
    {
        if (!Options.paused)
        {
            rb.velocity = rb.velocity.normalized * magnitude;
            lastVelocity = rb.velocity;
            rb.rotation = Mathf.Atan2(rb.velocity.y, rb.velocity.x) * Mathf.Rad2Deg;
            if (rb.velocity == Vector2.zero || transform.position.x < -5 || transform.position.x > 5 || transform.position.y < -4.5 || transform.position.y > 5.5)
            {
                Destroy(gameObject);
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Void")
        {
            return;
        }
        else if (collision.gameObject.tag != "Wall" || currentBounces >= maxBounces)
        {
            Destroy(gameObject);
            soundManager.PlaySound(soundManager.bulletBreak);
        }  
        else if (collision.gameObject.tag == "Wall")
        {
            float x = collision.contacts[0].normal.x;
            float y = collision.contacts[0].normal.y;
            Vector2 newVelocity = new Vector2(x != 0 ? Mathf.Abs(lastVelocity.x) * x : lastVelocity.x , y != 0 ? Mathf.Abs(lastVelocity.y) * y : lastVelocity.y);

            rb.velocity = newVelocity;
            currentBounces++;
            soundManager.PlaySound(soundManager.bulletRebound);
        }
    }
}
