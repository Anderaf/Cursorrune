using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeAndShrink : MonoBehaviour
{
    [HideInInspector] public float fadeSpeed = 0.02f;
    [HideInInspector] public float shrinkSpeedX = 0.02f;
    [HideInInspector] public float shrinkSpeedY = 0.02f;

    [HideInInspector] public SpriteRenderer spriteRenderer;
    private void Awake()
    {
        
    }
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        shrinkSpeedX *= transform.localScale.x;
        shrinkSpeedY *= transform.localScale.y;
    }
    void Update()
    {
        if (spriteRenderer.color.a <= 0)
        {
            Destroy(gameObject);
        }
        else
        {
            spriteRenderer.color -= new Color(0, 0, 0, fadeSpeed);
        }

        if (transform.localScale.x <= shrinkSpeedX)
        {
            transform.localScale = new Vector3(0, transform.localScale.y, transform.localScale.z);
        }
        else
        {
            transform.localScale -= shrinkSpeedX * Vector3.right;
        }

        if (transform.localScale.y <= shrinkSpeedY)
        {
            transform.localScale = new Vector3(transform.localScale.x, 0, transform.localScale.z);
        }
        else
        {
            transform.localScale -= shrinkSpeedY * Vector3.up;
        }
    }
}
