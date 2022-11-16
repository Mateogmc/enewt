using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fader : MonoBehaviour
{
    SpriteRenderer sr;

    public bool unFading;
    bool flag = true;
    float alpha = 0f;

    private void Start()
    {
        sr = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        if (unFading && flag)
        {
            alpha = 1f;
            flag = false;
        } else if (unFading)
        {
            alpha -= 1f * Time.deltaTime;
        } else
        {
            alpha += 1f * Time.deltaTime;
        }
        sr.color = new Color(0f, 0f, 0f, alpha);
        if (alpha < 0f)
        {
            Destroy(gameObject);
        }
    }
}
