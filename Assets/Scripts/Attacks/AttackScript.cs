using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AttackScript : MonoBehaviour
{
    int difficulty = 3;
    public void SetDifficulty(int _difficulty)
    {
        difficulty = _difficulty;
    }
    public abstract void StartAttack();
}
