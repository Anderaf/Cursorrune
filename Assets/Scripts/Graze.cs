using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Graze : MonoBehaviour
{
    [SerializeField] float fadeSpeed = 0.01f;

    List<Bullet> grazedBullets = new List<Bullet>();
    SpriteRenderer spriteRenderer;
    BattleManager battleManager;

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        battleManager = FindObjectOfType<BattleManager>();
    }
    private void Update()
    {
        if (spriteRenderer.color.a > 0)
        {
            spriteRenderer.color = new Color(spriteRenderer.color.r, spriteRenderer.color.g, spriteRenderer.color.b, spriteRenderer.color.a - fadeSpeed);
        }
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        Bullet _bullet = collision.GetComponent<Bullet>();
        if (collision.GetComponent<Bullet>() && !grazedBullets.Contains(_bullet))
        {
            grazedBullets.Add(_bullet);
            StartCoroutine(GrazeDelayCoroutine(_bullet));
            GrazeBullet();
        }
    }
    void GrazeBullet()
    {
        spriteRenderer.color = new Color(spriteRenderer.color.r, spriteRenderer.color.g, spriteRenderer.color.b, 1);
        battleManager.AddTP(0.5f);
    }
    IEnumerator GrazeDelayCoroutine(Bullet _bullet, float _delay = 1f)
    {
        yield return new WaitForSeconds(_delay);
        grazedBullets.Remove(_bullet);
    }
}
