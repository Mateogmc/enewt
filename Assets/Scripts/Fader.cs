using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fader : MonoBehaviour
{
    SpriteRenderer sr;

    public bool fading;
    bool flag = true;
    float alpha = 1f;

    private void Start()
    {
        sr = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        if (fading && flag)
        {
            alpha = 0f;
            flag = false;
        } else if (fading)
        {
            alpha += 1f * Time.deltaTime;
        } else
        {
            alpha -= 1f * Time.deltaTime;
        }
        sr.color = new Color(0f, 0f, 0f, alpha);
    }
}
