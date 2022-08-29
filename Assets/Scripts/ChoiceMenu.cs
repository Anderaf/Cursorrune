using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class ChoiceMenu : MonoBehaviour
{
    public UnityEvent OnTurnUsed;
    [SerializeField] TMP_Text[] itemTexts;
    InventoryManager inventoryManager;
    BattleManager battleManager;
    ItemObject chosenItem;
    ActObject chosenAct;
    CharacterManager[] characters = new CharacterManager[3];
    ChoiceState state;
    public bool fight;
    public enum ChoiceState
    {
        Item,
        Act,
        Character,
        Enemy
    }
    public enum AnimationState
    {
        PreFight,
        PreAct,
        PreItem,
        PreSpare
    }
    private void Start()
    {
        fight = false;
        inventoryManager = FindObjectOfType<InventoryManager>();
        battleManager = FindObjectOfType<BattleManager>();
        
        ShowItems();

        CharacterManager[] _characters = FindObjectsOfType<CharacterManager>();
        for (int i = 0; i < _characters.Length; i++)
        {
            characters[_characters[i].GetId()] = _characters[i];
        }
        gameObject.SetActive(false);
    }
    public void Defend()
    {
        characters[battleManager.GetCurrentMenuId()].DefendEnter();
        OnTurnUsed.Invoke();
        chosenAct = null;
        chosenItem = null;
        gameObject.SetActive(false);
    }
    public void ChangePreAnimation(AnimationState _state)
    {
        var character = characters[battleManager.GetCurrentMenuId()];
        character.Idle();
        switch (_state)
        {
            case AnimationState.PreFight:
                character.PreSlash();
                break;
            case AnimationState.PreAct:
                character.PreActEnter();
                break;
            case AnimationState.PreItem:
                character.PreItemEnter();
                break;
            case AnimationState.PreSpare:
                character.PreActEnter();
                break;
            default:
                break;
        }
    }
    public void Clicked(int id)
    {
        switch (state)
        {
            case ChoiceState.Item:
                chosenItem = inventoryManager.GetItem(id);
                ShowCharacters();
                break;
            case ChoiceState.Act:
                chosenAct = characters[battleManager.GetCurrentMenuId()].GetAct(id);
                if (chosenAct.useOnEnemies)
                {
                    ShowEnemies();
                }
                else
                {
                    ShowCharacters();
                }
                break;
            case ChoiceState.Character:
                if (chosenItem)
                {
                    chosenItem.SetTargetId(id);
                    characters[battleManager.GetCurrentMenuId()].UseItem(chosenItem);
                    inventoryManager.DeleteItem(id);
                    chosenItem = null;
                    gameObject.SetActive(false);
                    OnTurnUsed.Invoke();
                }
                else if (chosenAct)
                {
                    chosenAct.SetTargetId(id);
                    characters[battleManager.GetCurrentMenuId()].UseAct(chosenAct);
                    chosenItem = null;
                    gameObject.SetActive(false);
                    OnTurnUsed.Invoke();
                }
                break;
            case ChoiceState.Enemy:
                if (chosenAct)
                {
                    chosenAct.SetTargetId(id);
                    characters[battleManager.GetCurrentMenuId()].UseAct(chosenAct);
                    chosenAct = null; //<--
                    gameObject.SetActive(false);
                    OnTurnUsed.Invoke();
                }
                else if (fight)
                {
                    
                    characters[battleManager.GetCurrentMenuId()].Slash(id);
                    gameObject.SetActive(false);
                    OnTurnUsed.Invoke();
                }
                else
                {
                    characters[battleManager.GetCurrentMenuId()].Spare(id);
                    gameObject.SetActive(false);
                    OnTurnUsed.Invoke();
                }
                break;
            default:
                break;
        }
    }
    public void ShowItems()
    {
        chosenAct = null;
        state = ChoiceState.Item;
        for (int i = 0; i < itemTexts.Length; i++)
        {
            if (inventoryManager.GetItem(i) != null)
            {
                itemTexts[i].enabled = true;
                itemTexts[i].text = inventoryManager.GetItem(i).name;
                itemTexts[i].GetComponent<ShowItemDescription>().description = inventoryManager.GetItem(i).description;
            }
            else
            {
                itemTexts[i].enabled = false;
            }
        }
    }
    public void ShowActs(int CharacterId)
    {
        chosenItem = null;
        state = ChoiceState.Act;
        for (int i = 0; i < itemTexts.Length; i++)
        {
            if (characters[CharacterId].GetAct(i) != null)
            {
                itemTexts[i].enabled = true;
                itemTexts[i].text = characters[CharacterId].GetAct(i).name;
                itemTexts[i].GetComponent<ShowItemDescription>().description = characters[CharacterId].GetAct(i).description;
            }
            else
            {
                itemTexts[i].enabled = false;
            }
        }
    }
    public void ShowCharacters()
    {
        state = ChoiceState.Character;
        for (int y = 0; y < itemTexts.Length; y++)
        {
            itemTexts[y].enabled = false;
        }
        for (int i = 0; i < characters.Length; i++)
        {           
            if (i < itemTexts.Length)
            {
                itemTexts[i].enabled = true;
                itemTexts[i].text = characters[i].characterName;
                itemTexts[i].GetComponent<ShowItemDescription>().description = "";
            }
        }
    }
    public void ShowEnemies()
    {
        state = ChoiceState.Enemy;
        for (int y = 0; y < itemTexts.Length; y++)
        {
            itemTexts[y].enabled = false;
        }
        for (int i = 0; i < battleManager.GetEnemies().Count; i++)
        {
            if (i < itemTexts.Length)
            {
                itemTexts[i].enabled = true;
                itemTexts[i].text = battleManager.GetEnemy(i).name;
                itemTexts[i].GetComponent<ShowItemDescription>().description = "";
            }
        }
    }
}
