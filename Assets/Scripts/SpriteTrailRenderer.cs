using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteTrailRenderer : MonoBehaviour
{
    [SerializeField] GameObject fader;
    public float delay = 0.1f;

    [Header("Fader Settings")]
    public float fadeSpeed = 0.02f;
    public float shrinkSpeedX = 0.02f;
    public float shrinkSpeedY = 0.02f;

    float timeCounter;
    SpriteRenderer spriteRenderer;
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        timeCounter += Time.deltaTime;
        if (timeCounter >= delay)
        {
            timeCounter = 0;
            var _fader = Instantiate(fader, transform.position + Vector3.forward * 0.001f, Quaternion.identity).GetComponent<FadeAndShrink>();
            var _faderSpriteRenderer = _fader.GetComponent<SpriteRenderer>();
            _fader.transform.localScale = transform.localScale;
            _faderSpriteRenderer.sprite = spriteRenderer.sprite;
            _faderSpriteRenderer.color = spriteRenderer.color;
            _fader.fadeSpeed = fadeSpeed;
            _fader.shrinkSpeedX = shrinkSpeedX;
            _fader.shrinkSpeedY = shrinkSpeedY;
        }
    }
}
