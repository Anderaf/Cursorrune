using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ActScript : MonoBehaviour
{
    [HideInInspector] public List<Enemy> enemies;
    [HideInInspector] public CharacterManager[] characters;
    [HideInInspector] public CharacterManager actingCharacter;
    [HideInInspector] public int targetID;
    float timeBeforeDestroy = 10;
    abstract public void StartAct();
    void Start()
    {
        Destroy(gameObject, timeBeforeDestroy);
    }
}
