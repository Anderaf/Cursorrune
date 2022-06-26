using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharcterState : MonoBehaviour
{
    [SerializeField] int health = 100;

    int maxHealth;

    Animator animator;
    void Start()
    {
        animator = GetComponent<Animator>();

        maxHealth = health;
    }

    void Update()
    {
        
    }
    
}
