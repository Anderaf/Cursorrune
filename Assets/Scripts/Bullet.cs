using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public int damage = 40;
    public float speed = 1;
    public float lifeTime = 15;
    public Transform target;
    Transform thisTransform;
    void Start()
    {
        Destroy(gameObject, lifeTime);
        thisTransform = transform;
        if (target)
        {
            thisTransform.right = new Vector3(target.transform.position.x, target.transform.position.y,transform.position.z) - transform.position;
        }
    }

    void Update()
    {
        thisTransform.position += thisTransform.right * speed * Time.deltaTime;
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.GetComponent<SoulController>())
        {
            collision.GetComponent<SoulController>().TakeDamage(damage);
        }
    }
}
