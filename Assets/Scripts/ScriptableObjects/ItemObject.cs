using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObject/Consumable")]
public class ItemObject : ScriptableObject
{
    [Header("Description")]
    public string description = "Default description";
    [Header("Healing")]
    public int healValue = 50;

    public bool individualHealingValues;
    public int krisHealValue = 50;
    public int susieHealValue = 50;
    public int ralseiHealValue = 50;
    public int noelleHealValue = 50;

    public bool groupHeal;

    public bool randomHeal;
    public int minHeal = 10;
    public int maxHeal = 80;

    public bool isForRevival;
    public int healAfterRevival = 50;

    [Header("TP")]
    public int TPValue;

    public bool randomTP;
    public int minTP = 10;
    public int maxTP = 60;
    [Header("Other")]

    int targetId;
    public void SetTargetId(int id)
    {
        targetId = id;
    }
    public int GetTargetId()
    {
        return targetId;
    }
}
