using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    [Header("Health")]
    [SerializeField] int health = 100;
    [SerializeField] int maxHealth;

    [Header("Defense")]
    [SerializeField] int defense = 30;

    [Header("Data")]
    [SerializeField] int usedTurns;
    [SerializeField] int spare;

    [SerializeField] AudioClip damageSound;
    [SerializeField] AudioClip spareSound;
    [SerializeField] AudioClip runAwaySound;

    [SerializeField] Slider spareSlider;
    [SerializeField] TMP_Text spareText;
    [SerializeField] Slider hpSlider;
    [SerializeField] TMP_Text hpText;

    BattleManager battleManager;

    DamageText damageText;

    
    private void Awake()
    {
        damageText = GetComponent<DamageText>();
        battleManager = FindObjectOfType<BattleManager>();
    }
    void Start()
    {
        if (maxHealth <= 0)
        {
            maxHealth = health;
        }
        
        SetHPToSliderAndText();
        SetSpareToSliderAndText();
    }

    void Update()
    {
        
    }
    int ApplyDefense(int _damage)
    {
        float afterDamage;
        
        afterDamage = _damage - defense;
        if ((_damage / 4f) > afterDamage)
        {
            Debug.Log(afterDamage);
            afterDamage = Mathf.FloorToInt(_damage / 4f);
        }
        return (int)afterDamage;
    }
    public void TrySpare()
    {
        if (spare >= 100)
        {
            Leave();
        }
    }
    public void TakeSpare(int _spare)
    {
        
        spare += _spare;
        string spareString = "+" + _spare + "%";
        damageText.ShowDamage(spareString, Color.yellow);
        SetSpareToSliderAndText();
    }
    public void TakeDamage(int _damage)
    {
        _damage = ApplyDefense(_damage);
        health -= _damage;
        damageText.ShowDamage(_damage.ToString(), Color.red);
        battleManager.soundPlayer.PlayOneShot(damageSound);
        if (health <= 0)
        {
            health = 0;
            RunAway();
        }
        SetHPToSliderAndText();
    }
    public void TakeDamage(int _damage, Color _color, bool playSound = true)
    {
        _damage = ApplyDefense(_damage);
        health -= _damage;
        damageText.ShowDamage(_damage.ToString(), _color);

        if (playSound)
        {
            battleManager.soundPlayer.PlayOneShot(damageSound);
        }
        
        if (health <= 0)
        {
            health = 0;
            RunAway();
        }
        SetHPToSliderAndText();
    }
    void Leave()
    {
        hpSlider.gameObject.SetActive(false);
        spareSlider.gameObject.SetActive(false);
        battleManager.soundPlayer.PlayOneShot(spareSound);
        GetComponent<Animator>().Play("Leave");

        BecomeInactive();
    }
    void Die()
    {
        //some animation
        Destroy(gameObject);
    }
    void RunAway()
    {
        hpSlider.gameObject.SetActive(false);
        spareSlider.gameObject.SetActive(false);
        battleManager.soundPlayer.PlayOneShot(runAwaySound);
        GetComponent<Animator>().Play("RunAway");

        BecomeInactive();
    }
    void BecomeInactive()
    {
        battleManager.RemoveEnemy(GetComponent<Enemy>());
        Destroy(gameObject.transform.parent.gameObject, 2);
    }
    void SetHPToSliderAndText()
    {
        //Debug.Log("HP" + health + " MHP" + maxHealth + " RATIO" + health / maxHealth);
        float ratio = (float)health / (float)maxHealth * 100;
        ratio = Mathf.FloorToInt(ratio);
        //Debug.Log("RATIO" + ratio);

        hpText.text = ratio.ToString() + "%";
        hpSlider.value = ratio;
    }
    void SetSpareToSliderAndText()
    {
        spareText.text = spare.ToString() + "%";
        spareSlider.value = spare;
    }
}
