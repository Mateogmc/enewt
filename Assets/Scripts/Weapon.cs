using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public GameObject bulletPrefab;
    List<GameObject> bullets = new List<GameObject>();
    public Transform firePoint;

    public void Fire(int maxBounces, float fireForce, int maxBullets)
    {
        if (bullets.Count < maxBullets)
        {
            GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
            bullet.GetComponent<Bullet>().maxBounces = maxBounces;
            bullet.GetComponent<Rigidbody2D>().AddForce(firePoint.right * fireForce, ForceMode2D.Impulse);
            bullets.Add(bullet);
        } else
        {
            for (int i = 0; i < maxBullets; i++)
            {
                if (bullets[i] == null)
                {
                    GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
                    bullet.GetComponent<Bullet>().maxBounces = maxBounces;
                    bullet.GetComponent<Rigidbody2D>().AddForce(firePoint.right * fireForce, ForceMode2D.Impulse);
                    bullets[i] = bullet;
                    break;
                }
            }
        }
    }
}
