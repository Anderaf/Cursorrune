using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BattleManager : MonoBehaviour
{
    public float tpValue { get; private set; }

    [SerializeField] ChoiceMenu choiceMenu;
    [SerializeField] CharacterMenu[] menus;
    [SerializeField] List<Enemy> enemies;

    [SerializeField] Slider tpSlider;
    [SerializeField] TMP_Text tpText;

    public AudioSource soundPlayer;
    [SerializeField] GameObject soundPrefab;

    [SerializeField] AudioClip healSound;
    [SerializeField] AudioClip attackSound;

    public ParticleSystem healFX;

    CharacterManager[] characters = new CharacterManager[3];
    int currentMenu = 0;

    void Start()
    {
        menus[currentMenu].Open();

        CharacterManager[] _characters = FindObjectsOfType<CharacterManager>();
        for (int i = 0; i < _characters.Length; i++)
        {
            characters[_characters[i].GetId()] = _characters[i];
        }
        tpValue = 50;
    }

    void Update()
    {
        tpSlider.value = Mathf.Lerp(tpSlider.value, tpValue, 0.05f);
    }
    public void PlayPitchedSound(AudioClip _clip, float pitch,float volume = 1, float lifetime = 4)
    {
        var soundObject = Instantiate(soundPrefab);        
        AudioSource _audioSource = transform.GetChild(0).GetComponent<AudioSource>();

        /*_audioSource.clip = _clip;*/
        _audioSource.pitch = pitch;
        _audioSource.volume = volume;
        _audioSource.PlayOneShot(_clip);
        Destroy(soundObject, lifetime);
    }
    public void PlayHealSound()
    {
        Debug.Log("Played heal sound");
        soundPlayer.PlayOneShot(healSound);
    }
    public void AddTP(float value)
    {
        tpValue += value;
        if (tpValue < 0)
        {
            tpValue = 0;
        }
        else if (tpValue > 100)
        {
            tpValue = 100;
        }
        tpText.text = Mathf.FloorToInt(tpValue).ToString();

        Debug.Log("+" + value + "TP");
    }
    public void DrainTP(int value)
    {
        tpValue -= value;
        if (tpValue < 0)
        {
            tpValue = 0;
        }
        else if (tpValue > 100)
        {
            tpValue = 100;
        }
        tpText.text = Mathf.FloorToInt(tpValue).ToString();

        Debug.Log("-" + value + "TP");
    }
    public CharacterManager GetCharacter(int id)
    {
        return characters[id];
    }
    public CharacterManager[] GetCharacters()
    {
        return characters;
    }
    public List<Enemy> GetEnemies()
    {
        return enemies;
    }
    public Enemy GetEnemy(int id)
    {
        if (id < enemies.Count)
        {
            return enemies[id];
        }
        else
        {
            return null;
        }
    }
    public int GetCurrentMenuId()
    {
        return currentMenu;
    }
    public void NextTurn()
    {
        menus[currentMenu].Close();
        currentMenu++;
        if (currentMenu == menus.Length)
        {
            StartEnemyTurn();
        }
        else
        {
            menus[currentMenu].Open();
        }
    }
    public void OpenItemChoiceMenu()
    {
        choiceMenu.gameObject.SetActive(true);
        choiceMenu.ShowItems();
        choiceMenu.ChangePreAnimation(ChoiceMenu.AnimationState.PreItem);
    }
    public void OpenActChoiceMenu(int id)
    {
        choiceMenu.gameObject.SetActive(true);
        choiceMenu.ShowActs(id);
        choiceMenu.ChangePreAnimation(ChoiceMenu.AnimationState.PreAct);
    }
    public void OpenFightChoiceMenu()
    {
        choiceMenu.fight = true;
        choiceMenu.gameObject.SetActive(true);
        choiceMenu.ShowEnemies();
        choiceMenu.ChangePreAnimation(ChoiceMenu.AnimationState.PreFight);
    }
    public void OpenSpareChoiceMenu()
    {
        choiceMenu.fight = false;
        choiceMenu.gameObject.SetActive(true);
        choiceMenu.ShowEnemies();
        choiceMenu.ChangePreAnimation(ChoiceMenu.AnimationState.PreSpare);
    }
    public void Defend()
    {
        choiceMenu.Defend();
    }
    void StartEnemyTurn()
    {
        FindObjectOfType<BulletBoxController>().StartSpawnAnimation();
    }
    public void RemoveEnemy(Enemy enemy)
    {
        enemies.Remove(enemy);
    }
}
