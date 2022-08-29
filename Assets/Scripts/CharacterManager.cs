using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class CharacterManager : MonoBehaviour
{
    public UnityEvent onTurnUsed;

    public GameObject hitEffect;
    public AudioClip swingSound;
    [SerializeField] float swingSoundPitch = 1;
    [SerializeField] Slider linkedSlider;
    [SerializeField] AudioSource soundPlayer;
    Animator characterAnimator;
    BattleManager battleManager;

    [SerializeField] ActObject[] acts;

    [SerializeField] int damage = 80;
    [SerializeField] int health  = 100;
    [SerializeField] Color enemyDamageTextColor = Color.white;
    [SerializeField] int defensePercent = 3;

    [SerializeField] Transform effectPositionTransform;

    int maxHealth;
    int initialDefence;

    bool isTurnUsed = false;
    [SerializeField] int id = 999;
    public string characterName = "Chara";
    // Start is called before the first frame update
    void Start()
    {
        if (maxHealth == 0)
        {
            maxHealth = health;
        }
        linkedSlider.maxValue = maxHealth;
        linkedSlider.value = health;
        battleManager = FindObjectOfType<BattleManager>();
        characterAnimator = GetComponent<Animator>();
        initialDefence = defensePercent;

        //TakeDamage(60);
    }
    public ActObject GetAct(int id)
    {
        if (id < acts.Length)
        {
            return acts[id];
        }
        else
        {
            return null;
        }
    }

    void Update()
    {
        
    }
    public void TakeDamage(int _damage)
    {
        health -= ApplyDefense(_damage);

        if (health < 0)
        {
            linkedSlider.value = 0;
        }
        else
        {
            linkedSlider.value = health;
        }

        characterAnimator.SetTrigger("Hurt");
        if (health <= 0)
        {
            characterAnimator.SetBool("Fallen", true);
        }
        /*else
        {
            characterAnimator.SetBool("Fallen", false);
        }*/
    }
    int ApplyDefense(int _damage)
    {
        float afterDamage;

        afterDamage = _damage - (_damage * defensePercent) / 100;
        if ((_damage / 4f) > afterDamage)
        {
            Debug.Log(afterDamage);
            afterDamage = Mathf.FloorToInt(_damage / 4f);
        }
        return (int)afterDamage;
    }
    public void Idle()
    {
        PreItemExit();
        PreActExit();
        characterAnimator.Play("Idle");
    }
    public void ResetBools()
    {
        PreItemExit();
        PreActExit();
    }
    public int GetId() 
    {
        return id;
    }
    public void UseItem(ItemObject item)
    {
        Item();
        Debug.Log(item.GetTargetId() + " consumed " + item.name + " that " + item.description);
        battleManager.GetCharacter(item.GetTargetId()).ConsumeItem(item);
        PreItemExit();
        PreActExit();
    }
    public void ConsumeItem(ItemObject item)
    {
        if (item.individualHealingValues)
        {
            if (item.groupHeal)
            {
                for (int i = 0; i < battleManager.GetCharacters().Length; i++)
                {
                    CharacterManager character = battleManager.GetCharacter(i);
                    switch (character.name)
                    {
                        case "Kris":
                            character.Heal(item.krisHealValue);
                            break;
                        case "Susie":
                            character.Heal(item.susieHealValue);
                            break;
                        case "Ralsei":
                            character.Heal(item.ralseiHealValue);
                            break;
                        case "Noelle":
                            character.Heal(item.noelleHealValue);
                            break;
                        default:
                            character.Heal(item.healValue);
                            break;
                    }
                }
            }
            else
            {
                switch (name)
                {
                    case "Kris":
                        Heal(item.krisHealValue);
                        break;
                    case "Susie":
                        Heal(item.susieHealValue);
                        break;
                    case "Ralsei":
                        Heal(item.ralseiHealValue);
                        break;
                    case "Noelle":
                        Heal(item.noelleHealValue);
                        break;
                    default:
                        Heal(item.healValue);
                        break;
                }
            }            
        }
        else
        {
            if (item.groupHeal)
            {
                foreach (CharacterManager character in battleManager.GetCharacters())
                {
                    character.Heal(item);
                }
            }
            else
            {
                Heal(item);
            }
        }
    }
    public void Heal(int healValue)
    {
        health += healValue;
        if (health > maxHealth)
        {
            health = maxHealth;
        }
        linkedSlider.value = health;
        battleManager.PlayHealSound();
        if (effectPositionTransform)
        {
            var fx = Instantiate(battleManager.healFX, effectPositionTransform.position + new Vector3(0.5f, 0.5f, -0.5f), Quaternion.Euler(-90, 0, 0)).gameObject;
            Destroy(fx, 1);
        }
        else
        {
            var fx = Instantiate(battleManager.healFX, transform.position + new Vector3(0.5f, 0.5f, -0.5f), Quaternion.Euler(-90, 0, 0)).gameObject;
            Destroy(fx, 1);
        }
    }
    public void GainTP(ItemObject item)
    {
        if (item.randomTP)
        {
            battleManager.AddTP(Random.Range(item.minTP, item.maxTP));
        }
        else
        {
            battleManager.AddTP(item.TPValue);
        }
    }
    public void Heal(ItemObject item)
    {
        if (item.isForRevival && health <= 0)
        {
            health = 0;
            health += item.healAfterRevival;
        }
        else if (item.randomHeal)
        {
            health += Random.Range(item.minHeal,item.maxHeal);
        }
        else
        {
            health += item.healValue;
        }
        if (health > maxHealth)
        {
            health = maxHealth;
        }
        linkedSlider.value = health;
        battleManager.PlayHealSound();
        if (effectPositionTransform)
        {
            var fx = Instantiate(battleManager.healFX, effectPositionTransform.position + new Vector3(0.5f, 0.5f, -0.5f), Quaternion.Euler(-90, 0, 0)).gameObject;
            Destroy(fx, 1);
        }
        else
        {
            var fx = Instantiate(battleManager.healFX, transform.position + new Vector3(0.5f, 0.5f, -0.5f), Quaternion.Euler(-90, 0, 0)).gameObject;
            Destroy(fx, 1);
        }
        
    }
    public void UseAct(ActObject act)
    {      
        Debug.Log(characterName + " used " + act.name + " on entity with id " + act.GetTargetId() + ". That act " + act.description);
        if (act.TPCost <= battleManager.TPValue)
        {
            battleManager.DrainTP(act.TPCost);
            if (!act.useCustomScript)
            {
                Act();
                if (act.groupSpare)
                {
                    for (int i = 0; i < battleManager.GetEnemies().Count; i++)
                    {
                        battleManager.GetEnemy(i).TakeSpare(act.spareValue);
                    }
                }
                else
                {
                    battleManager.GetEnemy(act.GetTargetId()).TakeSpare(act.spareValue);
                }
            }
            else
            {
                var actScript = Instantiate(act.customScript);
                actScript.enemies = battleManager.GetEnemies();
                actScript.characters = battleManager.GetCharacters();
                actScript.actingCharacter = GetComponent<CharacterManager>();
                actScript.targetID = act.GetTargetId();
                actScript.StartAct();
            }
        }
        
        

        PreItemExit();
        PreActExit();
    }
    public void PreSlash()
    {
        characterAnimator.SetTrigger("PreSlash");
    }
    /*public void Slash()
    {
        if (!isTurnUsed)
        {
            isTurnUsed = true;
            //onTurnUsed.Invoke();
            StartCoroutine(SlashCoroutine());
            PreItemExit();
            PreActExit();
        }      
    }*/
    public void Slash(int id)
    {
        //Slash enemy by id
        Debug.Log("Slashed " + id);
        if (!isTurnUsed)
        {
            isTurnUsed = true;
            characterAnimator.SetTrigger("Slash");
            var enemy = battleManager.GetEnemy(id);
            enemy.TakeDamage(damage,enemyDamageTextColor);

            Destroy(Instantiate(hitEffect, enemy.transform.position, Quaternion.identity), 4);
            battleManager.PlayPitchedSound(swingSound, swingSoundPitch);
            battleManager.AddTP(5);

            PreItemExit();
            PreActExit();
        }
    }
    public void Spare(int id)
    {
        Debug.Log("Spared " + id);
        if (!isTurnUsed)
        {
            Act();
            isTurnUsed = true;
            var enemy = battleManager.GetEnemy(id);
            enemy.TrySpare();
            PreItemExit();
            PreActExit();
        }
    }
    public Color GetColor()
    {
        return enemyDamageTextColor;
    }
    public void PreActEnter()
    {
        characterAnimator.SetBool("PreAct", true);
    }
    public void PreActExit()
    {
        characterAnimator.SetBool("PreAct", false);
    }
    public void Act()
    {
        characterAnimator.SetTrigger("Act");
    }
    public void PreItemEnter()
    {
        characterAnimator.SetBool("PreItem", true);
    }
    public void PreItemExit()
    {
        characterAnimator.SetBool("PreItem", false);
    }
    public void Item()
    {
        characterAnimator.SetTrigger("Item");
    }
    public void DefendEnter()
    {
        defensePercent = initialDefence + 30;
        battleManager.AddTP(15);
        PreActExit();
        PreItemExit();
        characterAnimator.SetBool("DefendIdle", true);
    }
    public void DefendExit()
    {
        defensePercent = initialDefence;
        characterAnimator.SetBool("DefendIdle", false);
    }
    /*public void TakeDamage()
    {
        
    }*/
    public bool CheckIfFallen()
    {
        return characterAnimator.GetBool("Fallen");
    }
    /*IEnumerator SlashCoroutine()
    {
        PreSlash();
        yield return new WaitForSeconds(1);
        characterAnimator.SetTrigger("Slash");
        var enemyHealth = FindObjectOfType<Enemy>();
        yield return new WaitForSeconds(0.2f);
        enemyHealth.TakeDamage(damage);
    }
    IEnumerator SlashCoroutine(int id)
    {
        PreSlash();
        yield return new WaitForSeconds(1);
        characterAnimator.SetTrigger("Slash");
        var enemyHealth = battleManager.GetEnemy(id);
        yield return new WaitForSeconds(0.2f);
        enemyHealth.TakeDamage(damage);
    }*/
}
