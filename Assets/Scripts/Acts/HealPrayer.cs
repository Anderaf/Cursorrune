using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealPrayer : ActScript
{
    [SerializeField] int healValue = 65;
    public override void StartAct()
    {
        actingCharacter.GetComponent<Animator>().Play("Heal Prayer");
        Invoke("HealCharacter", 0.7f);
    }
    void HealCharacter()
    {
        characters[targetID].Heal(healValue);
    }
}
