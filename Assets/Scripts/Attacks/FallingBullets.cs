using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingBullets : AttackScript
{
    [SerializeField] GameObject bullet;
    [SerializeField] float bulletSpeed = 1;
    [SerializeField] float bulletSpawnDelay = 0.2f;
    [SerializeField] bool useAcceleration = false;
    [SerializeField] float acceleration = 0.1f;
    [SerializeField] bool spawnBulletsStrictlyAboveSoul = false;
    [SerializeField] float spawnZoneWidth = 1;
    [SerializeField] float spawnZoneYOffset = 2;

    Transform thisTransform;
    Transform soulTransform;

    [SerializeField] Transform point1;
    [SerializeField] Transform point2;
    
    float spawnCounter;
    void Start()
    {
        spawnCounter = bulletSpawnDelay;
        soulTransform = FindObjectOfType<SoulController>().transform;
        thisTransform = transform;
        thisTransform.position = soulTransform.position;
        if (spawnBulletsStrictlyAboveSoul)
        {
            point1.position = thisTransform.position + new Vector3(0, spawnZoneYOffset, 0.001f);
            point2.position = thisTransform.position + new Vector3(0, spawnZoneYOffset, 0.001f);
        }
        else
        {
            point1.position = thisTransform.position + new Vector3(-spawnZoneWidth / 2, spawnZoneYOffset, 0.001f);
            point2.position = thisTransform.position + new Vector3(spawnZoneWidth / 2, spawnZoneYOffset, 0.001f);
        }
    }

    void Update()
    {
        CountAttackTime();
        thisTransform.position = soulTransform.position;
        if (useAcceleration)
        {
            foreach (var _bullet in spawnedBullets)
            {
                _bullet.speed += acceleration;
            }
        }
        if (started)
        {
            spawnCounter -= Time.deltaTime;
            if (spawnCounter <= 0)
            {
                spawnCounter = bulletSpawnDelay;
                Spawn();
            }
        }
        
    }
    public override void StartAttack()
    {
        started = true;
        //StartCoroutine(SpawnCoroutine());
    }

    Vector3 RandomPointBetween2Points(Vector3 point1, Vector3 point2)
    {
        Vector3 point;
        point = point2 - point1;
        point *= Random.Range(0f, 1f);
        return point;
    }
    void Spawn()
    {
        var spawnedBullet = Instantiate(bullet, point1.position + RandomPointBetween2Points(point1.position, point2.position), Quaternion.identity).GetComponent<Bullet>();
        spawnedBullets.Add(spawnedBullet);
        spawnedBullet.speed = bulletSpeed;
        spawnedBullet.lifeTime = attackTime + 1;
        spawnedBullet.transform.right = Vector3.down;
    }
    void Spawn(int count)
    {
        for (int i = 0; i < count; i++)
        {
            Spawn();
        }
    }
    IEnumerator SpawnCoroutine()
    {
        yield return new WaitForSeconds(8);
        Spawn(10);
    }
}
