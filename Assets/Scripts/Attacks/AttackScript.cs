using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AttackScript : MonoBehaviour
{
    public float attackTime = 8;
    public List<Bullet> spawnedBullets;

    int damage = 40;
    int difficulty = 1;
    public bool started;


    public void CountAttackTime()
    {
        if (started)
        {
            attackTime -= Time.deltaTime;
            if (attackTime <= 0)
            {
                StopAttack();
            }
        }
    }
    public void SetDifficulty(int _difficulty)
    {
        difficulty = Mathf.Clamp(_difficulty, 1, 3);
    }
    public void SetDamage(int _damage)
    {
        damage = _damage;
    }
    public abstract void StartAttack();
    public void DestroyBullets()
    {
        foreach (var bullet in spawnedBullets)
        {
            Destroy(bullet.gameObject);
        }
    }
    public void StopAttack()
    {
        DestroyBullets();
        Destroy(gameObject);
        started = false;
        FindObjectOfType<BattleManager>().CheckAttacks();
    }
}
