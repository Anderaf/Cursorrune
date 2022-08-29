using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObject/Act or Magic")]
public class ActObject : ScriptableObject
{
    [Header("Main")]
    public string description = "Default description";
    public string useText = "You acted";
    public bool useOnEnemies;

    [Header("Spare")]
    public int spareValue = 10;

    public bool groupSpare;

    /*public bool randomSpare;
    public int minSpare = 1;
    public int maxSpare = 20;*/

    /*[Header("Healing")]
    public int healValue = 0;

    public bool groupHeal;

    public bool randomHeal;
    public int minHeal = 10;
    public int maxHeal = 80;

    public bool isForRevival;
    public int healAfterRevival = 50;*/

    [Header("TP")]
    public int TPCost;

    /*[Header("Animation")]
    Animation animation;*/

    //[Header("Sound")]
    //public bool playOnStart = true;
    //public AudioClip sfx;

    [Header("Custom")]
    public bool useCustomScript;
    public ActScript customScript;
    //public GameObject[] customObjects;
    //public AudioClip[] customSounds;

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
